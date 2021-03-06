﻿using Coching.Model;
using Coching.Model.Data;
using Microsoft.EntityFrameworkCore;
using Public.Dal;
using System;
using System.Linq;
using System.Threading.Tasks;
using Coching.Model.Front;
using Public.Containers;
using Public.Model.Front;
using System.Collections.Generic;
using EFCore.BulkExtensions;
using Public.Model.Data;

// add-migration
// update-database
namespace Coching.Dal
{
    public class CochingModels : Models
    {
        public CochingModels(DbContextOptions<CochingModels> options)
            : base(options)
        {

        }

        public virtual DbSet<Projects> ProjectsTable { get; set; }
        public virtual DbSet<Nodes> NodesTable { get; set; }
        public virtual DbSet<Notes> NotesTable { get; set; }
        public virtual DbSet<Partners> PartnersTable { get; set; }
        public virtual DbSet<Offers> OffersTable { get; set; }
        public virtual DbSet<ActionLogs> ActionLogsTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Projects>().HasIndex(e => e.CreatorGuid);
            modelBuilder.Entity<Projects>().HasIndex(e => e.Name);
            modelBuilder.Entity<Nodes>().HasIndex(e => e.ProjectGuid);
            modelBuilder.Entity<Nodes>().HasIndex(e => e.RootGuid);
            modelBuilder.Entity<Nodes>().HasIndex(e => e.ParentGuid);
            modelBuilder.Entity<Nodes>().HasIndex(e => e.WorkerGuid);
            modelBuilder.Entity<Nodes>().HasIndex(e => e.EndTime);
            modelBuilder.Entity<Notes>().HasIndex(e => e.NodeGuid);
            modelBuilder.Entity<Notes>().HasIndex(e => e.CreatorGuid);
            modelBuilder.Entity<Partners>().HasIndex(e => e.ProjectGuid);
            modelBuilder.Entity<Partners>().HasIndex(e => e.UserGuid);
            modelBuilder.Entity<Offers>().HasIndex(e => e.NodeGuid);
            modelBuilder.Entity<Offers>().HasIndex(e => e.UserGuid);
            modelBuilder.Entity<ActionLogs>().HasIndex(e => e.ProjectGuid);
            modelBuilder.Entity<ActionLogs>().HasIndex(e => e.UserGuid);
        }

        public async Task<bool> checkRoot(Guid nodeGuid, Guid rootGuid)
        {
            return await (from n in NodesTable
                          where n.KeyGuid == nodeGuid
                          select n.RootGuid).SingleAsync() == rootGuid;

        }

        public async Task<bool> checkProjectPartner(Guid projectGuid, Guid userGuid)
        {
            return await (from p in PartnersTable
                          where p.ProjectGuid == projectGuid && p.UserGuid == userGuid && p.Deleted == false
                          select p.KeyGuid).FirstOrDefaultAsync() != Guid.Empty;
        }

        public async Task<bool> checkProjectsPartner(Guid[] projectGuids, Guid userGuid)
        {
            return await (from p in PartnersTable
                          where projectGuids.Contains(p.ProjectGuid) && p.UserGuid == userGuid && p.Deleted == false
                          select p.KeyGuid).CountAsync() == projectGuids.Length;
        }

        public async Task<bool> checkProjectAdminPartner(Guid projectGuid, Guid userGuid)
        {
            var roles = new int[]
            {
                (int)PartnerRole.管理员,
            };

            return await (from p in PartnersTable
                          where p.ProjectGuid == projectGuid && p.UserGuid == userGuid && roles.Contains(p.Role) && p.Deleted == false
                          select p.KeyGuid).FirstOrDefaultAsync() != Guid.Empty;
        }

        public async Task<bool> checkProjectModifyPartner(Guid projectGuid, Guid userGuid)
        {
            var roles = new int[]
            {
                (int)PartnerRole.管理员,
                (int)PartnerRole.执行者,
            };

            return await (from p in PartnersTable
                          where p.ProjectGuid == projectGuid && p.UserGuid == userGuid && roles.Contains(p.Role) && p.Deleted == false
                          select p.KeyGuid).FirstOrDefaultAsync() != Guid.Empty;
        }

        public async Task<bool> checkNodePartner(Guid nodeGuid, Guid userGuid)
        {
            var node = await (from n in NodesTable
                              where n.KeyGuid == nodeGuid
                              select n).SingleAsync();

            return await checkProjectPartner(node.ProjectGuid, userGuid);
        }

        public async Task<bool> checkNodeAdminPartner(Guid nodeGuid, Guid userGuid)
        {
            var node = await (from n in NodesTable
                              where n.KeyGuid == nodeGuid
                              select n).SingleAsync();

            return await checkProjectAdminPartner(node.ProjectGuid, userGuid);
        }

        public async Task<bool> checkNodeModifyPartner(Guid nodeGuid, Guid userGuid)
        {
            var node = await (from n in NodesTable
                              where n.KeyGuid == nodeGuid
                              select n).SingleAsync();

            return await checkProjectModifyPartner(node.ProjectGuid, userGuid);
        }

        public async Task<bool> checkNotePartner(Guid noteGuid, Guid userGuid)
        {
            var node = await (from t in NotesTable
                              join n in NodesTable on t.NodeGuid equals n.KeyGuid
                              where t.KeyGuid == noteGuid
                              select n).SingleAsync();

            return await checkProjectPartner(node.ProjectGuid, userGuid);
        }

        public async Task<bool> checkNoteModifyPartner(Guid noteGuid, Guid userGuid)
        {
            var node = await (from t in NotesTable
                              join n in NodesTable on t.NodeGuid equals n.KeyGuid
                              where t.KeyGuid == noteGuid
                              select n).SingleAsync();

            return await checkProjectModifyPartner(node.ProjectGuid, userGuid);
        }

        public async Task<bool> checkPartnerModifyPartner(Guid partnerGuid, Guid userGuid)
        {
            var partner = await (from p in PartnersTable
                                 where p.KeyGuid == partnerGuid
                                 select p).SingleAsync();

            return await checkProjectAdminPartner(partner.ProjectGuid, userGuid);
        }

        public async Task<bool> checkLeaf(Guid nodeGuid)
        {
            return await (from n in NodesTable where n.ParentGuid == nodeGuid && n.Deleted == false select n.KeyGuid).FirstOrDefaultAsync() == Guid.Empty;
        }

        public async Task<bool> checkCochingUp(Guid nodeGuid)
        {
            if (nodeGuid == Guid.Empty)
            {
                return false;
            }

            var db = (from n in NodesTable where n.KeyGuid == nodeGuid select n).Single();
            if (db.Coching)
            {
                return true;
            }
            if (db.isRoot())
            {
                return false;
            }
            return await checkCochingUp(db.ParentGuid);
        }

        public async Task<bool> checkCochingChildren(Guid[] parentGuids)
        {
            var dbs = await (from n in NodesTable where parentGuids.Contains(n.ParentGuid) select new { n.KeyGuid, n.Coching }).ToArrayAsync();
            if (dbs.Length == 0)
            {
                return false;
            }
            if (dbs.FirstOrDefault(db => db.Coching) != null)
            {
                return true;
            }
            return await checkCochingChildren(dbs.Select(db => db.KeyGuid).ToArray());
        }

        public async Task<FProject[]> getProjectsOfUser(Guid userGuid, ProjectCondition condition, int pageSize, int pageIndex)
        {
            var dbs = await (from n in ProjectsTable
                             join p in PartnersTable on new { k = n.KeyGuid, d = false, u = userGuid } equals new { k = p.ProjectGuid, d = p.Deleted, u = p.UserGuid }
                             join c in UsersTable on n.CreatorGuid equals c.KeyGuid
                             orderby n.CreatedTime descending
                             where n.Deleted == false
                             select new { n, c }).AsNoTracking().pageOnlyAsync(pageSize, pageIndex);

            var projects = dbs.Select(db => db.n.KeyGuid);
            var partners = await (from p in PartnersTable
                                  join u in UsersTable on p.UserGuid equals u.KeyGuid
                                  where projects.Contains(p.ProjectGuid) && p.Deleted == false
                                  orderby p.JoinTime
                                  select new { p, u }).AsNoTracking().ToArrayAsync();

            return (from db in dbs
                    select new FProject(db.n.KeyGuid, db.n, new FUser(db.c.KeyGuid, db.c)
                    , (from p in partners where p.p.ProjectGuid == db.n.KeyGuid
                       select new FPartner(p.p.KeyGuid, p.p, new FUser(p.u.KeyGuid, p.u))).ToArray())).ToArray();
        }

        public async Task<FProject> getProject(Guid id)
        {
            var db = await (from n in ProjectsTable
                            join c in UsersTable on n.CreatorGuid equals c.KeyGuid
                            where n.KeyGuid == id
                            select new { n, c }).AsNoTracking().SingleAsync();

            var partners = await (from p in PartnersTable
                                  join u in UsersTable on p.UserGuid equals u.KeyGuid
                                  where p.ProjectGuid == db.n.KeyGuid && p.Deleted == false
                                  orderby p.JoinTime
                                  select new { p, u }).AsNoTracking().ToArrayAsync();

            return new FProject(db.n.KeyGuid, db.n, new FUser(db.c.KeyGuid, db.c)
                , (from p in partners
                   select new FPartner(p.p.KeyGuid, p.p, new FUser(p.u.KeyGuid, p.u))).ToArray());
        }

        public async Task<Guid> insertProject(ProjectData data)
        {
            return await insert(ProjectsTable, data);
        }

        public async Task modifyProject(Guid id, ProjectData oldData, ProjectData newData)
        {
            await modify(ProjectsTable, id, oldData, newData);
        }

        public async Task deleteProject(Guid id)
        {
            await delete(ProjectsTable, id);
        }

        public async Task<FRoot> getRoot(Guid id)
        {
            var db = await (from n in NodesTable
                            join c in UsersTable on n.CreatorGuid equals c.KeyGuid
                            join w in UsersTable on n.WorkerGuid equals w.KeyGuid into ws
                            from w in ws.DefaultIfEmpty()
                            where n.KeyGuid == id
                            select new { n, c, w }).AsNoTracking().SingleAsync();

            var workers = await (from n in NodesTable
                                 where n.RootGuid == db.n.KeyGuid && n.Deleted == false && n.WorkerGuid != Guid.Empty
                                 select n.WorkerGuid).Distinct().ToArrayAsync();

            return new FRoot(new FNode(db.n.KeyGuid, db.n, new FUser(db.c.KeyGuid, db.c), db.w == null ? null : new FUser(db.w.KeyGuid, db.w)), workers);
        }

        public async Task<Page<FRoot>> getRootsOfProject(Guid projectGuid, int pageSize, int pageIndex)
        {
            var dbs = await (from n in NodesTable
                             join c in UsersTable on n.CreatorGuid equals c.KeyGuid
                             join w in UsersTable on n.WorkerGuid equals w.KeyGuid into ws
                             from w in ws.DefaultIfEmpty()
                             where n.Deleted == false && n.ParentGuid == Guid.Empty && n.ProjectGuid == projectGuid
                             orderby n.CreatedTime descending
                             select new { n, c, w }).AsNoTracking().pageAsync(pageSize, pageIndex);

            var roots = dbs.Items.Select(db => db.n.KeyGuid);
            var workers = await (from n in NodesTable
                                 where roots.Contains(n.RootGuid) && n.Deleted == false && n.WorkerGuid != Guid.Empty
                                 select new { n.RootGuid, n.WorkerGuid }).Distinct().ToArrayAsync();

            return new Page<FRoot>(dbs.TotalCount, dbs.Items.Select(db => 
                new FRoot(new FNode(db.n.KeyGuid, db.n, new FUser(db.c.KeyGuid, db.c), db.w == null ? null : new FUser(db.w.KeyGuid, db.w))
                , (from w in workers where w.RootGuid == db.n.KeyGuid select w.WorkerGuid).ToArray())).ToArray());
        }

        private FNode[] findNodes(dynamic[] nodes, Guid parentGuid)
        {
            var result = (from db in nodes
                          where db.n.ParentGuid == parentGuid
                          select new FNode(db.n.KeyGuid, db.n, new FUser(db.c.KeyGuid, db.c), db.w == null ? null : new FUser(db.w.KeyGuid, db.w))).ToArray();
            foreach (var n in result)
            {
                n.Children = findNodes(nodes, n.ID);
            }
            return result;
        }

        public async Task<FNode> getTree(Guid id)
        {
            var dbs = await (from n in NodesTable
                             join c in UsersTable on n.CreatorGuid equals c.KeyGuid
                             join w in UsersTable on n.WorkerGuid equals w.KeyGuid into ws
                             from w in ws.DefaultIfEmpty()
                             where n.RootGuid == id && n.Deleted == false
                             orderby n.CreatedTime
                             select new { n, c, w }).AsNoTracking().ToArrayAsync();
            return findNodes(dbs, Guid.Empty).Single();
        }

        public async Task __addActualManHour(Guid id, decimal hour)
        {
            if (id == Guid.Empty)
            {
                return;
            }

            var db = await (from n in NodesTable where n.KeyGuid == id select n).SingleAsync();
            db.ActualManHour += hour;
            await __addActualManHour(db.ParentGuid, hour);
        }

        public async Task addActualManHour(Guid id, decimal hour)
        {
            await __addActualManHour(id, hour);
            await _safeSaveChanges();
        }

        public async Task addCoching(Guid projectGuid, Guid userGuid, Decimal coching)
        {
            if (coching == 0)
            {
                return;
            }

            var db = await (from p in PartnersTable
                            where p.ProjectGuid == projectGuid && p.UserGuid == userGuid && p.Deleted == false
                            select p).FirstOrDefaultAsync();
            if (db != null)
            {
                db.Coching += coching;
                await _safeSaveChanges();
            }
        }

        public async Task<FTask[]> getTasks(Guid userGuid, NodeCondition condition, int pageSize, int pageIndex)
        {
            var dbs = await (from pr in ProjectsTable
                             join pa in PartnersTable on new { id = pr.KeyGuid, d = false, uid = userGuid } equals new { id = pa.ProjectGuid, d = pa.Deleted, uid = pa.UserGuid }
                             join n in NodesTable on pr.KeyGuid equals n.ProjectGuid
                             join r in NodesTable on n.RootGuid equals r.KeyGuid
                             join c in UsersTable on n.CreatorGuid equals c.KeyGuid
                             join w in UsersTable on n.WorkerGuid equals w.KeyGuid into ws
                             from w in ws.DefaultIfEmpty()
                             where pr.Deleted == false && n.Deleted == false
                             && (condition.WorkerGuid == null || condition.WorkerGuid == n.WorkerGuid)
                             && (condition.Status == null || (int)condition.Status.Value == n.Status)
                             && (condition.Coching == null || condition.Coching == n.Coching)
                             orderby n.LastModifyTime descending
                             select new { pr, n, r, c, w }).AsNoTracking().pageOnlyAsync(pageSize, pageIndex);

            return (from db in dbs 
                    select new FTask(new FProject(db.pr.KeyGuid, db.pr, null, null)
                    , new FNode(db.r.KeyGuid, db.r, null, null)
                    , new FNode(db.n.KeyGuid, db.n, new FUser(db.c.KeyGuid, db.c), db.w == null ? null : new FUser(db.w.KeyGuid, db.w)))).ToArray();
        }

        public async Task<FNode> getNode(Guid id)
        {
            var db = await (from n in NodesTable
                            join c in UsersTable on n.CreatorGuid equals c.KeyGuid
                            join w in UsersTable on n.WorkerGuid equals w.KeyGuid into ws
                            from w in ws.DefaultIfEmpty()
                            where n.KeyGuid == id
                            select new { n, c, w }).AsNoTracking().SingleAsync();
            return new FNode(db.n.KeyGuid, db.n, new FUser(db.c.KeyGuid, db.c), db.w == null ? null : new FUser(db.w.KeyGuid, db.w));
        }

        public async Task<Guid> insertNode(NodeData data)
        {
            return await insert(NodesTable, data);
        }

        public async Task modifyNode(Guid id, NodeData oldData, NodeData newData)
        {
            await modify(NodesTable, id, oldData, newData);
        }

        private async Task<bool> __deleteChildren(IEnumerable<Guid> ids, Func<Nodes, Task> onDeleted)
        {
            var dbs = await (from n in NodesTable where ids.Contains(n.ParentGuid) && n.Deleted == false select n).ToArrayAsync();
            var parentIds = new List<Guid>();
            var modified = false;
            foreach (var db in dbs)
            {
                db.Deleted = true;
                parentIds.Add(db.KeyGuid);
                modified = true;

                await onDeleted?.Invoke(db);
            }

            if (modified)
            {
                await __deleteChildren(parentIds, onDeleted);
            }

            return modified;
        }

        private async Task __deleteDocumentRefs(Guid ownerGuid)
        {
            var dbs = await (from d in DocumentRefsTable where d.OwnerGuid == ownerGuid && d.Deleted == false select d).ToArrayAsync();
            foreach (var db in dbs)
            {
                db.Deleted = true;
            }
        }

        public async Task deleteChildren(Guid id, Func<Nodes, Task> onDeleted)
        {
            if (await __deleteChildren(new Guid[] { id }, onDeleted))
            {
                await _safeSaveChanges();
            }
        }

        public async Task deleteNode(Guid id)
        {
            await __deleteDocumentRefs(id);
            await delete(NodesTable, id);
        }

        public async Task<FNote[]> getNotesOfNode(Guid nodeGuid)
        {
            var dbs = await (from n in NotesTable
                             join u in UsersTable on n.CreatorGuid equals u.KeyGuid
                             where n.NodeGuid == nodeGuid && n.Deleted == false
                             orderby n.CreatedTime descending
                             select new { n, u }).AsNoTracking().ToArrayAsync();

            var documents = await getDocumentRefsOfOwners(dbs.Select(db => db.n.KeyGuid).ToArray());
            return dbs.Select(db => new FNote(db.n.KeyGuid, db.n, new FUser(db.u.KeyGuid, db.u), documents.Where(d => d.OwnerGuid == db.n.KeyGuid).ToArray())).ToArray();
        }

        public async Task<FNote> getNote(Guid id)
        {
            var db = await (from n in NotesTable
                            join u in UsersTable on n.CreatorGuid equals u.KeyGuid
                            where n.KeyGuid == id
                            select new { n, u }).AsNoTracking().SingleAsync();

            var documents = await getDocumentRefs(db.n.KeyGuid);
            return new FNote(db.n.KeyGuid, db.n, new FUser(db.u.KeyGuid, db.u), documents);
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
            await __deleteDocumentRefs(id);
            await delete(NotesTable, id);
        }

        public async Task<FUser[]> getUsers(Guid userGuid, UserCondition condition)
        {
            if (condition.ProjectName == null)
            {
                var dbs = await (from u in UsersTable
                                 where u.Deleted == false && (u.Tel.Contains(condition.Key) || u.Name.Contains(condition.Key))
                                 orderby u.Tel
                                 select u).Take(20).AsNoTracking().ToArrayAsync();
                return dbs.Select(db => new FUser(db.KeyGuid, db)).ToArray();
            }
            else
            {
                var projectGuid = await (from pr in ProjectsTable
                                         join pa in PartnersTable on new { k = pr.KeyGuid, d = false } equals new { k = pa.ProjectGuid, d = pa.Deleted }
                                         where pr.Deleted == false && pa.UserGuid == userGuid && pr.Name.Contains(condition.ProjectName)
                                         select pr.KeyGuid).FirstOrDefaultAsync();

                if (projectGuid == Guid.Empty)
                {
                    return new FUser[] { };
                }

                var dbs = await (from p in PartnersTable
                                 join u in UsersTable on p.UserGuid equals u.KeyGuid
                                 where p.ProjectGuid == projectGuid && p.Deleted == false
                                 orderby u.Tel
                                 select u).AsNoTracking().ToArrayAsync();
                return dbs.Select(db => new FUser(db.KeyGuid, db)).ToArray();
            }
        }

        public async Task<FUser[]> getRelatedUsers(Guid userGuid)
        {
            var dbs = await (from pr in ProjectsTable
                             join pa in PartnersTable on new { id = pr.KeyGuid, d = false, uid = userGuid } equals new { id = pa.ProjectGuid, d = pa.Deleted, uid = pa.UserGuid }
                             join pa1 in PartnersTable on new { id = pr.KeyGuid, d = false } equals new { id = pa1.ProjectGuid, d = pa1.Deleted }
                             join u in UsersTable on pa1.UserGuid equals u.KeyGuid
                             select u).AsNoTracking().Distinct().ToArrayAsync();

            return (from db in dbs select new FUser(db.KeyGuid, db)).ToArray();
        }

        public async Task<FPartner> getPartnerOfProject(Guid projectGuid, Guid userGuid)
        {
            var db = await (from p in PartnersTable
                            join u in UsersTable on p.UserGuid equals u.KeyGuid
                            where p.ProjectGuid == projectGuid && p.UserGuid == userGuid && p.Deleted == false
                            select new { p, u }).AsNoTracking().SingleAsync();
            return new FPartner(db.p.KeyGuid, db.p, new FUser(db.u.KeyGuid, db.u));
        }

        public async Task<FPartner[]> getPartnersOfProject(Guid projectGuid, PartnerCondition condition)
        {
            var roles = condition.Roles == null ? null : condition.Roles.Select(r => (int)r);
            var dbs = await (from p in PartnersTable
                             join u in UsersTable on p.UserGuid equals u.KeyGuid
                             where p.ProjectGuid == projectGuid && p.Deleted == false
                                && (roles == null || roles.Contains(p.Role))
                             orderby p.JoinTime
                             select new { p, u }).AsNoTracking().ToArrayAsync();

            return dbs.Select(db => new FPartner(db.p.KeyGuid, db.p, new FUser(db.u.KeyGuid, db.u))).ToArray();
        }

        public async Task<FPartner> getPartner(Guid id)
        {
            var db = await (from p in PartnersTable
                            join u in UsersTable on p.UserGuid equals u.KeyGuid
                            where p.KeyGuid == id
                            select new { p, u }).AsNoTracking().SingleAsync();
            return new FPartner(db.p.KeyGuid, db.p, new FUser(db.u.KeyGuid, db.u));
        }

        public async Task<Guid> insertPartner(PartnerData data)
        {
            return await insert(PartnersTable, data);
        }

        public async Task<Guid> setPartner(Guid projectGuid, Guid userGuid, PartnerRole role)
        {
            var db = await (from p in PartnersTable
                            where p.ProjectGuid == projectGuid && p.UserGuid == userGuid && p.Deleted == false
                            select p).SingleOrDefaultAsync();
            if (db == null)
            {
                return await insert(PartnersTable, new PartnerData(projectGuid, userGuid, role));
            }

            if (db.getRole() != role)
            {
                db.Role = (int)role;
                await _safeSaveChanges();
            }

            return db.KeyGuid;
        }

        public async Task deletePartner(Guid id)
        {
            await delete(PartnersTable, id);
        }

        public async Task<FOffer[]> getOffersOfNode(Guid nodeGuid)
        {
            var dbs = await (from o in OffersTable
                             join u in UsersTable on o.UserGuid equals u.KeyGuid
                             orderby o.EstimatedManHour
                             where o.Deleted == false && o.NodeGuid == nodeGuid
                             select new { o, u }).AsNoTracking().ToArrayAsync();
            return dbs.Select(db => new FOffer(db.o.KeyGuid, db.o, new FUser(db.u.KeyGuid, db.u))).ToArray();
        }

        public async Task<FOffer> getOffer(Guid nodeGuid, Guid userGuid)
        {
            var db = await (from o in OffersTable
                            join u in UsersTable on o.UserGuid equals u.KeyGuid
                            where o.NodeGuid == nodeGuid && o.UserGuid == userGuid
                            select new { o, u }).AsNoTracking().SingleOrDefaultAsync();
            return db == null ? null : new FOffer(db.o.KeyGuid, db.o, new FUser(db.u.KeyGuid, db.u));
        }

        public async Task<FOffer> getOffer(Guid id)
        {
            var db = await (from o in OffersTable
                            join u in UsersTable on o.UserGuid equals u.KeyGuid
                            where o.KeyGuid == id
                            select new { o, u }).AsNoTracking().SingleAsync();
            return new FOffer(db.o.KeyGuid, db.o, new FUser(db.u.KeyGuid, db.u));
        }

        public async Task<Guid> offerToNode(Guid nodeGuid, Guid userGuid, decimal estimatedManHour)
        {
            var db = await (from o in OffersTable
                            where o.NodeGuid == nodeGuid && o.UserGuid == userGuid && o.Deleted == false
                            select o).SingleOrDefaultAsync();

            if (db == null)
            {
                return await insert(OffersTable, new OfferData(nodeGuid, userGuid, estimatedManHour));
            }

            if (db.EstimatedManHour != estimatedManHour)
            {
                db.EstimatedManHour = estimatedManHour;
                await _safeSaveChanges();
            }

            return db.KeyGuid;
        }

        public async Task<Guid> addActionLog(ActionLogData data, bool save)
        {
            var db = new ActionLogs(data);
            ActionLogsTable.Add(db);
            if (save)
            {
                await _safeSaveChanges();
            }
            return db.KeyGuid;
        }

        public async Task<FActionLog[]> getActionLogsOfUser(Guid userGuid, int pageSize, int pageIndex)
        {
            var dbs = await (from p in PartnersTable
                             join l in ActionLogsTable on p.ProjectGuid equals l.ProjectGuid
                             join c in UsersTable on l.UserGuid equals c.KeyGuid
                             orderby l.CreatedTime descending
                             where l.Deleted == false && p.Deleted == false && p.UserGuid == userGuid
                             select new { l, c }).AsNoTracking().pageOnlyAsync(pageSize, pageIndex);

            return dbs.Select(db => new FActionLog(db.l.KeyGuid, db.l, new FUser(db.c.KeyGuid, db.c))).ToArray();
        }

        public async Task<FCoching[]> charts(Guid[] projectGuids)
        {
            var dbs = await (from p in (from p in PartnersTable
                             where projectGuids.Contains(p.ProjectGuid) && p.Deleted == false
                             group p by p.UserGuid into ps
                             select new { id = ps.Key, c = ps.Sum(p => p.Coching) })
                             join u in UsersTable on p.id equals u.KeyGuid
                             select new { u, p.c }).AsNoTracking().ToArrayAsync();

            var startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            var todays = await (from n in NodesTable
                                where projectGuids.Contains(n.ProjectGuid) && n.EndTime >= startTime && n.Coching == true && n.Status == (int)NodeStatus.完成
                                group n by n.WorkerGuid into ns
                                select new { uid = ns.Key, coching = ns.Sum(n => n.EstimatedManHour) }).ToArrayAsync();

            return (from db in dbs select new FCoching(new FUser(db.u.KeyGuid, db.u), db.c, todays.FirstOrDefault(t => t.uid == db.u.KeyGuid)?.coching ?? 0)).ToArray();
        }

        public async Task<StatusLogData[]> getStatusLogDatas(Guid ownerGuid)
        {
            var dbs = await getStatusLogDbs(ownerGuid);
            return dbs.Select(db => (StatusLogData)db).ToArray();
        }
    }
}
