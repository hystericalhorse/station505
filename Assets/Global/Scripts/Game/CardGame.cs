using System.Collections.Generic;

public abstract class CardGame
{
	public CardGame(int decks) { this.decks = decks; }

	public Deck deck { get; set; }
	public int decks;

	public virtual void SetupTable() { }
	public virtual void StartGame() { }
	public virtual void PlaceBets() { }
	public virtual void EndRound() { }
	public virtual void OnPlayerTurn() { }
	public virtual void OnDealerTurn() { }
	public virtual void TryGetWin() { }
	public virtual void CleanTable() { }
	public virtual void EndGame() { }
}
