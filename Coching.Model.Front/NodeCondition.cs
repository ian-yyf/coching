using Coching.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class NodeCondition
    {
        public Guid? WorkerGuid { get; set; }
        public NodeStatus? Status { get; set; }
        public bool? Coching { get; set; }
    }
}
