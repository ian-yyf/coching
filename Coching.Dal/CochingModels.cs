using Coching.Model;
using Microsoft.EntityFrameworkCore;
using Public.Dal;
using System;

namespace Coching.Dal
{
    public class CochingModels : Models
    {
        public CochingModels(DbContextOptions<CochingModels> options)
            : base(options)
        {

        }

        public virtual DbSet<Nodes> NodesTable { get; set; }
    }
}
