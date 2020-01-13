using Coching.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FPartner : PartnerData
    {
        public FPartner()
        {

        }

        public FPartner(Guid id, PartnerData data)
            : base(data)
        {
            ID = id;
        }

        public Guid ID { get; set; }
    }
}
