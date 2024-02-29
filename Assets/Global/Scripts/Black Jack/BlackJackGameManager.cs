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
    [SerializeField] private Button dealBtn;
    [SerializeField] private Button hitBtn;
    [SerializeField] private Button standBtn;
    [SerializeField] private Button betBtn;

    //Player Hand
    private Card[] playerHand;

    //Dealer 
    private Wheeler wheeler;
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

    // Deals Cards to Player and Dealer
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
