using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexasHoldEm.Exceptions;
using TexasHoldEm.Models;

namespace TexasHoldEm.Services
{
    public class Seat
    {
        private String playerUsername;
        private Card[] cards;
        public bool isEmpty { get; set; }
        public bool isFolded { get; set; }

        public Seat(int nbrCards)
        {
            cards = new Card[nbrCards];
            for (int i = 0; i < cards.Length; ++i)
                cards[i] = null;
            playerUsername = null;
            isEmpty = true;
            isFolded = false;
        }
        public Seat(int nbrCards, String playerUsername) : this(nbrCards)
        {
            this.playerUsername = playerUsername;
            isEmpty = false;
            isFolded = false;
        }
        public void ReceiveCard(Card c)
        {
            if (cards[0] == null)
                cards[0] = c;
            else if (cards[1] == null)
                cards[1] = c;
            else
                throw new HandFullException(cards.Length);
        }
        public void EmptySeat()
        {
            playerUsername = null;
            ResetHand();
            isEmpty = true;
        }
        public void ResetHand()
        {
            for (int i = 0; i < cards.Length; ++i)
                cards[i] = null;
        }
        public Card[] DisplayCard()
        {
            return cards;
        }
        public String GetPlayerName()
        {
            return playerUsername;
        }
        public void AddPlayer(String playerUsername)
        {
            if (!isEmpty)
                throw new SeatAlreadyTakenException(playerUsername);

            this.playerUsername = playerUsername;
            this.isEmpty = false;
        }
        public void Fold()
        {
            if (isEmpty)
                return;
            isFolded = true;
        }
    }
}
