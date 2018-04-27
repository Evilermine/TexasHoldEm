using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TexasHoldEm.Services.Rules
{
    public static class RuleSet
    {
        public static Card Highest(List<Card> cards)
        {
            Card highest = cards[0];
            foreach (Card card in cards)
                if (card > highest)
                    highest = card;
            return highest;
        }
        public static Card[] Pair(List<Card> cards)
        {
            if (cards.Count < 2)
                return null;

            List<Card> temp = new List<Card>(cards);
            temp.Sort();

            for (int i = 0; i < temp.Count - 1; ++i)
                if (temp[i] == temp[i + 1])
                {
                    Card[] rep = { temp[i], temp[i + 1] };
                    return rep;
                }
            return null;

        }
        public static Card[][] DoublePair(List<Card> cards)
        {
            if (cards.Count < 4)
                return null;

            List<Card> temp = new List<Card>(cards);
            Card[] firstPair, secondPair;

            if ((firstPair = Pair(temp)) == null)
                return null;
            temp.RemoveAt(temp.IndexOf(firstPair[0]));
            temp.RemoveAt(temp.IndexOf(firstPair[0]));

            if ((secondPair = Pair(temp)) == null)
                return null;

            Card[][] rep = { firstPair, secondPair };
            return rep;
        }
        public static Card[] ThreeOfAKind(List<Card> cards)
        {
            if (cards.Count < 3)
                return null;

            List<Card> temp = new List<Card>(cards);
            temp.Sort();
            Card[] toak;

            for (int i = 0; i < temp.Count - 2; ++i)
                if (temp[i] == temp[i + 1] && temp[i + 1] == temp[i + 2])
                {
                    toak = new Card[3];
                    toak[0] = temp[i];
                    toak[1] = temp[i + 1];
                    toak[2] = temp[i + 2];
                    return toak;
                }

            return null;
        }
        public static Card[] Straight(List<Card> cards, int numberOfCards)
        {
            List<Card> temp = new List<Card>(cards);
            temp.Sort();
            List<Card> straight = new List<Card>();
            straight.Add(temp[0]);

            for (int i = 1; i < temp.Count; ++i)
            {
                if (straight.Last().value + 1 == temp[i].value)
                {
                    straight.Add(temp[i]);
                    if (straight.Count >= numberOfCards)
                        return straight.ToArray();
                }
                else
                {
                    straight.Clear();
                    straight.Add(temp[i]);
                }
            }
            return null;
        }
        public static Card[][] Flush(List<Card> cards, int nbrOfCards)
        {
            List<Card> temp = new List<Card>(cards);
            List<Card> clubs = new List<Card>();
            List<Card> spades = new List<Card>();
            List<Card> hearts = new List<Card>();
            List<Card> diamonds = new List<Card>();
            Card[][] flush = new Card[4][];

            foreach (Card card in temp)
            {
                switch (card.color)
                {
                    case Card.Color.SPADE:
                        spades.Add(card);
                        break;
                    case Card.Color.HEART:
                        hearts.Add(card);
                        break;
                    case Card.Color.DIAMOND:
                        diamonds.Add(card);
                        break;
                    case Card.Color.CLUB:
                        clubs.Add(card);
                        break;
                    default:
                        return null;
                }
            }

            if (spades.Count >= nbrOfCards)
            {
                flush[0] = new Card[spades.Count];
                for (int i = 0; i < spades.Count; ++i)
                    flush[0][i] = spades[i];
            }
            else
                flush[0] = null;

            if (hearts.Count >= nbrOfCards)
            {
                flush[1] = new Card[hearts.Count];
                for (int i = 0; i < hearts.Count; ++i)
                    flush[1][i] = hearts[i];
            }
            else
                flush[1] = null;

            if (clubs.Count >= nbrOfCards)
            {
                flush[2] = new Card[clubs.Count];
                for (int i = 0; i < clubs.Count; ++i)
                    flush[2][i] = clubs[i];
            }
            else
                flush[2] = null;

            if (diamonds.Count >= nbrOfCards)
            {
                flush[3] = new Card[diamonds.Count];
                for (int i = 0; i < diamonds.Count; ++i)
                    flush[3][i] = diamonds[i];
            }
            else
                flush[3] = null;

            return flush;
        }
        public static Card[][] FullHouse(List<Card> cards)
        {
            Card[][] fullHouse = new Card[2][];
            fullHouse[0] = null;
            fullHouse[1] = null;

            List<Card> temp = new List<Card>(cards);
            Card[] pair = Pair(temp);
            if (pair == null)
                return null;
            temp.Remove(pair[0]);
            temp.Remove(pair[1]);

            fullHouse[0] = pair;

            Card[] toak = ThreeOfAKind(temp);
            if (toak == null)
                return null;
            fullHouse[1] = toak;

            return fullHouse;
        }
        public static Card[] FourOfAKind(List<Card> cards)
        {
            List<Card> temp = new List<Card>(cards);

            do
            {
                Card[] toak = ThreeOfAKind(temp);
                if (toak == null)
                    break;

                List<Card> foak = new List<Card>(toak);
                for (int i = 0; i < foak.Count; i++)
                    temp.Remove(foak[i]);

                for (int i = 0; i < temp.Count; i++)
                {
                    if (foak[0].value == temp[i].value)
                    {
                        foak.Add(temp[i]);
                        return foak.ToArray();
                    }
                }
            } while (temp.Count >= 4);

            return null;
        }
        public static Card[] StraightFlush(List<Card> cards, int nbrOfCards)
        {
            List<Card> temp = new List<Card>(cards);
            do
            {
                Card[] straight = Straight(temp, nbrOfCards);
                if (straight == null)
                    break;

                Card[][] flush = Flush(new List<Card>(straight), nbrOfCards);
                for (int i = 0; i < 4; ++i)
                {
                    if (flush[i] != null)
                        return straight.ToArray();
                }

                for (int i = 0; i < straight.Length; i++)
                    temp.Remove(straight[i]);
            } while (temp.Count >= nbrOfCards);

            return null;
        }
        public static Card[] StraightFlushRoyal(List<Card> cards, int nbrOfCards)
        {
            List<Card> temp = new List<Card>(cards);

            do
            {
                Card[] straightFlush = StraightFlush(temp, nbrOfCards);
                if (straightFlush == null)
                    break;
                if (straightFlush.Last().value != Card.Value.ACE)
                    temp.Remove(straightFlush[0]);
                else
                    return straightFlush;
            } while (temp.Count >= nbrOfCards);

            return null;
        }
    }
}
