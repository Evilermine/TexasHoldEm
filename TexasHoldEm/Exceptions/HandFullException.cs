using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexasHoldEm.Exceptions
{
    public class HandFullException : Exception
    {
        public HandFullException(int nbrCards) : base(String.Format("Player already has {0} card(s)",nbrCards)) { }
    }
}
