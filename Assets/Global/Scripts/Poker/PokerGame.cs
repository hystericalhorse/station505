using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
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

    // UI
    [SerializeField] private TextMeshProUGUI winnerBox;

    // Player Hand
    private Card[] playerHand;

    // Dealer Hand 
    private Card[] dealerHand;

    // Deck 
    private Deck deck = new();

    // AI Instance
    private Wheeler wheeler;

    // Funny AutoFill
    private List<Card> hitList;

    // Start is called before the first frame update
    void Start()
    {
        wheeler = new Wheeler();

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
        winnerBox.text = "Player Folded. Dealer Wins";
        RestartGame();
    }

    private void StandClicked()
    {
        DealerTurn();
    }

    // Draws a Card Into the Player Hand
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

        AudioManager.instance.PlaySound("PlayCard");
    }

    // Deals Hand to Player and Dealer
    private void DealClicked()
    {
        if (deck == null || deck.Get.Count == 0)
        {
            Debug.Log("Deck is Empty/Null");
        }

        deck.Shuffle();

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
            playerHand[i] = deck.Get[0];
            deck.Get.RemoveAt(i);
            AudioManager.instance.PlaySound("PlayCard");
        }

        for (int i = 0; i < 5; i++)
        {
            dealerHand[i] = deck.Get[0];
            deck.Get.RemoveAt(i);
            AudioManager.instance.PlaySound("PlayCard");
        }
    }

    private void DetermineWinner()
    {
        PokerHand player = CardAlgorithms.EvaluateHand(playerHand, out var playerHighCard);
        PokerHand dealer = CardAlgorithms.EvaluateHand(dealerHand, out var dealerHighCard);

        if (player > dealer)
        {
            winnerBox.text = "Player Wins with " + CardAlgorithms.EvaluateHand(playerHand, out playerHighCard).ToString();
            GameManager.instance.SetMoney( GameManager.instance.GetMoney() + GameManager.instance.currentBet * 2);
            GameManager.instance.currentBet = 0;
            StartCoroutine(WaitThreeSeconds());
			GameManager.instance.BetUI.GetComponent<BetUIMenu>().BetReset();
			RestartGame();
            return;
        }
        if (player < dealer) 
        {
            winnerBox.text = "Dealer Wins with " + CardAlgorithms.EvaluateHand(playerHand, out dealerHighCard).ToString();
            GameManager.instance.currentBet = 0;
            StartCoroutine(WaitThreeSeconds());
			GameManager.instance.BetUI.GetComponent<BetUIMenu>().BetReset();
			RestartGame();
            return;
		}

        if (player == dealer)
        {
            if (playerHighCard.rank > dealerHighCard.rank)
            {
                winnerBox.text = "Player Wins with " + playerHighCard.ToString();
				GameManager.instance.SetMoney(GameManager.instance.GetMoney() + GameManager.instance.currentBet * 2);
				GameManager.instance.currentBet = 0;
				StartCoroutine(WaitThreeSeconds());
				GameManager.instance.BetUI.GetComponent<BetUIMenu>().BetReset();
				RestartGame();
                return;
            }
            else if (playerHighCard.rank < dealerHighCard.rank)
            {
                winnerBox.text = "Dealer Wins with " + dealerHighCard.ToString();
                GameManager.instance.currentBet = 0;
                StartCoroutine(WaitThreeSeconds());
                RestartGame();
				return;
			}
            else
            {
                // Split
                GameManager.instance.SetMoney(GameManager.instance.GetMoney() + GameManager.instance.currentBet);
                winnerBox.text = "Dealer Wins Tie";
                StartCoroutine(WaitThreeSeconds());
				GameManager.instance.BetUI.GetComponent<BetUIMenu>().BetReset();
				RestartGame();
				return;
			}
            
        }
    }

    private void DealerTurn()
    {
        wheeler.PlayPoker(ref deck, ref deck);

        DetermineWinner();
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
