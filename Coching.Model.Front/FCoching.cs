using Public.Model.Front;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Front
{
    public class FCoching
    {
        public FCoching()
        {

        }

        public FCoching(FUser user, decimal coching)
        {
            User = user;
            Coching = coching;
        }

        public FUser User { get; set; }
        public decimal Coching { get; set; }
    }
}
