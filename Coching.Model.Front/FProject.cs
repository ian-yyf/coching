using Coching.Model.Data;
using Public.Model.Front;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FProject : ProjectData
    {
        public FProject()
        {

        }

        public FProject(Guid id, ProjectData data, FUser creator, FPartner[] partners)
            : base(data)
        {
            ID = id;
            Creator = creator;
            Partners = partners;
        }

        public Guid ID { get; set; }
        public FUser Creator { get; set; }
        public FPartner[] Partners { get; set; }
    }
}
