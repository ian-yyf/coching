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

        public FNodeDetail(FNode node, FNote[] notes, FOffer[] offers)
        {
            Node = node;
            Notes = notes;
            Offers = offers;
        }

        public FNode Node { get; set; }
        public FNote[] Notes { get; set; }
        public FOffer[] Offers { get; set; }
    }
}
