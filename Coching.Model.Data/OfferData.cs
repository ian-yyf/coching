using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coching.Model.Data
{
    public class OfferData
    {
        public OfferData()
        {

        }

        public OfferData(Guid nodeGuid, Guid userGuid, decimal estimatedManHour)
        {
            NodeGuid = nodeGuid;
            UserGuid = userGuid;
            EstimatedManHour = estimatedManHour;
        }

        public OfferData(OfferData rhs)
        {
            NodeGuid = rhs.NodeGuid;
            UserGuid = rhs.UserGuid;
            EstimatedManHour = rhs.EstimatedManHour;
        }

        public Guid NodeGuid { get; set; }
        public Guid UserGuid { get; set; }
        [Column(TypeName = "decimal(10, 1)")]
        public decimal EstimatedManHour { get; set; }

        public string EstimatedTime
        {
            get
            {
                return $"{EstimatedManHour.ToString("0.#")}工时";
            }
        }
    }
}
