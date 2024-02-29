using UnityEngine;
using UnityEngine.UI;

public class Blackjack : CardGame
{
    //Player
    private Card[] playerHand;

	// Dealer
	private Card[] dealerHand;

    public Blackjack(int decks) : base(decks) { }

	#region CardGame

	public override void SetupTable()
	{
		
	}
	public override void StartGame()
	{
		deck = new Deck(decks: decks);

		deck.Shuffle();

		if (playerHand == null) 
		{
			playerHand = new Card[2];
		}

		if (dealerHand == null)
		{
			dealerHand = new Card[2];
		}

        for (int i = 0; i < 2; i++)
        {
			playerHand[i] = deck.Cards[0];
			deck.Cards.RemoveAt(i);
        }

        for (int i = 0; i < 2; i++)
        {
            dealerHand[i] = deck.Cards[0];
            deck.Cards.RemoveAt(i);
        }
    }

	public override void PlaceBets()
	{ 
		
	}

	public override void EndRound()
	{
		
	}

	public override void OnPlayerTurn()
	{
		
	}

	public override void OnDealerTurn()
	{
		var wheeler = GameManager.instance.GetWheeler();
		Move move = wheeler.PlayBlackjack();
		switch (move)
		{
			default:
			case Move.Stand:
				TryGetWin();
				return;
			case Move.Hit:
				wheeler.DrawCards(deck, 1);
				break;
		}
	}

	public override void TryGetWin()
	{
		
	}

	public override void CleanTable()
	{
		
	}

	public override void EndGame()
	{
		
	}

	#endregion

	#region Blackjack
	public enum Move
	{
		Stand, Hit
	}
	#endregion
}
