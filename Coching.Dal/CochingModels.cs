using Coching.Model;
using Coching.Model.Data;
using Microsoft.EntityFrameworkCore;
using Public.Dal;
using System;
using System.Linq;
using System.Threading.Tasks;
using Coching.Model.Front;
using Public.Containers;
using Public.Model.Front;

namespace Coching.Dal
{
    public class CochingModels : Models
    {
        public CochingModels(DbContextOptions<CochingModels> options)
            : base(options)
        {

        }

        public virtual DbSet<Nodes> NodesTable { get; set; }
        public virtual DbSet<Notes> NotesTable { get; set; }
        public virtual DbSet<Partners> PartnersTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        
        public async Task<bool> checkRoot(Guid nodeGuid, Guid rootGuid)
        {
            return await (from n in NodesTable
                          where n.KeyGuid == nodeGuid
                          select n.RootGuid).SingleAsync() == rootGuid;

        }

        public async Task<bool> checkNodePartner(Guid nodeGuid, Guid rootGuid, Guid userGuid)
        {
            return await (from p in PartnersTable
                          where (p.NodeGuid == nodeGuid || p.NodeGuid == rootGuid) && p.UserGuid == userGuid && p.Deleted == false
                          select p.KeyGuid).FirstOrDefaultAsync() != Guid.Empty;
        }

        public async Task<bool> checkNodeModifyPartner(Guid nodeGuid, Guid rootGuid, Guid userGuid)
        {
            var roles = new int[]
            {
                (int)PartnerRole.管理员,
                (int)PartnerRole.执行者,
            };

            return await (from p in PartnersTable
                          where (p.NodeGuid == nodeGuid || p.NodeGuid == rootGuid) && p.UserGuid == userGuid && roles.Contains(p.Role) && p.Deleted == false
                          select p.KeyGuid).FirstOrDefaultAsync() != Guid.Empty;
        }

        public async Task<bool> checkNodePartner(Guid nodeGuid, Guid userGuid)
        {
            var node = await (from n in NodesTable
                              where n.KeyGuid == nodeGuid
                              select n).SingleAsync();

            return await checkNodePartner(node.KeyGuid, node.RootGuid, userGuid);
        }

        public async Task<bool> checkNodeModifyPartner(Guid nodeGuid, Guid userGuid)
        {
            var node = await (from n in NodesTable
                              where n.KeyGuid == nodeGuid
                              select n).SingleAsync();

            return await checkNodeModifyPartner(node.KeyGuid, node.RootGuid, userGuid);
        }

        public async Task<bool> checkNotePartner(Guid noteGuid, Guid userGuid)
        {
            var node = await (from t in NotesTable
                              join n in NodesTable on t.NodeGuid equals n.KeyGuid
                              where t.KeyGuid == noteGuid
                              select n).SingleAsync();

            return await checkNodePartner(node.KeyGuid, node.RootGuid, userGuid);
        }

        public async Task<bool> checkNoteModifyPartner(Guid noteGuid, Guid userGuid)
        {
            var node = await (from t in NotesTable
                              join n in NodesTable on t.NodeGuid equals n.KeyGuid
                              where t.KeyGuid == noteGuid
                              select n).SingleAsync();

            return await checkNodeModifyPartner(node.KeyGuid, node.RootGuid, userGuid);
        }

        public async Task<bool> checkPartnerModifyPartner(Guid partnerGuid, Guid userGuid)
        {
            var node = await (from p in PartnersTable
                              join n in NodesTable on p.NodeGuid equals n.KeyGuid
                              where p.KeyGuid == partnerGuid
                              select n).SingleAsync();

            return await checkNodeModifyPartner(node.KeyGuid, node.RootGuid, userGuid);
        }

        public async Task<Page<FNode>> getUserRoots(Guid userGuid, NodeCondition condition, int pageSize, int pageIndex)
        {
            var tables = (from n in NodesTable
                          join p in PartnersTable on n.KeyGuid equals p.NodeGuid
                          select new { n, p.JoinTime, p.UserGuid });

            var sql = tables.build(db => db.n.Deleted == false && db.UserGuid == userGuid && db.n.ParentGuid == Guid.Empty);
            var dbs = await tables.Where(sql).OrderByDescending(db => db.JoinTime).pageAsync(pageSize, pageIndex);

            return new Page<FNode>(dbs.TotalCount, dbs.Items.Select(db => new FNode(db.n.KeyGuid, db.n)).ToArray());
        }

        private FNode[] findNodes(Nodes[] nodes, Guid parentGuid)
        {
            var result = (from n in nodes
                          where n.ParentGuid == parentGuid
                          select new FNode(n.KeyGuid, n)).ToArray();
            foreach (var n in result)
            {
                n.Children = findNodes(nodes, n.ID);
            }
            return result;
        }

        public async Task<FNode> getTree(Guid id)
        {
            var dbs = await (from n in NodesTable
                             where n.RootGuid == id
                             select n).ToArrayAsync();
            return findNodes(dbs, Guid.Empty).Single();
        }

        public async Task<FNode> getNode(Guid id)
        {
            var db = await (from n in NodesTable
                            where n.KeyGuid == id
                            select n).SingleAsync();
            return new FNode(db.KeyGuid, db);
        }

        public async Task<Guid> insertNode(NodeData data)
        {
            return await insert(NodesTable, data);
        }

        public async Task modifyNode(Guid id, NodeData oldData, NodeData newData)
        {
            await modify(NodesTable, id, oldData, newData);
        }

        public async Task deleteNode(Guid id)
        {
            await delete(NodesTable, id);
        }

        public async Task<FNote[]> getNotesOfNode(Guid nodeGuid)
        {
            var dbs = await (from n in NotesTable
                             join u in UsersTable on n.CreatorGuid equals u.KeyGuid
                             where n.NodeGuid == nodeGuid && n.Deleted == false
                             orderby n.CreatedTime descending
                             select new { n, u }).ToArrayAsync();
            return dbs.Select(db => new FNote(db.n.KeyGuid, db.n, new FUser(db.u.KeyGuid, db.u))).ToArray();
        }

        public async Task<FNote> getNote(Guid id)
        {
            var db = await (from n in NotesTable
                            join u in UsersTable on n.CreatorGuid equals u.KeyGuid
                            where n.KeyGuid == id
                            select new { n, u }).SingleAsync();
            return new FNote(db.n.KeyGuid, db.n, new FUser(db.u.KeyGuid, db.u));
        }

        public async Task<Guid> insertNote(NoteData data)
        {
            return await insert(NotesTable, data);
        }

        public async Task modifyNote(Guid id, NoteData oldData, NoteData newData)
        {
            await modify(NotesTable, id, oldData, newData);
        }

        public async Task deleteNote(Guid id)
        {
            await delete(NotesTable, id);
        }

        public async Task<FPartner> getPartner(Guid id)
        {
            var db = await (from p in PartnersTable
                            join u in UsersTable on p.UserGuid equals u.KeyGuid
                            where p.KeyGuid == id
                            select new { p, u }).SingleAsync();
            return new FPartner(db.p.KeyGuid, db.p, new FUser(db.u.KeyGuid, db.u));
        }

        public async Task<Guid> insertPartner(PartnerData data)
        {
            return await insert(PartnersTable, data);
        }

        public async Task deletePartner(Guid id)
        {
            await delete(PartnersTable, id);
        }
    }
}
