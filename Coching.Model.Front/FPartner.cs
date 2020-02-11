using Coching.Model.Data;
using Public.Model.Front;
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

        public FPartner(Guid id, PartnerData data, FUser user)
            : base(data)
        {
            ID = id;
            User = user;
        }

        public FPartner(Guid id, PartnerData data, FUser user, decimal coching)
            : this(id, data, user)
        {
            Coching = coching;
        }

        public Guid ID { get; set; }
        public FUser User { get; set; }
    }
}
