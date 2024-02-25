using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
	public Suit suit;
	public Rank rank;

	public Card(Suit suit, Rank rank)
	{
		this.suit = suit;
		this.rank = rank;
	}

	public Card(Rank rank)
	{
		this.rank = rank;
	}

	public int GetValue(bool acesHigh = true)
	{
		switch (rank)
		{
			case Rank.Ace:
				return acesHigh ? 11 : 1;
			case Rank.Two:
			case Rank.Three:
			case Rank.Four:
			case Rank.Five:
			case Rank.Six:
			case Rank.Seven:
			case Rank.Eight:
			case Rank.Nine:
				return (int)rank+1;
			case Rank.Ten:
			case Rank.Jack:
			case Rank.Queen:
			case Rank.King:
				return 10;
			default:
				return 0;
		}
	}
}

public class Deck
{
    public List<Card> Cards { get; private set; }

	public Deck(int decks = 1, int jokersPerDeck = 0)
    {
		for (decks = Mathf.Abs(decks); decks > 0; decks--)
		{
			for (int suit = 0; suit < 4; suit++)
				for (int rank = 0; rank < 13; rank++)
					Cards.Add(new((Suit)suit, (Rank)rank));

			for (; jokersPerDeck > 0; jokersPerDeck--)
				Cards.Add(new(Rank.Joker));
		}
	}

	public void Shuffle() => Cards.Shuffle();

	public Card Peek()
	{
		return Cards[0];
	}

	public Card Draw()
	{
		Card card = Cards[0];
		Cards.Remove(Cards[0]);
		return card;
	}

	public Card[] Draw(uint count = 1)
	{
		Card[] cards = new Card[count];
		for (var i = 0; i < count; i++)
			cards[i] = Draw();

		return cards;
	}
}

public static class CardExt
{
	public static List<Card> Shuffle(this List<Card> deckA)
	{
		List<Card> deckB = new();

		while (deckA.Count > 0)
		{
			var card = deckA[UnityEngine.Random.Range(0, (int)deckA.Count - 1)];
			deckB.Add(card); deckA.Remove(card);
		}

		return deckB;
	}
}