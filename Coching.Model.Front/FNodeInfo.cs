using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FNodeInfo
    {
        public FNodeInfo()
        {

        }

        public FNodeInfo(FNode node, FPartner[] partners)
        {
            Node = node;
            Partners = partners;
        }

        public FNode Node { get; set; }
        public FPartner[] Partners { get; set; }
    }
}
