using Coching.Model.Data;
using Public.Model.Front;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FOffer : OfferData
    {
        public FOffer()
        {

        }

        public FOffer(Guid id, OfferData data, FUser user)
            : base(data)
        {
            ID = id;
            User = user;
        }

        public Guid ID { get; set; }
        public FUser User { get; set; }
    }
}
