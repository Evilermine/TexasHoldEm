using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TexasHoldEm.Services
{
    public class Card : IComparable<Card>
    {
        public enum Color { SPADE, HEART, DIAMOND, CLUB };
        public enum Value { TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE };

        private static readonly String[] COLOR_NAME = { "SPADE", "HEART", "DIAMOND", "CLUB" };
        private static readonly String[] VALUE_NAME = { "TWO", "THREE",
            "FOUR", "FIVE", "SIX",
            "SEVEN", "EIGHT", "NINE",
            "TEN", "JACK", "QUEEN", "KING", "ACE" };

        public Color color { get; }
        public Value value { get; }
        public String name { get; }
        public bool isAceBest { get; }

        public Card(int color, int value)
        {
            this.color = (Color)color;
            this.value = (Value)value;
            name = VALUE_NAME[(int)value] + " of " + COLOR_NAME[(int)color];
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Card card = (Card)obj;
            return ((int)value == (int)card.value);
        }

        public override int GetHashCode()
        {
            var hashCode = 818507544;
            hashCode = hashCode * -1521134295 + color.GetHashCode();
            hashCode = hashCode * -1521134295 + value.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            return hashCode;
        }

        public int CompareTo(Card other)
        {
            if (object.ReferenceEquals(this, null))
                return -1;
            if (object.ReferenceEquals(other, null))
                return 1;
            if (this == other)
                return 0;
            else if (this > other)
                return 1;
            else
                return -1;
        }

        public static bool operator <(Card c1, Card c2)
        {
            if (object.ReferenceEquals(c1, null))
                return true;
            if (object.ReferenceEquals(c2, null))
                return false;
            if (c1.isAceBest && c2.isAceBest && c2.value == 0 && c1.value != c2.value)
                return true;
            return c1.value < c2.value;
        }

        public static bool operator >(Card c1, Card c2)
        {
            if (object.ReferenceEquals(c2, null))
                return true;
            if (object.ReferenceEquals(c1, null))
                return false;
            if (c1.isAceBest && c2.isAceBest && c1.value == 0 && c1.value != c2.value)
                return true;
            return c1.value > c2.value;
        }

        public static bool operator <=(Card c1, Card c2)
        {
            if (object.ReferenceEquals(c1, null))
                return true;
            if (object.ReferenceEquals(c2, null))
                return false;
            if (c1.isAceBest && c2.isAceBest && c2.value == 0)
                return true;
            return c1.value <= c2.value;
        }

        public static bool operator >=(Card c1, Card c2)
        {
            if (object.ReferenceEquals(c2, null))
                return true;
            if (object.ReferenceEquals(c1, null))
                return false;
            if (c1.isAceBest && c2.isAceBest && c1.value == 0)
                return true;
            return c1.value >= c2.value;
        }

        public static bool operator ==(Card c1, Card c2)
        {
            if (object.ReferenceEquals(c1, null) || object.ReferenceEquals(c2, null))
                return false;
            return c1.value == c2.value;
        }

        public static bool operator !=(Card c1, Card c2)
        {
            if (object.ReferenceEquals(c1, null) || object.ReferenceEquals(c2, null))
                return true;
            return c1.value != c2.value;
        }
    }
}
