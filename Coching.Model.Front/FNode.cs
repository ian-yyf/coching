using Coching.Model.Data;
using Public.Model.Front;
using System;
using System.Linq;

namespace Coching.Model.Front
{
    public class FNode : NodeData
    {
        public FNode()
        {

        }

        public FNode(Guid id, NodeData data, FUser creator, FUser worker)
            : base(data)
        {
            ID = id;
            Creator = creator;
            Worker = worker;
        }

        public Guid ID { get; set; }
        public FUser Creator { get; set; }
        public FUser Worker { get; set; }
        public FNode[] Children { get; set; }

        public static dynamic[] toTreeData(FNode[] items)
        {
            return items.Select(i =>
            {
                return new
                {
                    id = i.ID,
                    root = i.RootGuid,
                    parent = i.ParentGuid,
                    name = i.Name,
                    label = i.getLabel(),
                    description = i.Description,
                    creator = new
                    {
                        id = i.Creator.ID,
                        name = i.Creator.Name,
                        header = i.Creator.Header
                    },
                    worker = i.Worker == null ? null : new
                    {
                        id = i.Worker.ID,
                        name = i.Worker.Name,
                        header = i.Worker.Header
                    },
                    status = new
                    {
                        value = i.Status,
                        title = i.getStatus().ToString(),
                        color = "#62b7f4"
                    },
                    children = toTreeData(i.Children ?? new FNode[] { }),
                    collapsed = false
                };
            }).ToArray();
        }
    }
}
