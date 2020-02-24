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

        public FCoching(FUser user, decimal coching, decimal today)
        {
            User = user;
            Coching = coching;
            Today = today;
        }

        public FUser User { get; set; }
        public decimal Coching { get; set; }
        public decimal Today { get; set; }
        public decimal History
        {
            get
            {
                return Coching - Today;
            }
        }
    }
}
