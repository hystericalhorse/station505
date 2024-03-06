using System;
using System.Collections;
using System.Collections.Generic;
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

    // Cards
	private List<Card> playerHand = new();
	private List<Card> dealerHand = new();

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
		Card playerCard = playerHand[0];
		playerHand.Remove(playerCard);

		Card dealerCard = dealerHand[0];
		dealerHand.Remove(dealerCard);

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

		//sets the hands
		for (int i = 0; i < deckHalf; i++)
		{
			Card drawnCard = deck.Draw();
			playerHand.Add(drawnCard);
			playerHandScript.DrawCardFromDeck(drawnCard);

			AudioManager.instance.PlaySound("PlayCard");

			
		}
		for (int i = deckHalf; i < deck.Get.Count; i++)
		{
			Card drawnCard = deck.Draw();
			dealerHand.Add(drawnCard);
			playerHandScript.DrawCardFromDeck(drawnCard);

			AudioManager.instance.PlaySound("PlayCard");
		}

		playerHandScript.FlipCard(0);
	}

	private void RestartGame()
	{
		deck = new Deck();
		playerHand = new();
		dealerHand = new();
		winnerBox.text = string.Empty;
	}

	private void CheckWinner()
	{
		if (playerPoints >= 10)
		{
			winnerBox.text = "Player Won Game";
			StartCoroutine(WaitThreeSeconds(() => {
				RestartGame();
			}));
		}
		else if (dealerPoints >= 10)
		{
			winnerBox.text = "Dealer Won Game";
			StartCoroutine(WaitThreeSeconds(() => {
				RestartGame();
			}));
		}
	}

	private delegate void AfterThreeSeconds();
	private IEnumerator WaitThreeSeconds(AfterThreeSeconds afterDel = null)
	{
		yield return new WaitForSeconds(3f);
		afterDel?.Invoke();
	}

	private void ResetWinnerBoxText()
	{
		winnerBox.text = string.Empty;
	}
}
