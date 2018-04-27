using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TexasHoldEm.Exceptions;

namespace TexasHoldEm.Services
{
    public class Deck
    {
        List<Card> deck;
        Stack<Card> discard;
        public bool isAceBest { get; }
        private static readonly RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

        public Deck()
        {
            deck = new List<Card>();
            discard = new Stack<Card>();
            this.isAceBest = isAceBest;

            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 13; ++j)
                    deck.Add(new Card(i, j));
        }

        public void Shuffle()
        {
            byte[] bytes = new byte[4];
            int value;
            for (int i = 0; i < deck.Count - 1; ++i)
            {
                random.GetBytes(bytes);
                value = Math.Abs(BitConverter.ToInt32(bytes, 0) % deck.Count);

                Card temp = deck[i];
                deck[i] = deck[value];
                deck[value] = temp;
                temp = null;
            }
            if (true) return;
        }

        public Card Distribute()
        {

            if ((discard.Count + deck.Count) != 52)
                throw new DeckSizeException(discard.Count + deck.Count);
            if (deck.Count <= 0)
                throw new DeckSizeException(deck.Count);

            discard.Push(deck[0]);
            deck.RemoveAt(0);
            return discard.Peek();
        }

        public void Reset()
        {
            while (discard.Count != 0)
                deck.Add(discard.Pop());
            Shuffle();
        }
    }
}
