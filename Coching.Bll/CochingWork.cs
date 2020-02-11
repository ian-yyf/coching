using App.Utils;
using Coching.Dal;
using Coching.Model.Data;
using Coching.Model.Front;
using Public.Containers;
using Public.Model.Data;
using Public.Model.Front;
using Public.Utils;
using System;
using System.Collections.Generic;
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

            var link = new ActionLogLink("Index", "Coching", new {
                projectGuid = id
            }.toKeyValueArray(), data.Name);
            await _models.addActionLog(new ActionLogData(id, token.ID, ActionLogKind.创建项目, $"创建了项目 {link.toString()}"), true);

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

            var link = new ActionLogLink("Index", "Coching", new
            {
                projectGuid = id
            }.toKeyValueArray(), newData.Name);
            await _models.addActionLog(new ActionLogData(id, token.ID, ActionLogKind.修改项目, $"修改了项目 {link.toString()}"), false);
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

            var project = await _models.getProject(id);
            await _models.addActionLog(new ActionLogData(id, token.ID, ActionLogKind.删除项目, $"删除了项目 {project.Name} "), false);
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

            if (data.WorkerGuid != Guid.Empty && !await _models.checkProjectModifyPartner(data.ProjectGuid, data.WorkerGuid))
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            if (data.isRoot() && data.RootGuid != Guid.Empty)
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            if (data.ParentGuid != Guid.Empty && !await _models.checkRoot(data.ParentGuid, data.RootGuid))
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            if ((data.EstimatedManHour > 0 || data.Coching) && !await _models.checkProjectAdminPartner(data.ProjectGuid, token.ID))
            {
                return new Result<FNodeModify>(false, null, "没有权限");
            }

            if (data.Coching && await _models.checkCochingUp(data.ParentGuid))
            {
                return new Result<FNodeModify>(false, null, "一条支线上只能有一个考成项");
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
                data.RootGuid = id;
            }

            var link = new ActionLogLink("Index", "Coching", new
            {
                projectGuid = data.ProjectGuid,
                rootGuid = data.RootGuid
            }.toKeyValueArray(), data.Name);
            await _models.addActionLog(new ActionLogData(data.ProjectGuid, token.ID, ActionLogKind.添加分支, $"添加了{(data.isRoot() ? "任务" : "分支")} {link.toString()}"), true);

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

            FNode _dbData = null;
            Func<Task<FNode>> dbData = async () =>
            {
                if (_dbData == null)
                {
                    _dbData = await _models.getNode(id);
                }
                return _dbData;
            };

            Func<Task<DateTime?>> finalStartTime = async () =>
            {
                if (oldData.StartTime != newData.StartTime)
                {
                    return newData.StartTime;
                }
                return (await dbData()).StartTime;
            };

            Func<Task<DateTime?>> finalEndTime = async () =>
            {
                if (oldData.EndTime != newData.EndTime)
                {
                    return newData.EndTime;
                }
                return (await dbData()).EndTime;
            };

            Func<Task<NodeStatus>> finalStatus = async () =>
            {
                if (oldData.Status != newData.Status)
                {
                    return newData.getStatus();
                }
                return (await dbData()).getStatus();
            };

            Func<Task<decimal>> finalEstimatedManHour = async () =>
            {
                if (oldData.EstimatedManHour != newData.EstimatedManHour)
                {
                    return newData.EstimatedManHour;
                }
                return (await dbData()).EstimatedManHour;
            };

            Func<Task<bool>> finalCoching = async () =>
            {
                if (oldData.Coching != newData.Coching)
                {
                    return newData.Coching;
                }
                return (await dbData()).Coching;
            };

            Func<Task<Guid>> finalWorker = async () =>
            {
                if (oldData.WorkerGuid != newData.WorkerGuid)
                {
                    return newData.WorkerGuid;
                }
                return (await dbData()).WorkerGuid;
            };

            if (oldData.EstimatedManHour != newData.EstimatedManHour)
            {
                if (await finalWorker() != token.ID && !await _models.checkNodeAdminPartner(id, token.ID))
                {
                    return new Result<FNodeModify>(false, null, "没有权限");
                }
            }

            if (oldData.Coching != newData.Coching)
            {
                if (!await _models.checkNodeAdminPartner(id, token.ID))
                {
                    return new Result<FNodeModify>(false, null, "没有权限");
                }

                if (newData.Coching)
                {
                    if (await _models.checkCochingUp((await dbData()).ParentGuid) || await _models.checkCochingChildren(new Guid[] { id }))
                    {
                        return new Result<FNodeModify>(false, null, "一条支线上只能有一个考成项");
                    }
                }
            }

            if (oldData.Status != newData.Status)
            {
                if (newData.getStatus() == NodeStatus.进行中 && oldData.StartTime == newData.StartTime && await finalStartTime() == null)
                {
                    newData.StartTime = DateTime.Now;
                }

                if (newData.getStatus() == NodeStatus.完成 && oldData.EndTime == newData.EndTime && await finalEndTime() == null)
                {
                    newData.EndTime = DateTime.Now;
                }
            }

            if (oldData.ActualManHour == newData.ActualManHour)
            {
                if (oldData.Status != newData.Status
                    || oldData.StartTime != newData.StartTime
                    || oldData.EndTime != newData.EndTime)
                {
                    if (await finalStatus() == NodeStatus.完成 && await finalStartTime() != null && await finalEndTime() != null)
                    {
                        if (await _models.checkLeaf(id))
                        {
                            oldData.ActualManHour = (await dbData()).ActualManHour;
                            newData.ActualManHour = (decimal)Math.Round(((await finalEndTime()).Value - (await finalStartTime()).Value).TotalHours, 1);
                        }
                    }
                }
            }

            var tran = _models.Database.BeginTransaction();

            if (oldData.ActualManHour != newData.ActualManHour)
            {
                await _models.addActualManHour((await dbData()).ParentGuid, newData.ActualManHour - oldData.ActualManHour);
            }

            if (oldData.Coching != newData.Coching 
                || oldData.Status != newData.Status && (oldData.getStatus() == NodeStatus.完成 || newData.getStatus() == NodeStatus.完成)
                || oldData.WorkerGuid != newData.WorkerGuid 
                || oldData.EstimatedManHour != newData.EstimatedManHour)
            {
                var db = await dbData();
                var oriCoching = db.Coching && db.getStatus() == NodeStatus.完成 && db.WorkerGuid != Guid.Empty ? db.EstimatedManHour : 0;
                var newCoching = await finalCoching() && await finalStatus() == NodeStatus.完成 && await finalWorker() != Guid.Empty ? await finalEstimatedManHour() : 0;

                if (oldData.WorkerGuid != newData.WorkerGuid)
                {
                    if (oldData.WorkerGuid != Guid.Empty)
                    {
                        await _models.addCoching(db.ProjectGuid, oldData.WorkerGuid, -oriCoching);
                    }
                    if (newData.WorkerGuid != Guid.Empty)
                    {
                        await _models.addCoching(db.ProjectGuid, newData.WorkerGuid, newCoching);
                    }
                }
                else if (await finalWorker() != Guid.Empty)
                {
                    await _models.addCoching(db.ProjectGuid, await finalWorker(), newCoching - oriCoching);
                }
            }

            await _models.modifyNode(id, oldData, newData);

            if (oldDocuments != null && newDocuments != null && !__equals(oldDocuments, newDocuments))
            {
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
            }

            var link = new ActionLogLink("Index", "Coching", new
            {
                projectGuid = (await dbData()).ProjectGuid,
                rootGuid = (await dbData()).RootGuid
            }.toKeyValueArray(), (await dbData()).Name);
            await _models.addActionLog(new ActionLogData((await dbData()).ProjectGuid, token.ID, ActionLogKind.修改分支, $"修改了{((await dbData()).isRoot() ? "任务" : "分支")} {link.toString()}"), true);
            if (oldData.WorkerGuid != newData.WorkerGuid)
            {
                var user = await _models.getUser(newData.WorkerGuid);
                await _models.addActionLog(new ActionLogData((await dbData()).ProjectGuid, token.ID, ActionLogKind.改变分支执行人, $"修改{((await dbData()).isRoot() ? "任务" : "分支")} {link.toString()} 执行人为 {(user == null ? "无" : user.Name)}"), true);
            }
            if (oldData.Status != newData.Status)
            {
                await _models.addActionLog(new ActionLogData((await dbData()).ProjectGuid, token.ID, ActionLogKind.修改分支状态, $"修改{((await dbData()).isRoot() ? "任务" : "分支")} {link.toString()} 状态到 {newData.StatusTitle}"), true);
            }
            if (oldData.EstimatedManHour != newData.EstimatedManHour)
            {
                if (newData.EstimatedManHour == 0)
                {
                    await _models.addActionLog(new ActionLogData((await dbData()).ProjectGuid, token.ID, ActionLogKind.确定预估工时, $"把{((await dbData()).isRoot() ? "任务" : "分支")} {link.toString()} 预估工时改为未确定"), true);
                }
                else
                {
                    await _models.addActionLog(new ActionLogData((await dbData()).ProjectGuid, token.ID, ActionLogKind.确定预估工时, $"确定{((await dbData()).isRoot() ? "任务" : "分支")} {link.toString()} 预估工时为 {newData.EstimatedTime}"), true);
                }
            }
            if (oldData.ActualManHour != newData.ActualManHour)
            {
                if (newData.ActualManHour == 0)
                {
                    await _models.addActionLog(new ActionLogData((await dbData()).ProjectGuid, token.ID, ActionLogKind.修改实际工时, $"把{((await dbData()).isRoot() ? "任务" : "分支")} {link.toString()} 实际工时改为未确定"), true);
                }
                else
                {
                    var db = await dbData();
                    db.ActualManHour = newData.ActualManHour;
                    await _models.addActionLog(new ActionLogData((await dbData()).ProjectGuid, token.ID, ActionLogKind.修改实际工时, $"修改{((await dbData()).isRoot() ? "任务" : "分支")} {link.toString()} 实际工时为 {newData.ActualTime} 任务执行结果：{db.TimeInfo}"), true);
                }
            }

            await tran.CommitAsync();

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

            var node = await _models.getNode(id);

            if (node.Coching && node.WorkerGuid != Guid.Empty && node.EstimatedManHour != 0)
            {
                return new Result<bool>(false, false, "本项已经计入考成，不可删除，可以先解除本项【考成项】状态");
            }

            await _models.addActionLog(new ActionLogData(node.ProjectGuid, token.ID, ActionLogKind.修改分支, $"删除了{(node.isRoot() ? "任务" : "分支")} {node.Name}"), false);
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

            var node = await _models.getNode(data.NodeGuid);
            var link = new ActionLogLink("Index", "Coching", new
            {
                projectGuid = node.ProjectGuid,
                rootGuid = node.RootGuid
            }.toKeyValueArray(), node.Name);
            await _models.addActionLog(new ActionLogData(node.ProjectGuid, token.ID, ActionLogKind.添加批注, $"为{(node.isRoot() ? "任务" : "分支")} {link.toString()} 添加了批注"), true);

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

            var node = await _models.getNode(newData.NodeGuid);
            var link = new ActionLogLink("Index", "Coching", new
            {
                projectGuid = node.ProjectGuid,
                rootGuid = node.RootGuid
            }.toKeyValueArray(), node.Name);

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
                await _models.addActionLog(new ActionLogData(node.ProjectGuid, token.ID, ActionLogKind.修改批注, $"修改了{(node.isRoot() ? "任务" : "分支")} {link.toString()} 的批注"), true);
                await tran.CommitAsync();
            }
            else
            {
                await _models.addActionLog(new ActionLogData(node.ProjectGuid, token.ID, ActionLogKind.修改批注, $"修改了{(node.isRoot() ? "任务" : "分支")} {link.toString()} 的批注"), false);
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

            var note = await _models.getNote(id);
            var node = await _models.getNode(note.NodeGuid);
            var link = new ActionLogLink("Index", "Coching", new
            {
                projectGuid = node.ProjectGuid,
                rootGuid = node.RootGuid
            }.toKeyValueArray(), node.Name);
            await _models.addActionLog(new ActionLogData(node.ProjectGuid, token.ID, ActionLogKind.删除批注, $"删除了{(node.isRoot() ? "任务" : "分支")} {link.toString()} 的批注"), false);

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

        public async Task<Result<FPartner>> getPartnerOfProject(FUserToken token, Guid projectGuid, Guid userGuid)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FPartner>(false, null, "请重新登录");
            }

            if (token.ID != userGuid)
            {
                return new Result<FPartner>(false, null, "没有权限");
            }

            return new Result<FPartner>(await _models.getPartnerOfProject(projectGuid, userGuid));
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

        public async Task<Result<FActionLog[]>> getActionLogsOfUser(FUserToken token, Guid userGuid, int pageSize, int pageIndex)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FActionLog[]>(false, null, "请重新登录");
            }

            if (token.ID != userGuid)
            {
                return new Result<FActionLog[]>(false, null, "没有权限");
            }

            return new Result<FActionLog[]>(await _models.getActionLogsOfUser(userGuid, pageSize, pageIndex));
        }

        public async Task<Result<FPartner[]>> charts(FUserToken token, Guid[] projectGuids)
        {
            if (!await _models.checkToken(token.ID, token.Token))
            {
                return new Result<FPartner[]>(false, null, "请重新登录");
            }

            if (!await _models.checkProjectsPartner(projectGuids, token.ID))
            {
                return new Result<FPartner[]>(false, null, "没有权限");
            }

            return new Result<FPartner[]>(await _models.charts(projectGuids));
        }
    }
}
