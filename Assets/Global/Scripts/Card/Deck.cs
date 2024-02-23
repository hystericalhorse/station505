using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Card
{
	public enum Suit
	{
		Hearts,
		Clubs,
		Diamonds,
		Spades
	}
	public enum Rank
	{
		Ace,
		Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
		Jack, Queen, King,
		Joker
	}

	Suit suit;
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

	public int GetBlackJackValue()
	{
		switch (rank)
		{
			case Rank.Ace:
				return 11;
			case Rank.Two:
				return 2;
			case Rank.Three:
				return 3;
			case Rank.Four:
				return 4;
			case Rank.Five:
				return 5;
			case Rank.Six:
				return 6;
			case Rank.Seven:
				return 7;
			case Rank.Eight:
				return 8;
			case Rank.Nine:
				return 9;
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
					Cards.Add(new((Card.Suit)suit, (Card.Rank)rank));

			for (; jokersPerDeck > 0; jokersPerDeck--)
				Cards.Add(new(Card.Rank.Joker));
		}
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