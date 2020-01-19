using Public.Model.Front;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FRoot
    {
        public FRoot()
        {

        }

        public FRoot(FNode node, Guid[] workers)
        {
            Node = node;
            Workers = workers;
        }

        public FNode Node { get; set; }
        public Guid[] Workers { get; set; }
    }
}
