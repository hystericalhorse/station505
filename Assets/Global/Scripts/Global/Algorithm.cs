using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.XR;

public static class CardAlgorithms
{
	public static List<List<Card>> SortByRank(Card[] hand)
	{
		List<List<Card>> sorted = new();

		List<Card> temp = new();
		temp = hand.ToList(); // Create a copy of the hand as a list.
							  // Sort by Rank
		while (temp.Count > 0)
		{
			int i = 1;
			List<Card> list = new() { temp[0] }; // Create a temporary list to store all cards of this rank.

			while (true)
			{
				if (i == temp.Count) // if the index is at the end of the list, remove the [0] card and start the loop again.
				{
					temp.RemoveAt(0);
					sorted.Add(list);
					break;
				}

				if (temp[0].rank == temp[i].rank) // if the cards are the same rank, add the temp[i] card into the temporary list.
				{
					list.Add(temp[i]);
					temp.RemoveAt(i);
				}
				else i++;
			}
		}

		return sorted;
	}

	public static List<Card> SortSequentially(Card[] hand)
	{
		List<Card> sorted = new();
		List<Card> temp = hand.ToList();

		bool done = false;
		while (!done)
		{
			int next = 0;
			for (int i = 1; i < temp.Count; i++)
				if (temp[i].rank < temp[next].rank)
					next = i;

			sorted.Add(temp[next]);
			temp.RemoveAt(next);
			done = temp.Count == 0;
		}


		return sorted;
	}


	public static PokerHand EvaluateHand(Card[] hand)
	{
		if (hand.Length > 5 || hand.Length < 5)
			throw new System.Exception("You aren't playing poker");

		List<List<Card>> sorted = SortByRank(hand);

		// Log Sorted List
		string debug = "";
		foreach (var list in sorted)
			debug += list.First().rank.ToString() + ": " + list.Count().ToString() + "\n";

		UnityEngine.Debug.Log(debug);

		switch (sorted.Count)
		{
			default:
				return PokerHand.Nothing;
			case 5:
				List<Card> sorted2 = SortSequentially(hand);
				debug = "";
				foreach (var list in sorted2)
					debug += list.rank.ToString() + " ";

				UnityEngine.Debug.Log(debug);

				bool flush = true;
				bool straight = true;
				for (int i = 0; i < sorted2.Count - 1; i++)
				{
					if (((int)sorted2[i + 1].rank - (int)sorted2[i].rank) != 1)
					{
						straight = false;
						break;
					}
				}

				foreach (var card0 in sorted2)
				{
					if (!flush) break;
					foreach (var card1 in sorted2)
						if (card0.suit != card1.suit)
						{
							flush = false;
							break;
						}
				}

				if (straight && flush) return PokerHand.StraightFlush;
				if (flush) return PokerHand.Flush;
				if (straight) return PokerHand.Straight;

				return PokerHand.Nothing;
			case 2:
				if (sorted[0].Count == 4 || sorted[1].Count == 4)
				{
					if ((sorted[0].Count == 4 && sorted[1][0].rank == Rank.Joker) || (sorted[1].Count == 4 && sorted[0][0].rank == Rank.Joker))
						return PokerHand.FiveOf;
					else
						return PokerHand.FourOf;
				}

				return PokerHand.FullHouse;
			case 3:
				foreach (var list in sorted)
					if (list.Count == 3)
						return PokerHand.ThreeOf;

				return PokerHand.TwoPair;
			case 4:
				return PokerHand.Pair;
		}
	}
}

public enum PokerHand
{
	// A kicker is a card you hold that doesn't share rank with any other card in your hand.

	Nothing, // Shit hand lol
	Pair, // Two Cards of One Rank + Three Kickers
	TwoPair, // Two Pairs + Kicjer
	ThreeOf, // Three Cards of One Rank + Two Kickers
	Straight, // Five Cards of Sequential Rank
	Flush, // Five Cards of Same Suit
	FullHouse, // ThreeOf + OnePair
	FourOf, // Four Cards of One Rank + Kicker
	StraightFlush, // Five Cards of Sequential Rank & Same Suit
	FiveOf // FourOf + Joker
}