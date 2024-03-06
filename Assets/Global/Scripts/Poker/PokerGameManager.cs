using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokerGameManager : MonoBehaviour
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
    private List<Card> playerHand;

	// Dealer Hand 
	private List<Card> dealerHand;

    // Deck 
    private Deck deck = new();
    public PlayerHand playerHandScript;

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

        Card drawnCard = deck.Draw();

        playerHand.Add(drawnCard);

        AudioManager.instance.PlaySound("PlayCard");

        playerHandScript.DrawCardFromDeck(drawnCard);
        
    }

    // Deals Hand to Player and Dealer
    private void DealClicked()
    {
        RestartGame();

        deck.Shuffle();

		for (int i = 0; i < 5; i++)
        {
			playerHand.Add(deck.Draw());
			AudioManager.instance.PlaySound("PlayCard");
        }

        for (int i = 0; i < 5; i++)
        {
            dealerHand.Add(deck.Draw());
			AudioManager.instance.PlaySound("PlayCard");
        }

        foreach (var card in playerHand)
        {
            playerHandScript.DrawCardFromDeck(card);
        }
    }

    private void DetermineWinner()
    {
        PokerHand player = CardAlgorithms.EvaluateHand(playerHand.ToArray(), out var playerHighCard);
        PokerHand dealer = CardAlgorithms.EvaluateHand(dealerHand.ToArray(), out var dealerHighCard);

        if (player > dealer)
        {
            winnerBox.text = "Player Wins with " + CardAlgorithms.EvaluateHand(playerHand.ToArray(), out playerHighCard).ToString();
            GameManager.instance.SetMoney( GameManager.instance.GetMoney() + GameManager.instance.currentBet * 2);
            GameManager.instance.currentBet = 0;
            StartCoroutine(WaitThreeSeconds());
			GameManager.instance.BetUI.GetComponent<BetUIMenu>().BetReset();
			RestartGame();
            return;
        }
        if (player < dealer) 
        {
            winnerBox.text = "Dealer Wins with " + CardAlgorithms.EvaluateHand(playerHand.ToArray(), out dealerHighCard).ToString();
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
        playerHand = new();
        dealerHand = new();
        winnerBox.text = string.Empty;
        playerHandScript.DeleteAllCards();
        deck.hand = playerHandScript;
    }

    private IEnumerator WaitThreeSeconds()
    {
        yield return new WaitForSeconds(3f);
    }
}
