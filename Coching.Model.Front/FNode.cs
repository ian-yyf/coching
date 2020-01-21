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

        public FNode(FNode rhs)
            : base(rhs)
        {
            ID = rhs.ID;
            Creator = rhs.Creator;
            Worker = rhs.Worker;
            Children = rhs.Children;
        }

        public Guid ID { get; set; }
        public FUser Creator { get; set; }
        public FUser Worker { get; set; }
        public FNode[] Children { get; set; }
    }
}
