using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Data
{
    public class PartnerData
    {
        public PartnerData()
        {

        }

        public PartnerData(Guid nodeGuid, Guid userGuid, PartnerRole role)
        {
            NodeGuid = nodeGuid;
            UserGuid = userGuid;
            Role = (int)role;
            JoinTime = DateTime.Now;
        }

        public PartnerData(PartnerData rhs)
        {
            NodeGuid = rhs.NodeGuid;
            UserGuid = rhs.UserGuid;
            Role = rhs.Role;
            JoinTime = rhs.JoinTime;
        }

        public Guid NodeGuid { get; set; }
        public Guid UserGuid { get; set; }
        public int Role { get; set; }
        public DateTime JoinTime { get; set; }
        
        public PartnerRole getRole()
        {
            return (PartnerRole)Role;
        }
    }
}
