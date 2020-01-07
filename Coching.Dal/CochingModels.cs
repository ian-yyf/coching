using Coching.Model;
using Coching.Model.Data;
using Microsoft.EntityFrameworkCore;
using Public.Dal;
using System;
using System.Linq;
using System.Threading.Tasks;
using Coching.Model.Front;
using Public.Containers;

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

        public async Task<Page<FNode>> getUserRoots(Guid userGuid, NodeCondition condition, int pageSize, int pageIndex)
        {
            var tables = (from n in NodesTable
                          join p in PartnersTable on n.ParentGuid equals p.KeyGuid
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

        public async Task<Guid> insertPartner(PartnerData data)
        {
            return await insert(PartnersTable, data);
        }

        public async Task modifyPartner(Guid id, PartnerData oldData, PartnerData newData)
        {
            await modify(PartnersTable, id, oldData, newData);
        }

        public async Task deletePartner(Guid id)
        {
            await delete(PartnersTable, id);
        }
    }
}
