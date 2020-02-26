using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FTask
    {
        public FTask()
        {

        }

        public FTask(FProject project, FNode root, FNode node)
        {
            Project = project;
            Root = root;
            Node = node;
        }

        public FProject Project { get; set; }
        public FNode Root { get; set; }
        public FNode Node { get; set; }
    }
}
