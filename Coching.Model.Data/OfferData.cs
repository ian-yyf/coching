using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Data
{
    public class OfferData
    {
        public OfferData()
        {

        }

        public OfferData(Guid nodeGuid, Guid userGuid, int totalMinutes)
        {
            NodeGuid = nodeGuid;
            UserGuid = userGuid;
            TotalMinutes = totalMinutes;
        }

        public OfferData(OfferData rhs)
        {
            NodeGuid = rhs.NodeGuid;
            UserGuid = rhs.UserGuid;
            TotalMinutes = rhs.TotalMinutes;
        }

        public Guid NodeGuid { get; set; }
        public Guid UserGuid { get; set; }
        public int TotalMinutes { get; set; }

        public string TotalTime
        {
            get
            {
                return $"{(int)Math.Floor(TotalMinutes / 60.0)}小时{TotalMinutes % 60}分钟";
            }
        }
    }
}
