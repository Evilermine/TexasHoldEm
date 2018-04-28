using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexasHoldEm.Services.Rules
{
    public class TexasRules
    {
        private enum HandValue
        {
            CARD, PAIR, DOUBLE_PAIR, THREE_OF_A_KIND,
            STRAIGHT, FLUSH, FULLHOUSE, FOUR_OF_A_KIND,
            STRAIGHT_FLUSH, ROYAL_FLUSH
        };
        /*
         * Compare two hands
         * 
         * first param is the first hand
         * second param is the second hand
         * third param is the current river
         * 
         * return 1 if the first hand is better than the other one
         * return 0 if both hands are "equal"
         * return -1 if the second hand is better than the first one
         */
        public static int Compare(List<Card> h1, List<Card> h2, List<Card> river)
        {
            List<Card> hand1 = new List<Card>(h1);
            hand1.AddRange(river);
            List<Card> hand2 = new List<Card>(h2);
            hand2.AddRange(river);

            Tuple<HandValue, Card[]> handValue1 = GetHand(hand1);
            Tuple<HandValue, Card[]> handValue2 = GetHand(hand2);

            /*
             * The HandValue is different
             */
            if ((int)handValue1.Item1 > (int)handValue2.Item1)
                return 1;
            else if ((int)handValue1.Item1 < (int)handValue2.Item1)
                return -1;

            Card first = RuleSet.Highest(new List<Card>(handValue1.Item2));
            Card second = RuleSet.Highest(new List<Card>(handValue2.Item2));

            /*
             * The cards are different
             */
            if (first > second)
                return 1;
            else if (first < second)
                return -1;
            else
            {
                if ((int)first.color > (int)second.color)
                    return 1;
                else if ((int)first.color < (int)second.color)
                    return -1;
            }

            /*
             * The HandValue is the same and the cards are the same
             * We are using the river to build our game
             */
            List<Card> newRiver = new List<Card>(river);

            foreach (Card card in handValue1.Item2)
            {
                newRiver.Remove(card);
            }

            if (newRiver.Count <= 0)
                return 0;

            return Compare(h1, h2, newRiver);
        }
        private static Tuple<HandValue, Card[]> GetHand(List<Card> h)
        {
            Tuple<HandValue, Card[]> rep;

            if (RuleSet.StraightFlushRoyal(h, 5) != null)
                rep = new Tuple<HandValue, Card[]>(HandValue.ROYAL_FLUSH, RuleSet.StraightFlushRoyal(h, 5));
            else if (RuleSet.StraightFlush(h, 5) != null)
                rep = new Tuple<HandValue, Card[]>(HandValue.STRAIGHT_FLUSH, RuleSet.StraightFlush(h, 5));
            else if (RuleSet.FourOfAKind(h) != null)
                rep = new Tuple<HandValue, Card[]>(HandValue.FOUR_OF_A_KIND, RuleSet.FourOfAKind(h));
            else if (RuleSet.FullHouse(h) != null)
                rep = new Tuple<HandValue, Card[]>(HandValue.FULLHOUSE, Flatten(RuleSet.FullHouse(h)));
            else if (RuleSet.Flush(h, 5) != null)
                rep = new Tuple<HandValue, Card[]>(HandValue.FLUSH, FlattenFlush(RuleSet.Flush(h, 5)));
            else if (RuleSet.Straight(h, 5) != null)
                rep = new Tuple<HandValue, Card[]>(HandValue.STRAIGHT, RuleSet.Straight(h, 5));
            else if (RuleSet.ThreeOfAKind(h) != null)
                rep = new Tuple<HandValue, Card[]>(HandValue.THREE_OF_A_KIND, RuleSet.ThreeOfAKind(h));
            else if (RuleSet.DoublePair(h) != null)
                rep = new Tuple<HandValue, Card[]>(HandValue.DOUBLE_PAIR, Flatten(RuleSet.DoublePair(h)));
            else if (RuleSet.Pair(h) != null)
                rep = new Tuple<HandValue, Card[]>(HandValue.PAIR, RuleSet.Pair(h));
            else
                rep = new Tuple<HandValue, Card[]>(HandValue.CARD, new Card[1] { RuleSet.Highest(h) });
            return rep;
        }
        private static Card[] Flatten(Card[][] cards)
        {
            return cards.SelectMany(x => x).ToArray();
        }
        private static Card[] FlattenFlush(Card[][] cards)
        {
            List<Card> rep = new List<Card>();
            foreach (Card[] card in cards)
                if (card != null)
                    foreach (Card card_inner in card)
                        rep.Add(card_inner);
            return rep.ToArray();
        }
    }
}
