using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexasHoldEm.Exceptions;
using TexasHoldEm.Models;
using TexasHoldEm.Services.Rules;

namespace TexasHoldEm.Services
{
    public class Table
    {
        private Deck deck;
        private Seat[] seats = new Seat[NBR_SEATS];
        private List<Card> river = new List<Card>();
        private const int NBR_CARDS_PER_HAND = 2;
        private const int NBR_SEATS = 4;
        private const int NBR_CARDS_IN_RIVER = 5;
        private int pot = 0;
        private int currentPlayerTurn = 0;


        public void onPlayerFold()
        {
            seats[currentPlayerTurn].Fold();
        }
        public String nextTurn()
        {
            do {
                    if (currentPlayerTurn < 3)
                    {
                        currentPlayerTurn += 1;
                        return seats[currentPlayerTurn].GetPlayerName();
                    }
                    if (currentPlayerTurn == 3)
                    {
                        currentPlayerTurn = 0;
                        return seats[currentPlayerTurn].GetPlayerName();
                    }
                    return null;
            } while (seats[currentPlayerTurn].isFolded);
        }
        public Table(String playerUsername)
        {
            deck = new Deck();
            deck.Shuffle();

            seats[0] = new Seat(NBR_CARDS_PER_HAND, playerUsername);
            for (int i = 1; i < NBR_SEATS; ++i)
                seats[i] = new Seat(NBR_CARDS_PER_HAND);
        }
        public List<Card> DistributeRiver(int nbrCards)
        {
            for (int i = 0; i < nbrCards; ++i)
            {
                try
                {
                    AddToRiver(deck.Distribute());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            return river;
        }
        public void DistributeToPlayer(int nbrCards)
        {
            for (int i = 0; i < nbrCards; i++)
                foreach (Seat seat in seats)
                    if (!seat.isEmpty)
                    {
                        try
                        {
                            seat.ReceiveCard(deck.Distribute());
                        }
                        catch (DeckSizeException e)
                        {
                            Console.WriteLine(e.Message);
                            return;
                        }
                        catch (HandFullException e)
                        {
                            Console.WriteLine(e.Message);
                            return;
                        }
                    }
        }
        public void Discard(int nbrCards)
        {
            for (int i = 0; i < nbrCards; i++)
                deck.Distribute();
        }
        public List<Seat> CalculateWinner()
        {
            if (river.Count != NBR_CARDS_IN_RIVER)
                throw new RoundNotCompletedException(river.Count);

            List<Seat> bestHand = new List<Seat>();
            bestHand.Add(seats[0]);

            for (int i = 1; i < seats.Length; ++i)
            {
                switch (TexasRules.Compare(new List<Card>(bestHand.First().DisplayCard()),
                                        new List<Card>(seats[i].DisplayCard()), river))
                {
                    case 1:
                        break;
                    case -1:
                        bestHand.Clear();
                        bestHand.Add(seats[i]);
                        break;
                    case 0:
                        bestHand.Add(seats[i]);
                        break;
                }
            }

            return bestHand;
        }
        public void EndRound()
        {
            if (river.Count != NBR_CARDS_IN_RIVER)
                throw new RoundNotCompletedException(river.Count);

            foreach (Seat seat in seats)
                seat.ResetHand();

            river.Clear();
            deck.Reset();
            deck.Shuffle();
        }
        /*
         * Removes a player from the table if he exists in the context
         * 
         * Return true is there is still players on the table
         * false otherwise
         */
        public bool RemovePlayer(String playerUsername)
        {
            for (int i = 0; i < seats.Length; ++i)
                if (seats[i] != null)
                    if (String.Compare(playerUsername, seats[i].GetPlayerName(), true) == 0)
                        seats[i].EmptySeat();

            foreach (Seat seat in seats)
                if (!seat.isEmpty)
                    return true;

            return false;
        }
        /*
         * Add a player to the table
         * 
         * If the table is full, return false
         * otherwise, return true
         */
        public bool AddPlayer(String playerUsername)
        {
            foreach (Seat seat in seats)
                if (seat.isEmpty)
                {
                    try
                    {
                        seat.AddPlayer(playerUsername);
                        return true;
                    }
                    catch (SeatAlreadyTakenException e)
                    {
                        Console.WriteLine(e.Message);
                        break;
                    }
                }

            return false;
        }
        private void AddToRiver(Card c)
        {
            if (river.Count >= NBR_CARDS_IN_RIVER)
                throw new RiverSizeException(river.Count);
            river.Add(c);
        }
        public String[] GetCardsByPlayer(String username)
        {
            Card[] cards = null;
            foreach(Seat seat in seats)
            {
                if(String.Compare(username, seat.GetPlayerName(), true) == 0)
                {
                    cards = seat.DisplayCard();
                }
            }
            if (cards == null)
                return null;

            String[] cardsName = new String[2];

            if (cards.Length != 2)
                return null;

            for (int i = 0; i < cards.Length; ++i)
                cardsName[i] = ParseCardByCard(cards[i]);

            return cardsName;
        }
        public String ParseCardByCard(Card c)
        {
            char[] value;
            char color;

            if (c.value == Card.Value.ACE)
                value = "14".ToArray();
            else
                value = ((int)c.value + 2).ToString().ToArray();

            String name = c.name;
            String[] cut_name = name.Split();

            color = cut_name[2][0];
            color = Char.ToLower(color);

            String finalName = color.ToString();
            finalName += (new String(value) + ".png");

            return finalName;
        }
        public int getPot()
        {
            return pot;
        }
        public void onBid(int bid)
        {
            pot += bid;
        }
        public int getCurrentPlayerTurn()
        {
            return currentPlayerTurn;
        }
    }
}
