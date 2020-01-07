using Coching.Model.Data;
using System;

namespace Coching.Model.Front
{
    public class FNode : NodeData
    {
        public FNode()
        {

        }

        public FNode(Guid id, NodeData data)
            : base(data)
        {
            ID = id;
        }

        public Guid ID { get; set; }
        public FNode[] Children { get; set; }
    }
}
