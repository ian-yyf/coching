using Coching.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FNodeDetail : FNodeInfo
    {
        public FNodeDetail()
        {

        }

        public FNodeDetail(FNode node, FNote[] notes, FPartner[] partners, FOffer[] offers)
            : base(node, partners)
        {
            Node = node;
            Notes = notes;
            Partners = partners;
            Offers = offers;
        }

        public FNote[] Notes { get; set; }
        public FOffer[] Offers { get; set; }
    }
}
