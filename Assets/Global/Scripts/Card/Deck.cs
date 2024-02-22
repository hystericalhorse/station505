using System.Collections.Generic;

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
	Rank rank;

	public Card(Suit suit, Rank rank)
	{
		this.suit = suit;
		this.rank = rank;
	}

	public Card(Rank rank)
	{
		this.rank = rank;
	}
}

public class Deck
{
    public readonly List<Card> Cards;

    public Deck(int jokers = 0)
    {
		for (int suit = 0; suit < 4; suit++)
			for (int rank = 0; rank < 13; rank++)
				Cards.Add(new((Card.Suit)suit, (Card.Rank)rank));

		for (; jokers > 0; jokers--)
			Cards.Add(new(Card.Rank.Joker));
	}
}
