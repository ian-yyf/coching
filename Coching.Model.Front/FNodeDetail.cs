using Coching.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FNodeDetail
    {
        public FNodeDetail()
        {

        }

        public FNodeDetail(FNode node, FNote[] notes, FPartner[] partners)
        {
            Node = node;
            Notes = notes;
            Partners = partners;
        }

        public FNode Node { get; set; }
        public FNote[] Notes { get; set; }
        public FPartner[] Partners { get; set; }
    }
}
