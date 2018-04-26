using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexasHoldEm.Exceptions
{
    public class DeckSizeException : Exception
    {
        public DeckSizeException(int cards) : 
            base(String.Format("Deck has invalid card number : {0}", cards))
        {
            
        }
    }
}
