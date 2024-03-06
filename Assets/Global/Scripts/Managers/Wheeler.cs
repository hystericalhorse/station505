using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheeler
{
    // Hi, my name is Wheeler!

    public Hand myHand = new();

    public void DrawCards(ref Deck deck, uint count) => myHand.Draw(ref deck, count);
    public void DiscardCard(ref Deck discards, Card card) => myHand.Discard(card, ref discards);

	public void PlayPoker(ref Deck deck, ref Deck discards) // To be called after the player's turn in the first round.
    {
		// I never fold! Probably not the best strategy...

		var checkHand = CardAlgorithms.EvaluateHand(myHand.hand.ToArray(), out var highCard, out var kickers);
        if ((int) checkHand > 0)
        {
		    switch (checkHand) // Only really need to discard if you have kickers.
            {
                default:
                    break;
                case PokerHand.Pair:// Two Cards of One Rank + Three Kickers
				case PokerHand.TwoPair:  // Two Pairs + One Kicker
                case PokerHand.ThreeOf: // Three Cards of One Rank + Two Kickers
                    //TODO Discard the kickers.
                    foreach (var card in kickers)
                        DiscardCard(ref discards, card);
					break;
			}
        }
        else
        {            
            // If I have nothing good, I will discard all but my highest card.
			var i = myHand.hand.FindIndex(0, myHand.Count, x => (x == highCard));
            for (int j = 0; j < myHand.Count; j++)
            {
                if (i == j || myHand.hand[j] == null)
                    continue;
                else
                    DiscardCard(ref discards, myHand.hand[j]);
            }
		}

		// Must draw.
		DrawCards(ref deck, (uint)(5 - myHand.Count));
	}

    public Blackjack.Move PlayBlackjack()
    {
        // In Blackjack, Hand[0] is always hidden.
        if (myHand.Count == 2 && (myHand.hand[1].rank == Rank.Ace))
        {
            return Blackjack.Move.Stand;
        }

        if (myHand.hand[1].GetBlackjackValue() >= 7)
        {
            switch (Random.Range(0,1))
            {
                default:
				case 0:
                    break;
				case 1:
					return Blackjack.Move.Stand;
			}
            
        }

		int handValue = 0;
        List<Card> aces = new();
        for (int i = 1; i < myHand.Count; i++)
        {
            if (myHand.hand[i].rank == Rank.Ace)
            {
                aces.Add(myHand.hand[i]);
				continue;
			}
            handValue += myHand.hand[i].GetBlackjackValue();
		}

        for (int i = 0; i < aces.Count; i++)
			handValue += aces[i].GetBlackjackValue(acesHigh: handValue < 10);

        if (handValue >= 10)
            return Blackjack.Move.Stand;
        else
            return Blackjack.Move.Hit;
    }

    public Rank PlayGoFish()
    {
        //TODO
        var books = CardAlgorithms.SortByRank(myHand.hand.ToArray());

        Rank whatToAskFor = Rank.Two;
        int mostVal = 0;
        foreach (var book in books)
        {
            if (book.Count > mostVal) // If I have more of these than those...
            {
                mostVal = book.Count;
                whatToAskFor = book[0].rank;
            }
        }

        return whatToAskFor;
    }
}
