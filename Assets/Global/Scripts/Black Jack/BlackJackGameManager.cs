using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class BlackJackGameManager : MonoBehaviour
{
    //Game Buttons 
    private Button dealBtn;
    private Button hitBtn;
    private Button standBtn;
    private Button betBtn;

    //Player Hand
    private Card[] playerHand;

    //Dealer Hand
    private Card[] dealerHand;

    //Deck
    private List<Card> deck;

    void Start()
    {
        // Click Listeners 
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        betBtn.onClick.AddListener(() => BetClicked());
    }

    void Update()
    {
        
    }

    private void BetClicked()
    {
        
    }

    private void StandClicked()
    {
        DealerTurn();
    }

    private void HitClicked()
    {
        if (deck == null || deck.Count == 0)
        {
            Debug.Log("Deck is Empty/Null");
        }

        Card drawnCard = deck[0];
        deck.RemoveAt(0);

        Array.Resize(ref playerHand, playerHand.Length + 1);
        playerHand[playerHand.Length - 1] = drawnCard;

        int handValue = GetCardValues(playerHand);
        if (handValue > 21) 
        {
            Debug.Log("Player Lost");
            DetermineWinner();
        }
    }

    private void DealClicked()
    {
        if (deck == null || deck.Count == 0)
        {
            Debug.Log("Deck is Empty/Null");
        }
        
        deck = deck.Shuffle();

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
            playerHand[i] = deck[0];
            deck.RemoveAt(i);
        }

        for (int i = 0; i < 2; i++)
        {
            dealerHand[i] = deck[0];
            deck.RemoveAt(i);
        }
    }

    private int GetCardValues(Card[] hand)
    {
        int value = 0;
        int aces = 0;

        foreach (Card card in hand) 
        { 
            int cardValue = card.GetValue();
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
        if (GetCardValues(dealerHand) < 17)
        {
            Card drawnCard = deck[0];
            deck.RemoveAt(0);
            Array.Resize(ref dealerHand, dealerHand.Length + 1);
            dealerHand[dealerHand.Length - 1] = drawnCard;
        }

        DetermineWinner();
    }

    private void DetermineWinner() 
    {
        int playerValue = GetCardValues(playerHand);
        int dealerValue = GetCardValues(dealerHand);

        if (playerValue > 21 && dealerValue < 21)
        {
            Debug.Log("Player Busts. Dealer Wins");
        } else if (playerValue < 21 && dealerValue > 21) 
        {
            Debug.Log("Dealer Busts. Player Wins");
        } else if (playerValue > dealerValue)
        {
            Debug.Log("Player Wins.");
        } else if (dealerValue > playerValue)
        {
            Debug.Log("Dealer Wins");
        }
        else
        {
            Debug.Log("It's a Draw");
        }
    }
}
