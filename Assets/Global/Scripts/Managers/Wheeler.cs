using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheeler
{
    // Hi, my name is Wheeler!

    Hand myHand;

    public void DealMe(Deck deck, uint count) => myHand.Draw(ref deck, count);

	public void PlayPoker()
    {
        // TODO
    }

    public Blackjack.Move PlayBlackjack()
    {
        // In Blackjack, Hand[0] is always hidden.
        if (myHand.Count == 2 && myHand.hand[1].rank == Rank.Ace)
        {
            return Blackjack.Move.Stand; 
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

        if (handValue > 17)
            return Blackjack.Move.Stand;
        else
            return Blackjack.Move.Hit;
    }

    public void PlayGoFish()
    {
        //TODO
    }
}
