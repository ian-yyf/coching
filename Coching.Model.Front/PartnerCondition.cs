using Coching.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class PartnerCondition
    {
        public PartnerCondition()
        {

        }

        public PartnerCondition(PartnerRole[] roles)
        {
            Roles = roles;
        }

        public PartnerRole[] Roles { get; set; }

        public static PartnerCondition modify()
        {
            return new PartnerCondition(new PartnerRole[] { PartnerRole.管理员, PartnerRole.执行者 });
        }
    }
}
