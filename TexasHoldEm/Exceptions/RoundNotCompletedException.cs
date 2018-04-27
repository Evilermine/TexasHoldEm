using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexasHoldEm.Exceptions
{
    public class RoundNotCompletedException : Exception
    {
        public RoundNotCompletedException(int riverCount) : 
            base(String.Format("Incomplete river: {0} card(s)", riverCount))
        { }
    }
}
