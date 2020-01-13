using Coching.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FNodeDetail : FNode
    {
        public FNodeDetail()
        {

        }

        public FNodeDetail(Guid id, NodeData data)
            : base(id, data)
        {

        }

        public FNote Notes { get; set; }
    }
}
