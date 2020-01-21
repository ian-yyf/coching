using Coching.Dal;
using Coching.Model.Data;
using Coching.Model.Front;
using Public.Containers;
using Public.Model.Data;
using Public.Model.Front;
using Public.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Coching.Bll
{
    public class CochingWork : _Work
    {
        public CochingWork(CochingModels models)
            : base(models)
        {

        }

        public async Task<Result<FProject[]>> getProjectsOfUser(FUserToken token, Guid userGuid, ProjectCondition condition)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FProject[]>(false, null, "请重新登录");
            }

            if (token.ID != userGuid)
            {
                return new Result<FProject[]>(false, null, "没有权限");
            }

            return new Result<FProject[]>(await _models.getProjectsOfUser(userGuid, condition));
        }

        public async Task<Result<FProject>> getProject(FUserToken token, Guid id)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FProject>(false, null, "请重新登录");
            }

            if (!await _models.checkProjectPartner(id, token.ID))
            {
                return new Result<FProject>(false, null, "没有权限");
            }

            return new Result<FProject>(await _models.getProject(id));
        }

        public async Task<Result<FProject>> insertProject(FUserToken token, ProjectData data)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FProject>(false, null, "请重新登录");
            }

            if (data.CreatorGuid != token.ID)
            {
                return new Result<FProject>(false, null, "没有权限");
            }

            var tran = _models.Database.BeginTransaction();
            var id = await _models.insertProject(data);
            await _models.insertPartner(new PartnerData(id, data.CreatorGuid, PartnerRole.管理员));
            await tran.CommitAsync();

            return new Result<FProject>(await _models.getProject(id));
        }

        public async Task<Result<FProject>> modifyProject(FUserToken token, Guid id, ProjectData oldData, ProjectData newData)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FProject>(false, null, "请重新登录");
            }

            if (!await _models.checkProjectModifyPartner(id, token.ID))
            {
                return new Result<FProject>(false, null, "没有权限");
            }

            if (oldData.CreatorGuid != newData.CreatorGuid)
            {
                return new Result<FProject>(false, null, "没有权限");
            }

            await _models.modifyProject(id, oldData, newData);
            return new Result<FProject>(await _models.getProject(id));
        }

        public async Task<Result<bool>> deleteProject(FUserToken token, Guid id)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<bool>(false, false, "请重新登录");
            }

            if (!await _models.checkProjectAdminPartner(id, token.ID))
            {
                return new Result<bool>(false, false, "没有权限");
            }

            await _models.deleteProject(id);
            return new Result<bool>(true);
        }

        public async Task<Result<FRoot>> getRoot(FUserToken token, Guid id)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FRoot>(false, null, "请重新登录");
            }

            if (!await _models.checkNodePartner(id, token.ID))
            {
                return new Result<FRoot>(false, null, "没有权限");
            }

            return new Result<FRoot>(await _models.getRoot(id));
        }

        public async Task<Result<Page<FRoot>>> getRootsOfProject(FUserToken token, Guid projectGuid, NodeCondition condition, int pageSize, int pageIndex)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<Page<FRoot>>(false, null, "请重新登录");
            }

            if (!await _models.checkProjectPartner(projectGuid, token.ID))
            {
                return new Result<Page<FRoot>>(false, null, "没有权限");
            }

            return new Result<Page<FRoot>>(await _models.getRootsOfProject(projectGuid, condition, pageSize, pageIndex));
        }

        public async Task<Result<FNode>> getTree(FUserToken token, Guid id)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FNode>(false, null, "请重新登录");
            }

            if (!await _models.checkNodePartner(id, token.ID))
            {
                return new Result<FNode>(false, null, "没有权限");
            }

            return new Result<FNode>(await _models.getTree(id));
        }

        public async Task<Result<FNode>> getNode(FUserToken token, Guid id)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FNode>(false, null, "请重新登录");
            }

            if (!await _models.checkNodePartner(id, token.ID))
            {
                return new Result<FNode>(false, null, "没有权限");
            }

            return new Result<FNode>(await _models.getNode(id));
        }

        public async Task<Result<FNodeModify>> getNodeModify(FUserToken token, Guid id)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FNodeModify>(false, null, "请重新登录");
            }

            if (!await _models.checkNodeModifyPartner(id, token.ID))
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            var node = await _models.getNode(id);
            var documents = await _models.getDocumentRefs(id);
            return new Result<FNodeModify>(new FNodeModify(node, documents));
        }

        public async Task<Result<FNodeDetail>> getNodeDetail(FUserToken token, Guid id)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FNodeDetail>(false, null, "请重新登录");
            }

            if (!await _models.checkNodePartner(id, token.ID))
            {
                return new Result<FNodeDetail>(false, null, "没有权限");
            }

            var node = await _models.getNode(id);
            var notes = await _models.getNotesOfNode(node.ID);
            FOffer[] offers;
            if (await _models.checkNodeAdminPartner(id, token.ID))
            {
                offers = await _models.getOffersOfNode(id);
            }
            else
            {
                var offer = await _models.getOffer(id, token.ID);
                offers = offer == null ? new FOffer[] { } : new FOffer[] { offer };
            }
            var documents = await _models.getDocumentRefs(id);
            return new Result<FNodeDetail>(new FNodeDetail(new FNodeModify(node, documents), notes, offers));
        }

        public async Task<Result<FNodeModify>> insertNode(FUserToken token, NodeData data, string[] documents)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FNodeModify>(false, null, "请重新登录");
            }

            if (data.CreatorGuid != token.ID)
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            if (!await _models.checkProjectModifyPartner(data.ProjectGuid, token.ID))
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            if (data.WorkerGuid != Guid.Empty)
            {
                if (!await _models.checkProjectModifyPartner(data.ProjectGuid, data.WorkerGuid))
                {
                    return new Result<FNodeModify>(false, null, "没有权限");
                }
            }

            if (data.isRoot() && data.RootGuid != Guid.Empty)
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            if (data.ParentGuid != Guid.Empty && !await _models.checkRoot(data.ParentGuid, data.RootGuid))
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            var tran = _models.Database.BeginTransaction();
            var id = await _models.insertNode(data);

            if (documents != null && documents.Length > 0)
            {
                var documentIds = await _models.addDocuments(documents.Select(d => new DocumentData(data.CreatorGuid, d, d.extension(false))).ToArray());
                await _models.addDocumentRefs(documentIds.Select(d => new DocumentRefData(d, id)).ToArray());
            }

            if (data.isRoot())
            {
                await _models.modifyNode(id, new NodeData() { RootGuid = Guid.Empty }, new NodeData() { RootGuid = id });
            }

            await tran.CommitAsync();

            var node = await _models.getNode(id);
            var docs = await _models.getDocumentRefs(id);

            return new Result<FNodeModify>(new FNodeModify(node, docs));
        }

        private bool __equals(FDocumentRef[] oldDocuments, string[] newDocuments)
        {
            if (oldDocuments.Length != newDocuments.Length)
            {
                return false;
            }
            if (oldDocuments.FirstOrDefault(d => !newDocuments.Contains(d.Document.Src)) != null)
            {
                return false;
            }
            return true;
        }

        public async Task<Result<FNodeModify>> modifyNode(FUserToken token, Guid id, NodeData oldData, NodeData newData, FDocumentRef[] oldDocuments, string[] newDocuments)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FNodeModify>(false, null, "请重新登录");
            }

            if (oldData.CreatorGuid != newData.CreatorGuid || oldData.ProjectGuid != newData.ProjectGuid)
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            if (oldData.EstimatedManHour != newData.EstimatedManHour && !await _models.checkNodeAdminPartner(id, token.ID))
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            if (!await _models.checkNodeModifyPartner(id, token.ID))
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            if (oldData.WorkerGuid != newData.WorkerGuid && newData.WorkerGuid != Guid.Empty && !await _models.checkNodeModifyPartner(id, newData.WorkerGuid))
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            if (oldData.ParentGuid != newData.ParentGuid)
            {
                if (oldData.ParentGuid != Guid.Empty && !await _models.checkNodeModifyPartner(oldData.ParentGuid, token.ID))
                {
                    return new Result<FNodeModify>(false, null, "没有权限");
                }
                if (newData.ParentGuid != Guid.Empty && !await _models.checkNodeModifyPartner(newData.ParentGuid, token.ID))
                {
                    return new Result<FNodeModify>(false, null, "没有权限");
                }
                if (newData.ParentGuid != Guid.Empty && !await _models.checkRoot(newData.ParentGuid, newData.RootGuid))
                {
                    return new Result<FNodeModify>(false, null, "没有权限");
                }
            }

            if (oldData.RootGuid != newData.RootGuid)
            {
                if (newData.isRoot() && newData.RootGuid != id)
                {
                    return new Result<FNodeModify>(false, null, "没有权限");
                }
                if (newData.ParentGuid != Guid.Empty && !await _models.checkRoot(newData.ParentGuid, newData.RootGuid))
                {
                    return new Result<FNodeModify>(false, null, "没有权限");
                }
            }

            if (oldData.Status != newData.Status)
            {
                if (newData.getStatus() == NodeStatus.进行中 && oldData.StartTime == newData.StartTime && newData.StartTime == null)
                {
                    newData.StartTime = DateTime.Now;
                }

                if (newData.getStatus() == NodeStatus.完成 && oldData.EndTime == newData.EndTime && newData.EndTime == null)
                {
                    newData.EndTime = DateTime.Now;
                }
            }

            if (oldDocuments != null && newDocuments != null && !__equals(oldDocuments, newDocuments))
            {
                var tran = _models.Database.BeginTransaction();
                await _models.modifyNode(id, oldData, newData);
                foreach (var d in oldDocuments)
                {
                    if (!newDocuments.Contains(d.Document.Src))
                    {
                        await _models.deleteDocumentRef(d.ID);
                    }
                }
                foreach (var d in newDocuments)
                {
                    if (oldDocuments.FirstOrDefault(o => o.Document.Src == d) == null)
                    {
                        var documentId = await _models.addDocument(new DocumentData(newData.CreatorGuid, d, d.extension(false)));
                        await _models.addDocumentRef(new DocumentRefData(documentId, id));
                    }
                }
                await tran.CommitAsync();
            }
            else
            {
                await _models.modifyNode(id, oldData, newData);
            }

            var node = await _models.getNode(id);
            var documents = await _models.getDocumentRefs(id);

            return new Result<FNodeModify>(new FNodeModify(node, documents));
        }

        public async Task<Result<bool>> deleteNode(FUserToken token, Guid id)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<bool>(false, false, "请重新登录");
            }

            if (!await _models.checkNodeModifyPartner(id, token.ID))
            {
                return new Result<bool>(false, false, "没有权限");
            }

            await _models.deleteNode(id);
            return new Result<bool>(true);
        }

        public async Task<Result<FNote>> getNote(FUserToken token, Guid id)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FNote>(false, null, "请重新登录");
            }

            if (!await _models.checkNotePartner(id, token.ID))
            {
                return new Result<FNote>(false, null, "没有权限");
            }

            return new Result<FNote>(await _models.getNote(id));
        }

        public async Task<Result<FNote>> insertNote(FUserToken token, NoteData data, string[] documents)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FNote>(false, null, "请重新登录");
            }

            if (data.CreatorGuid != token.ID)
            {
                return new Result<FNote>(false, null, "没有权限");
            }

            if (!await _models.checkNodeModifyPartner(data.NodeGuid, token.ID))
            {
                return new Result<FNote>(false, null, "没有权限");
            }

            var tran = _models.Database.BeginTransaction();
            var id = await _models.insertNote(data);

            if (documents != null && documents.Length > 0)
            {
                var documentIds = await _models.addDocuments(documents.Select(d => new DocumentData(data.CreatorGuid, d, d.extension(false))).ToArray());
                await _models.addDocumentRefs(documentIds.Select(d => new DocumentRefData(d, id)).ToArray());
            }

            await tran.CommitAsync();

            return new Result<FNote>(await _models.getNote(id));
        }

        public async Task<Result<FNote>> modifyNote(FUserToken token, Guid id, NoteData oldData, NoteData newData, FDocumentRef[] oldDocuments, string[] newDocuments)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FNote>(false, null, "请重新登录");
            }

            if (oldData.NodeGuid != newData.NodeGuid || oldData.CreatorGuid != newData.CreatorGuid)
            {
                return new Result<FNote>(false, null, "没有权限");
            }

            if (!await _models.checkNoteModifyPartner(id, token.ID))
            {
                return new Result<FNote>(false, null, "没有权限");
            }

            if (oldDocuments != null && newDocuments != null && !__equals(oldDocuments, newDocuments))
            {
                var tran = _models.Database.BeginTransaction();
                await _models.modifyNote(id, oldData, newData);
                foreach (var d in oldDocuments)
                {
                    if (!newDocuments.Contains(d.Document.Src))
                    {
                        await _models.deleteDocumentRef(d.ID);
                    }
                }
                foreach (var d in newDocuments)
                {
                    if (oldDocuments.FirstOrDefault(o => o.Document.Src == d) == null)
                    {
                        var documentId = await _models.addDocument(new DocumentData(newData.CreatorGuid, d, d.extension(false)));
                        await _models.addDocumentRef(new DocumentRefData(documentId, id));
                    }
                }
                await tran.CommitAsync();
            }
            else
            {
                await _models.modifyNote(id, oldData, newData);
            }

            return new Result<FNote>(await _models.getNote(id));
        }

        public async Task<Result<bool>> deleteNote(FUserToken token, Guid id)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<bool>(false, false, "请重新登录");
            }

            if (!await _models.checkNoteModifyPartner(id, token.ID))
            {
                return new Result<bool>(false, false, "没有权限");
            }
            await _models.deleteNote(id);
            return new Result<bool>(true);
        }

        public async Task<Result<FUser[]>> getUsers(FUserToken token, Guid userGuid, UserCondition condition)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FUser[]>(false, null, "请重新登录");
            }

            if (userGuid != token.ID)
            {
                return new Result<FUser[]>(false, null, "没有权限");
            }

            return new Result<FUser[]>(await _models.getUsers(userGuid, condition));
        }

        public async Task<Result<FPartner[]>> getPartnersOfProject(FUserToken token, Guid projectGuid, PartnerCondition condition)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FPartner[]>(false, null, "请重新登录");
            }

            if (!await _models.checkProjectPartner(projectGuid, token.ID))
            {
                return new Result<FPartner[]>(false, null, "没有权限");
            }

            return new Result<FPartner[]>(await _models.getPartnersOfProject(projectGuid, condition));
        }

        public async Task<Result<FPartner>> setPartner(FUserToken token, Guid projectGuid, Guid userGuid, PartnerRole role)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FPartner>(false, null, "请重新登录");
            }

            if (!await _models.checkProjectAdminPartner(projectGuid, token.ID))
            {
                return new Result<FPartner>(false, null, "没有权限");
            }

            var id = await _models.setPartner(projectGuid, userGuid, role);
            return new Result<FPartner>(await _models.getPartner(id));
        }

        public async Task<Result<bool>> deletePartner(FUserToken token, Guid id)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<bool>(false, false, "请重新登录");
            }

            if (!await _models.checkPartnerModifyPartner(id, token.ID))
            {
                return new Result<bool>(false, false, "没有权限");
            }

            await _models.deletePartner(id);
            return new Result<bool>(true);
        }

        public async Task<Result<FOffer>> offerToNode(FUserToken token, Guid nodeGuid, Guid userGuid, decimal estimatedManHour)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FOffer>(false, null, "请重新登录");
            }

            if (token.ID != userGuid || !await _models.checkNodeModifyPartner(nodeGuid, token.ID))
            {
                return new Result<FOffer>(false, null, "没有权限");
            }

            var id = await _models.offerToNode(nodeGuid, userGuid, estimatedManHour);
            return new Result<FOffer>(await _models.getOffer(id));
        }
    }
}
