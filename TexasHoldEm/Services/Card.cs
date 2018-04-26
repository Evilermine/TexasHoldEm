using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TexasHoldEm.Services
{
    public class Card
    {
        public enum Color { SPADE, HEART, DIAMOND, CLUB };
        public enum Value { ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE , TEN, JACK, QUEEN, KING };

        private static readonly String[] COLOR_NAME = { "SPADE", "HEART", "DIAMOND", "CLUB" };
        private static readonly String[] VALUE_NAME = { "ACE", "TWO", "THREE",
            "FOUR", "FIVE", "SIX",
            "SEVEN", "EIGHT", "NINE",
            "TEN", "JACK", "QUEEN", "KING" };

        Color color;
        Value value;
        String name;

        public Card(int color, int value)
        {
            this.color = (Color)color;
            this.value = (Value)value;
            name = VALUE_NAME[(int)value] + " of " + COLOR_NAME[(int)color];
        }
    }
}
