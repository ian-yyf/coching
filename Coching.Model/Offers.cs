using Coching.Model.Data;
using Public.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coching.Model
{
    [Table("Offers")]
    public class Offers : OfferData, IKeyDeleted
    {
        public Offers()
        {

        }

        public Offers(OfferData data)
            : base(data)
        {
            KeyGuid = Guid.NewGuid();
            Deleted = false;
        }

        [Key]
        public Guid KeyGuid { get; set; }
        public bool Deleted { get; set; }
    }
}
