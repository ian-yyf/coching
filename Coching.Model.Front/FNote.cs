﻿using Coching.Model.Data;
using Public.Model.Front;
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

        public FNote(Guid id, NoteData data, FUser user, FDocumentRef[] documents)
            : base(data)
        {
            ID = id;
            User = user;
            Documents = documents;
        }

        public Guid ID { get; set; }
        public FUser User { get; set; }
        public FDocumentRef[] Documents { get; set; }
    }
}
