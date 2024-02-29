using System.Collections.Generic;

public abstract class CardGame
{
	public CardGame(int decks) { this.decks = decks; }

	public Deck deck { get; set; }
	public int decks;

	public enum Turn { Player, Dealer }
	public Turn currentTurn;

	public virtual void SetupTable() { }
	public virtual void StartGame() { }

	public virtual void PlaceBets() { }

	public virtual void StartRound() { }
	public virtual void EndRound() { }

	public virtual void PassTurn(Turn turn) { }
	public virtual void OnPlayerTurn() { }
	public virtual void OnDealerTurn() { }

	public virtual void TryGetWin() { }
	public virtual void CleanTable() { }

	public virtual void EndGame() { }
}
