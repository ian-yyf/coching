using Coching.Model.Data;
using Public.Model.Front;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FNodeModify : FNode
    {
        public FNodeModify()
        {

        }

        public FNodeModify(FNode node, FDocumentRef[] documents)
            : base(node)
        {
            Documents = documents;
        }

        public FDocumentRef[] Documents { get; set; }
    }
}
