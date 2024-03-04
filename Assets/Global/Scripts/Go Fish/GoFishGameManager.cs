using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoFishGameManager : MonoBehaviour
{
    // Game Buttons 
    [SerializeField] public Button dealBtn;
    [SerializeField] public Button askBtn;
    [SerializeField] public Button betBtn;

    // SFX
    [SerializeField] private AudioSource placeCard;

    // UI
    [SerializeField] private TextMeshProUGUI winnerBox;

    // Player Hand
    private List<Card> playerHand;

    // Opponent Hand (Dealer in this case)
    private List<Card> opponentHand;

    private int playerScore;
    private int opponentScore;

    // Deck
    private Deck deck;

    // AI Instance
    private Wheeler wheeler;

    // Whether it is the Player Turn or not
    private bool isPlayerturn = true;

    void Start()
    {
        wheeler = new Wheeler();

        // Click Listeners 
        dealBtn.onClick.AddListener(() => DealClicked());
        askBtn.onClick.AddListener(() => AskClicked());
        betBtn.onClick.AddListener(() => AskClicked());

        InitializeDeck();
    }

    // Initialize the deck with all cards
    private void InitializeDeck()
    {
        deck = new Deck();

        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                deck.Get.Add(new Card(s, r));
            }
        }
    }

    // Deals Cards to Player and Opponent
    private void DealClicked()
    {
        if (deck == null || deck.Get.Count == 0)
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
            hand.Add(deck.Get[0]);
            deck.Get.RemoveAt(0);
            placeCard.Play();
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

            isPlayerturn = true;
            winnerBox.text = "Player can Go Again";

            // Check for books in the player's hand and handle scoring
            CheckForBooks(playerHand);
        }
        else
        {
            // If no matching cards, draw a card from the deck and end the player's turn
            DrawCard(playerHand);
            isPlayerturn = false;
            PassTurn();
        }
    }

    // Draw a card from the deck and add it to the player's hand
    private void DrawCard(List<Card> hand)
    {
        if (deck.Get.Count > 0)
        {
            hand.Add(deck.Get[0]);
            deck.Get.RemoveAt(0);
            placeCard.Play();
        }
        else
        {
            Debug.Log("Deck is empty, cannot draw more cards");
        }
    }

    private void PassTurn()
    {
        if (!isPlayerturn)
        {
            DealerTurn();
        }
        else
        {
            winnerBox.text = "It is Player's Turn";
            isPlayerturn = true;
        }
    }

    private void DealerTurn()
    {
        // Assume the player always asks for the first rank in their hand
        Rank askedRank = wheeler.PlayGoFish();

        // Check if the opponent has the asked rank
        List<Card> matchingCards = playerHand.FindAll(card => card.rank == askedRank);

        if (matchingCards.Count > 0)
        {
            // Transfer the matching cards to the player
            opponentHand.AddRange(matchingCards);
            playerHand.RemoveAll(card => card.rank == askedRank);

            isPlayerturn = false;
            winnerBox.text = "Dealer can Go Again";
            CheckForBooks(opponentHand);
        }
        else
        {
            // If no matching cards, draw a card from the deck and end the player's turn
            DrawCard(opponentHand);
            isPlayerturn = true;
            PassTurn();
        }
    }

    private void RestartGame()
    {
        playerHand = null;
        opponentHand = null;
        winnerBox.text = string.Empty;

        InitializeDeck();
    }

    private void CheckForBooks(List<Card> hand)
    {
        Dictionary<Rank, int> rankCount = new Dictionary<Rank, int>();

        // Count the occurrences of each rank in the hand
        foreach (Card card in hand)
        {
            if (rankCount.ContainsKey(card.rank))
            {
                rankCount[card.rank]++;
            }
            else
            {
                rankCount.Add(card.rank, 1);
            }
        }

        // Check for books (sets of four cards with the same rank)
        foreach (var kvp in rankCount)
        {
            if (kvp.Value == 4)
            {
                // Remove the four cards from the hand
                hand.RemoveAll(card => card.rank == kvp.Key);


                if (isPlayerturn == true)
                {
                    playerScore += 1;
                }
                else
                {
                    opponentScore += 1;
                }

                if (opponentScore + playerScore == 13)
                {
                    if (playerScore > opponentScore)
                    {
                        winnerBox.text = "Player Wins";
						GameManager.instance.SetMoney(GameManager.instance.GetMoney() + GameManager.instance.currentBet * 2);
						GameManager.instance.currentBet = 0;
					}
                    else if (playerScore < opponentScore)
                    {
                        winnerBox.text = "Dealer Wins";
						GameManager.instance.currentBet = 0;
					}
                    else
                    {
                        winnerBox.text = "Its A Tie";
                    }
                    WaitThreeSeconds();
                    RestartGame();
                }
            }
        }
    }

    private IEnumerator WaitThreeSeconds()
    {
        yield return new WaitForSeconds(3f);
    }

}
