using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coching.Model.Data
{
    public class PartnerData
    {
        public PartnerData()
        {

        }

        public PartnerData(Guid projectGuid, Guid userGuid, PartnerRole role)
        {
            ProjectGuid = projectGuid;
            UserGuid = userGuid;
            Role = (int)role;
            Coching = 0;
            JoinTime = DateTime.Now;
        }

        public PartnerData(PartnerData rhs)
        {
            ProjectGuid = rhs.ProjectGuid;
            UserGuid = rhs.UserGuid;
            Role = rhs.Role;
            Coching = rhs.Coching;
            JoinTime = rhs.JoinTime;
        }

        public Guid ProjectGuid { get; set; }
        public Guid UserGuid { get; set; }
        public int Role { get; set; }
        [Column(TypeName = "decimal(10, 1)")]
        public decimal Coching { get; set; }
        public DateTime JoinTime { get; set; }
        
        public PartnerRole getRole()
        {
            return (PartnerRole)Role;
        }

        public string RoleTitle
        {
            get
            {
                return getRole().ToString();
            }
        }

        public bool IsAdmin
        {
            get
            {
                return getRole() == PartnerRole.管理员;
            }
        }
    }
}
