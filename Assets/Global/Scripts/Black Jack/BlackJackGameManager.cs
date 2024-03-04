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
    private Card[] playerHand;

    //Dealer 
    private Wheeler wheeler;
    private Card[] dealerHand;

    //Deck
    private Deck deck = new();

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
            Debug.Log("Deck is Empty/Null");
        }

        Card drawnCard = deck.Get[0];
        deck.Get.RemoveAt(0);

        Array.Resize(ref playerHand, playerHand.Length + 1);
        playerHand[playerHand.Length - 1] = drawnCard;

        placeCard.Play();

        int handValue = GetCardValues(playerHand);
        if (handValue > 21) 
        {
            Debug.Log("Player Lost");
            DetermineWinner();
        }
    }

    // Deals Cards to Player and Dealer
    private void DealClicked()
    {
        if (deck == null || deck.Get.Count == 0)
        {
            Debug.Log("Deck is Empty/Null");
        }
        
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
            playerHand[i] = deck.Get[0];
            deck.Get.RemoveAt(i);
            AudioManager.instance.PlaySound("PlayCard");
        }

        for (int i = 0; i < 2; i++)
        {
            dealerHand[i] = deck.Get[0];
            deck.Get.RemoveAt(i);
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
        int playerValue = GetCardValues(playerHand);
        int dealerValue = GetCardValues(dealerHand);

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
        playerHand = null;
        dealerHand = null;
        winnerBox.text = string.Empty;
    }

    private IEnumerator WaitThreeSeconds()
    {
        yield return new WaitForSeconds(3f);
    }
}
