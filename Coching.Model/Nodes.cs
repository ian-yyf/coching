﻿using Coching.Model.Data;
using Public.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coching.Model
{
    [Table("Nodes")]
    public class Nodes : NodeData, IKeyDeleted
    {
        public Nodes()
        {

        }

        public Nodes(NodeData data)
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
