using Coching.Model.Data;
using Public.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coching.Model
{
    [Table("ActionLogs")]
    public class ActionLogs : ActionLogData, IKeyDeleted
    {
        public ActionLogs()
        {

        }

        public ActionLogs(ActionLogData data)
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
