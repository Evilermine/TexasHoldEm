using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TexasHoldEm.Exceptions;
using TexasHoldEm.Models;

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
    }
}
