using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoFishGameManager : MonoBehaviour
{
    // Game Buttons 
    public Button dealBtn;
    public Button askBtn;
    public Button betBtn;

    // Player Hand
    private List<Card> playerHand;

    // Opponent Hand (Dealer in this case)
    private List<Card> opponentHand;

    // Deck
    private List<Card> deck;

    void Start()
    {
        // Click Listeners 
        dealBtn.onClick.AddListener(() => DealClicked());
        askBtn.onClick.AddListener(() => AskClicked());
        betBtn.onClick.AddListener(() => AskClicked());

        InitializeDeck();
    }

    // Initialize the deck with all cards
    private void InitializeDeck()
    {
        deck = new List<Card>();

        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                deck.Add(new Card(s, r));
            }
        }

        
    }

    // Deals Cards to Player and Opponent
    private void DealClicked()
    {
        if (deck == null || deck.Count == 0)
        {
            Debug.Log("Deck is Empty/Null");
            return;
        }

        deck.Shuffle();

        playerHand = new List<Card>();
        opponentHand = new List<Card>();

        // Deal 7 cards to each player
        DealCards(playerHand, 7);
        DealCards(opponentHand, 7);
    }

    // Deal a specified number of cards to a player
    private void DealCards(List<Card> hand, int numCards)
    {
        for (int i = 0; i < numCards; i++)
        {
            hand.Add(deck[0]);
            deck.RemoveAt(0);
        }
    }

    // Ask for a card from the opponent
    private void AskClicked()
    {
        if (playerHand == null || opponentHand == null)
        {
            Debug.Log("Hands are not properly initialized");
            return;
        }

        // Assume the player always asks for the first rank in their hand
        Rank askedRank = playerHand[0].rank;

        // Check if the opponent has the asked rank
        List<Card> matchingCards = opponentHand.FindAll(card => card.rank == askedRank);

        if (matchingCards.Count > 0)
        {
            // Transfer the matching cards to the player
            playerHand.AddRange(matchingCards);
            opponentHand.RemoveAll(card => card.rank == askedRank);

            // TODO: Check for books in the player's hand and handle scoring
        }
        else
        {
            // If no matching cards, draw a card from the deck and end the player's turn
            DrawCard(playerHand);
        }
    }

    // Draw a card from the deck and add it to the player's hand
    private void DrawCard(List<Card> hand)
    {
        if (deck.Count > 0)
        {
            hand.Add(deck[0]);
            deck.RemoveAt(0);
        }
        else
        {
            Debug.Log("Deck is empty, cannot draw more cards");
        }
    }
}
