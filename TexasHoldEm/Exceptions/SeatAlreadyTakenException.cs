using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexasHoldEm.Exceptions
{
    public class SeatAlreadyTakenException : Exception
    {
        public SeatAlreadyTakenException(String playerUsername) : 
            base(String.Format("Seat already taken by {0}", playerUsername)) { }
    }
}
