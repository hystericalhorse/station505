using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokerGame : MonoBehaviour
{
    // Game Buttons
    [SerializeField] private Button dealBtn;
    [SerializeField] private Button hitBtn;
    [SerializeField] private Button foldBtn;
    [SerializeField] private Button standBtn;
    [SerializeField] private Button betBtn;

    // SFX
    [SerializeField] private AudioSource placeCard;

    // Player Hand
    private Card[] playerHand;

    // Dealer Hand 
    private Card[] dealerHand;

    // Deck 
    private List<Card> deck;

    // Funny AutoFill
    private List<Card> hitList;

    // Start is called before the first frame update
    void Start()
    {
        // Click Listeners 
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        foldBtn.onClick.AddListener(() => FoldClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        betBtn.onClick.AddListener(() => BetClicked());
    }

    private void BetClicked()
    {
        
    }

    private void FoldClicked()
    {
        Debug.Log("Dealer Wins");
    }

    private void StandClicked()
    {
        DetermineWinner();
    }

    // Draws a Card Into the Player Hand
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

        placeCard.Play();
    }

    // Deals Hand to Player and Dealer
    private void DealClicked()
    {
        if (deck == null || deck.Count == 0)
        {
            Debug.Log("Deck is Empty/Null");
        }

        deck = deck.Shuffle();

        if (playerHand == null)
        {
            playerHand = new Card[5];
        }

        if (dealerHand == null)
        {
            dealerHand = new Card[5];
        }

        for (int i = 0; i < 5; i++)
        {
            playerHand[i] = deck[0];
            deck.RemoveAt(i);
            placeCard.Play();
        }

        for (int i = 0; i < 5; i++)
        {
            dealerHand[i] = deck[0];
            deck.RemoveAt(i);
            placeCard.Play();
        }
    }

    private void DetermineWinner()
    {
        PokerHand player = CardAlgorithms.EvaluateHand(playerHand, out var playerHighCard);
        PokerHand dealer = CardAlgorithms.EvaluateHand(dealerHand, out var dealerHighCard);

        if (player > dealer)
        {
            // Player Wins
            return;
        }
        if (player < dealer) 
        {
			// Dealer Wins
			return;
		}

        if (player == dealer)
        {
            if (playerHighCard.rank > dealerHighCard.rank)
            {
                return;
            }
            else if (playerHighCard.rank < dealerHighCard.rank)
            {
				return;
			}
            else
            {
				// Split
				return;
			}
            
        }
    }
}
