using UnityEngine;
using UnityEngine.UI;

public class Blackjack : CardGame
{
    [SerializeField] Button btn_Hit;
    [SerializeField] Button btn_Deal;
    [SerializeField] Button btn_Stand;

	public Blackjack(int decks) : base(decks) { }

	public override void SetupTable() {
		
	}
	public override void StartGame() { 
		
	}

	public override void PlaceBets() { 
		
	}

	public override void EndRound() { 
		
	}

	public override void OnPlayerTurn() { 
		
	}
	public override void OnDealerTurn() { 
		
	}

	public override void TryGetWin() { 
		
	}

	public override void CleanTable() { 
		
	}
	public override void EndGame() { 
		
	}
}
