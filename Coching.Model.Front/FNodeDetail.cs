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

        public FNodeDetail(FNode node, FNote[] notes)
        {
            Node = node;
            Notes = notes;
        }

        public FNode Node { get; set; }
        public FNote[] Notes { get; set; }
    }
}
