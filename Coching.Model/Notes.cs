using Coching.Model.Data;
using Public.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coching.Model
{
    [Table("Notes")]
    public class Notes : NoteData, IKeyDeleted
    {
        public Notes()
        {

        }

        public Notes(NoteData data)
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
