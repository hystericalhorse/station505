using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class BlackJackGameManager : MonoBehaviour
{
    //Game Buttons 
    [SerializeField] private Button dealBtn;
    [SerializeField] private Button hitBtn;
    [SerializeField] private Button standBtn;
    [SerializeField] private Button betBtn;

    // SFX
    [SerializeField] private AudioSource placeCard;

    // UI
    [SerializeField] private TextMeshProUGUI winnerBox;

    //Player Hand
    private List<Card> playerHand = new();

    //Dealer 
    private Wheeler wheeler;
    private List<Card> dealerHand = new();

    //Deck
    private Deck deck = new();
    public PlayerHand playerHandScript;

    void Start()
    {
        wheeler = new Wheeler();

        // Click Listeners 
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        betBtn.onClick.AddListener(() => BetClicked());
    }

    // Place How Much You Wish To Bet
    private void BetClicked()
    {
        
    }

    // The Player is Fine with Their Hand
    private void StandClicked()
    {
        DealerTurn();
    }

    // The Player Draws a Card
    private void HitClicked()
    {
        if (deck == null || deck.Get.Count == 0)
        {
            throw new Exception("Deck is null on HitClicked().");
        }

		Card drawnCard = deck.Draw();
		playerHand.Add(drawnCard);

		//placeCard.Play();

		int handValue = GetCardValues(playerHand.ToArray());
        if (handValue > 21) 
        {
            Debug.Log("Player Lost");
            DetermineWinner();
        }
    }

    // Deals Cards to Player and Dealer
    private void DealClicked()
    {
        RestartGame();
        deck.Shuffle();

        for (int i = 0; i < 2; i++) 
        {
            Card drawnCard = deck.Draw();
		    playerHand.Add(drawnCard);
            playerHandScript.DrawCardFromDeck(drawnCard);

			dealerHand.Add(deck.Draw());
			AudioManager.instance.PlaySound("PlayCard");
        }
    }

    // Grabs the Card Values of Respective Hand
    private int GetCardValues(Card[] hand)
    {
        int value = 0;
        int aces = 0;

        foreach (Card card in hand) 
        { 
            int cardValue = card.GetBlackjackValue();
            value += cardValue;

            if (card.rank == Rank.Ace)
            {
                aces++;
            }

            while (value > 21 && aces > 0)
            {
                value -= 10;
                aces--;
            }
        }

        return value;
    }

    private void DealerTurn()
    {
        wheeler.PlayBlackjack();

        DetermineWinner();
    }

    private void DetermineWinner() 
    {
        int playerValue = GetCardValues(playerHand.ToArray());
        int dealerValue = GetCardValues(dealerHand.ToArray());

        if (playerValue > 21 && dealerValue < 21)
        {
            winnerBox.text = "Player Busts. Dealer Wins";
            GameManager.instance.currentBet = 0;
            StartCoroutine(WaitThreeSeconds());
			GameManager.instance.BetUI.GetComponent<BetUIMenu>().BetReset();
			RestartGame();
        } else if (playerValue < 21 && dealerValue > 21) 
        {
            winnerBox.text = "Dealer Busts. Player Wins";
            GameManager.instance.SetMoney(GameManager.instance.GetMoney() + GameManager.instance.currentBet * 2);
            GameManager.instance.currentBet = 0;
            StartCoroutine(WaitThreeSeconds());
			GameManager.instance.BetUI.GetComponent<BetUIMenu>().BetReset();
			RestartGame();
        } else if (playerValue > dealerValue)
        {
            winnerBox.text = "Player Wins with " + playerValue.ToString();
            GameManager.instance.SetMoney(GameManager.instance.GetMoney() + GameManager.instance.currentBet * 2);
            GameManager.instance.currentBet = 0;
            StartCoroutine(WaitThreeSeconds());
			GameManager.instance.BetUI.GetComponent<BetUIMenu>().BetReset();
			RestartGame();
        } else if (dealerValue > playerValue)
        {
            winnerBox.text = "Dealer Wins with " + dealerValue.ToString();
            GameManager.instance.currentBet = 0;
            StartCoroutine(WaitThreeSeconds());
			GameManager.instance.BetUI.GetComponent<BetUIMenu>().BetReset();
			RestartGame();
        }
        else
        {
            winnerBox.text = "It's a Draw";
            StartCoroutine(WaitThreeSeconds());
			GameManager.instance.BetUI.GetComponent<BetUIMenu>().BetReset();
			RestartGame();
        }
    }

    private void RestartGame()
    {
        deck = new Deck();
        playerHand = new();
        dealerHand = new();
        winnerBox.text = string.Empty;
        playerHandScript.DeleteAllCards();
    }

    private IEnumerator WaitThreeSeconds()
    {
        yield return new WaitForSeconds(3f);
    }
}
