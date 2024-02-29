using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poker : CardGame
{
	public Poker(int decks) : base(decks) { }

	#region CardGame

	public override void SetupTable()
	{

	}
	public override void StartGame()
	{
		deck = new Deck(decks: decks);
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

	#region Poker

	#endregion

}
