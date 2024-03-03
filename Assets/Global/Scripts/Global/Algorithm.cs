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



	public static PokerHand EvaluateHand(Card[] hand, out Card highCard)
	{
		if (hand.Length > 5 || hand.Length < 5)
			throw new System.Exception("You aren't playing poker");

		List<List<Card>> sorted = SortByRank(hand);
		List<Card> sorted2 = SortSequentially(hand);

		highCard = sorted2.Last();

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
					highCard = (sorted[0].Count == 4) ? sorted[0][0] : sorted[1][0];
					if ((sorted[0].Count == 4 && sorted[1][0].rank == Rank.Joker) || (sorted[1].Count == 4 && sorted[0][0].rank == Rank.Joker))
						return PokerHand.FiveOf;
					else
						return PokerHand.FourOf;
				}

				highCard = sorted[0][0].rank > sorted[1][0].rank ? sorted[0][0] : sorted[1][0];
				return PokerHand.FullHouse;
			case 3:
				highCard = (sorted[0].Count != 2) ? (sorted[1].Count != 2 ? sorted[2][0] : sorted[1][0]) : sorted[0][0];

				foreach (var list in sorted)
				{
					if (list.Count == 2 && highCard.rank < list[0].rank) highCard = list[0];
					if (list.Count == 3) return PokerHand.ThreeOf;
				}

				return PokerHand.TwoPair;
			case 4:
				foreach (var list in sorted)
				{
					if (list.Count == 2) highCard = list[0];
					break;
				}

				return PokerHand.Pair;
		}
	}
	public static PokerHand EvaluateHand(Card[] hand, out Card highCard, out List<Card> kickers)
	{
		if (hand.Length > 5 || hand.Length < 5)
			throw new System.Exception("You aren't playing poker");

		List<List<Card>> sorted = SortByRank(hand);
		List<Card> sorted2 = SortSequentially(hand);
		kickers = new();

		highCard = sorted2.Last();

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
					highCard = (sorted[0].Count == 4) ? sorted[0][0] : sorted[1][0];
					if ((sorted[0].Count == 4 && sorted[1][0].rank == Rank.Joker) || (sorted[1].Count == 4 && sorted[0][0].rank == Rank.Joker))
						return PokerHand.FiveOf;
					else
						return PokerHand.FourOf;
				}

				highCard = sorted[0][0].rank > sorted[1][0].rank ? sorted[0][0] : sorted[1][0];
				return PokerHand.FullHouse;
			case 3:

				var returnVal = PokerHand.TwoPair;

				highCard = (sorted[0].Count != 2) ? (sorted[1].Count != 2 ? sorted[2][0] : sorted[1][0]) : sorted[0][0];
				
				foreach (var list in sorted)
				{
					if (list.Count == 2 && highCard.rank < list[0].rank)
						highCard = list[0];
					if (list.Count == 3) returnVal = PokerHand.ThreeOf;
				}

				if (returnVal == PokerHand.ThreeOf)
				{
					foreach (var list in sorted)
					{
						if (list.Count != 3)
							kickers.AddRange(list);
					}

					return returnVal;
				}

				foreach (var list in sorted)
					if (list.Count != 2)
						kickers.AddRange(list);

				return returnVal;
			case 4:
				foreach (var list in sorted)
				{
					if (list.Count == 2)
					{
						highCard = list[0];
						break;
					}
				}

				foreach (var list in sorted)
					if (list.Count != 2)
						kickers.AddRange(list);

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