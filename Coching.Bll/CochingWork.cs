using Coching.Dal;
using Coching.Model.Data;
using Coching.Model.Front;
using Public.Containers;
using Public.Model.Front;
using System;
using System.Threading.Tasks;

namespace Coching.Bll
{
    public class CochingWork : _Work
    {
        public CochingWork(CochingModels models)
            : base(models)
        {

        }

        public async Task<Result<Page<FNodeInfo>>> getUserRoots(FUserToken token, Guid userGuid, NodeCondition condition, int pageSize, int pageIndex)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<Page<FNodeInfo>>(false, null, "请重新登录");
            }

            if (token.ID != userGuid)
            {
                return new Result<Page<FNodeInfo>>(false, null, "没有权限");
            }

            return new Result<Page<FNodeInfo>>(await _models.getUserRoots(userGuid, condition, pageSize, pageIndex));
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
            var partners = await _models.getPartnersOfNodeByRootGuid(node.RootGuid);
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

            return new Result<FNodeDetail>(new FNodeDetail(node, notes, partners, offers));
        }

        public async Task<Result<FNodeInfo>> insertNode(FUserToken token, NodeData data)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FNodeInfo>(false, null, "请重新登录");
            }

            if (data.CreatorGuid != token.ID)
            {
                return new Result<FNodeInfo>(false, null, "没有权限");
            }

            if (data.WorkerGuid != null)
            {
                if (data.isRoot() && data.WorkerGuid != token.ID)
                {
                    return new Result<FNodeInfo>(false, null, "没有权限");
                }
                if (data.ParentGuid != Guid.Empty && !await _models.checkNodeModifyPartner(data.ParentGuid, data.WorkerGuid.Value))
                {
                    return new Result<FNodeInfo>(false, null, "没有权限");
                }
            }

            if (data.ParentGuid != Guid.Empty && !await _models.checkNodeModifyPartner(data.ParentGuid, token.ID))
            {
                return new Result<FNodeInfo>(false, null, "没有权限");
            }

            if (data.isRoot() && data.RootGuid != Guid.Empty)
            {
                return new Result<FNodeInfo>(false, null, "没有权限");
            }

            if (data.ParentGuid != Guid.Empty && !await _models.checkRoot(data.ParentGuid, data.RootGuid))
            {
                return new Result<FNodeInfo>(false, null, "没有权限");
            }

            Guid id;
            if (data.isRoot())
            {
                var tran = _models.Database.BeginTransaction();
                id = await _models.insertNode(data);
                await _models.insertPartner(new PartnerData(id, data.CreatorGuid, PartnerRole.管理员));
                await _models.modifyNode(id, new NodeData() { RootGuid = Guid.Empty }, new NodeData() { RootGuid = id });
                await tran.CommitAsync();
            }
            else
            {
                id = await _models.insertNode(data);
            }

            var node = await _models.getNode(id);
            var partners = await _models.getPartnersOfNodeByRootGuid(node.RootGuid);
            return new Result<FNodeInfo>(new FNodeInfo(node, partners));
        }

        public async Task<Result<FNodeInfo>> modifyNode(FUserToken token, Guid id, NodeData oldData, NodeData newData)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FNodeInfo>(false, null, "请重新登录");
            }

            if (oldData.CreatorGuid != newData.CreatorGuid)
            {
                return new Result<FNodeInfo>(false, null, "没有权限");
            }

            if (oldData.WorkerGuid != newData.WorkerGuid && newData.WorkerGuid != null && !await _models.checkNodeModifyPartner(id, newData.WorkerGuid.Value))
            {
                return new Result<FNodeInfo>(false, null, "没有权限");
            }

            if (oldData.ParentGuid != newData.ParentGuid)
            {
                if (oldData.ParentGuid != Guid.Empty && !await _models.checkNodeModifyPartner(oldData.ParentGuid, token.ID))
                {
                    return new Result<FNodeInfo>(false, null, "没有权限");
                }
                if (newData.ParentGuid != Guid.Empty && !await _models.checkNodeModifyPartner(newData.ParentGuid, token.ID))
                {
                    return new Result<FNodeInfo>(false, null, "没有权限");
                }
            }

            if (oldData.RootGuid != newData.RootGuid)
            {
                if (newData.isRoot() && newData.RootGuid != id)
                {
                    return new Result<FNodeInfo>(false, null, "没有权限");
                }
                if (newData.ParentGuid != Guid.Empty && !await _models.checkRoot(newData.ParentGuid, newData.RootGuid))
                {
                    return new Result<FNodeInfo>(false, null, "没有权限");
                }
            }

            if (!await _models.checkNodeModifyPartner(id, token.ID))
            {
                return new Result<FNodeInfo>(false, null, "没有权限");
            }

            await _models.modifyNode(id, oldData, newData);

            var node = await _models.getNode(id);
            var partners = await _models.getPartnersOfNodeByRootGuid(node.RootGuid);
            return new Result<FNodeInfo>(new FNodeInfo(node, partners));
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

        public async Task<Result<FNote>> insertNote(FUserToken token, NoteData data)
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

            var id = await _models.insertNote(data);
            return new Result<FNote>(await _models.getNote(id));
        }

        public async Task<Result<FNote>> modifyNote(FUserToken token, Guid id, NoteData oldData, NoteData newData)
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

        public async Task<Result<FPartner>> insertPartner(FUserToken token, PartnerData data)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FPartner>(false, null, "请重新登录");
            }

            if (!await _models.checkNodeAdminPartner(data.NodeGuid, token.ID))
            {
                return new Result<FPartner>(false, null, "没有权限");
            }

            if (await _models.checkNodePartner(data.NodeGuid, data.UserGuid))
            {
                return new Result<FPartner>(false, null, "此成员已经存在");
            }

            var id = await _models.insertPartner(data);
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

        public async Task<Result<FOffer>> offerToNode(FUserToken token, Guid nodeGuid, Guid userGuid, int totalMinutes)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FOffer>(false, null, "请重新登录");
            }

            if (token.ID != userGuid || !await _models.checkNodeModifyPartner(nodeGuid, token.ID))
            {
                return new Result<FOffer>(false, null, "没有权限");
            }

            var id = await _models.offerToNode(nodeGuid, userGuid, totalMinutes);
            return new Result<FOffer>(await _models.getOffer(id));
        }
    }
}
