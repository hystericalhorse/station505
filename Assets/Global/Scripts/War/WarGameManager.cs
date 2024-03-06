using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WarGameManager : MonoBehaviour
{
    // Buttons
    [SerializeField] private Button hitBtn;
    [SerializeField] private Button dealBtn;

    // Audio
    [SerializeField] private AudioSource placeCard;

    // UI
    [SerializeField] private TextMeshProUGUI winnerBox;
    [SerializeField] private TextMeshProUGUI playerPointText;
    [SerializeField] private TextMeshProUGUI dealerPointText;

	[SerializeField] private WarUIScript warUI;

    // Cards
	//private List<Card> playerHand = new();
	private Deck playerHand = new();
	//private List<Card> dealerHand = new();
	private Deck dealerHand = new();

    // Deck
	private Deck deck = new();
	public PlayerHand playerHandScript;

	// Points
	private short playerPoints; // hehe short
	private short dealerPoints;

	private short tiePoints = 0;

	// Start is called before the first frame update
	void Start()
    {
		// Click Listeners 
		dealBtn.onClick.AddListener(() => DealClicked());
		hitBtn.onClick.AddListener(() => HitClicked());
	}

	private void HitClicked()
	{

		playerHandScript.DeleteAllCards();

		Card playerCard = playerHand.Draw();

		Card dealerCard = dealerHand.Draw();

		if (playerCard.rank > dealerCard.rank) // player has higher value
		{
			playerPoints = (short)(playerPoints + 1 + tiePoints);
			tiePoints = 0;
			winnerBox.text = "Player Won Round";
		}
		else if (playerCard.rank < dealerCard.rank) // dealer has higher value
		{
			dealerPoints = (short)(dealerPoints + 1 + tiePoints);
			tiePoints = 0;
			winnerBox.text = "Dealer Won Round";
		}
		else // tie
		{
			tiePoints++;
			winnerBox.text = "Round was a TIE";
		}

		playerHandScript.DrawCardFromDeck(playerCard);

		playerPointText.text = playerPoints.ToString();
		dealerPointText.text = dealerPoints.ToString();
		// check winner
		CheckWinner();

		// reset text 
		Invoke("ResetWinnerBoxText", 3);
	}

	private void DealClicked()
	{
		RestartGame();
		deck.Shuffle();

		int deckHalf = deck.Get.Count / 2;

		playerHand.Cards.Clear();
		dealerHand.Cards.Clear();

		//sets the hands
		for (int i = 0; i < deckHalf; i++)
		{
			playerHand.Cards.Add(deck.Draw());
			AudioManager.instance.PlaySound("PlayCard");
		}
		for (int i = 0; i < deckHalf; i++)
		{
			dealerHand.Cards.Add(deck.Draw());
			//playerHandScript.DrawCardFromDeck(drawnCard);

			AudioManager.instance.PlaySound("PlayCard");
		}
	}

	private void RestartGame()
	{
		deck = new Deck();
		playerHand = new();
		dealerHand = new();
		winnerBox.text = string.Empty;
		playerPoints = 0;
		dealerPoints = 0;
	}

	private void CheckWinner()
	{
		if (playerPoints >= 10)
		{
			winnerBox.text = "Player Won Game";
			GameManager.instance.SetMoney(GameManager.instance.GetMoney() + GameManager.instance.currentBet * 2);
		}
		else if (dealerPoints >= 10)
		{
			winnerBox.text = "Dealer Won Game";
		}

		StartCoroutine(WaitFiveSeconds());
	}

	private IEnumerator WaitFiveSeconds()
	{
		playerPointText.text = "000";
		dealerPointText.text = "000";
		GameManager.instance.BetUI.GetComponent<BetUIMenu>().UpdateValues();
		GameManager.instance.BetUI.GetComponent<BetUIMenu>().BetReset();
		warUI.ResetUI();
		yield return new WaitForSeconds(5f);
		RestartGame();
	}

	private void ResetWinnerBoxText()
	{
		winnerBox.text = string.Empty;
	}
}
