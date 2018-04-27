using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexasHoldEm.Exceptions
{
    class RiverSizeException : Exception
    {
        public RiverSizeException(int nbrCards) :
            base(String.Format("River already has {0} card(s)", nbrCards))
        { }
    }
}
