using Coching.Model.Data;
using Public.Model.Front;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FActionLog : ActionLogData
    {
        public FActionLog()
        {

        }

        public FActionLog(Guid id, ActionLogData data, FUser user)
            : base(data)
        {
            ID = id;
            User = user;
        }

        public Guid ID { get; set; }
        public FUser User { get; set; }
    }
}
