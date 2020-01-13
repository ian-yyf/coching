using Coching.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FNote : NoteData
    {
        public FNote()
        {

        }

        public FNote(Guid id, NoteData data)
            : base(data)
        {
            ID = id;
        }

        public Guid ID { get; set; }
    }
}
