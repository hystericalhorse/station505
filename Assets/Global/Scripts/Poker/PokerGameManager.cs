using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokerGameManager : MonoBehaviour
{
    public PokerGameManager me;

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
    public List<Card> playerHand;

	// Dealer Hand 
	private List<Card> dealerHand;

    // Deck 
    public Deck deck = new();
    public PlayerHand playerHandScript;

    // AI Instance
    private Wheeler wheeler;

    // Funny AutoFill
    private List<Card> hitList;

    // Start is called before the first frame update
    void Start()
    {
        wheeler = new Wheeler();
        me = this;

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
        playerHandScript.DrawCardFromDeck(drawnCard);

        AudioManager.instance.PlaySound("PlayCard"); 
    }

    // Deals Hand to Player and Dealer
    private void DealClicked()
    {
        RestartGame();

        deck.Shuffle();

		for (int i = 0; i < 5; i++)
        {
			playerHand.Add(deck.Draw());
			dealerHand.Add(deck.Draw());
			AudioManager.instance.PlaySound("PlayCard");
        }

        foreach (var card in playerHand)
        {
            playerHandScript.DrawCardFromDeck(card);
        }

        foreach (var card in playerHandScript.cards)
        {
            card.GetComponent<CardObject>().onInteract += () => {
                card.GetComponent<CardObject>().PokerDiscard(ref me);
            };
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
            StartCoroutine(WaitThreeSecondsThenRestart());
        }
        if (player < dealer) 
        {
            winnerBox.text = "Dealer Wins with " + CardAlgorithms.EvaluateHand(playerHand.ToArray(), out dealerHighCard).ToString();
            StartCoroutine(WaitThreeSecondsThenRestart());
		}

        if (player == dealer)
        {
            if (playerHighCard.rank > dealerHighCard.rank)
            {
                winnerBox.text = "Player Wins with " + playerHighCard.ToString();
				GameManager.instance.SetMoney(GameManager.instance.GetMoney() + GameManager.instance.currentBet * 2);
				StartCoroutine(WaitThreeSecondsThenRestart());
            }
            else if (playerHighCard.rank < dealerHighCard.rank)
            {
                winnerBox.text = "Dealer Wins with " + dealerHighCard.ToString();
                StartCoroutine(WaitThreeSecondsThenRestart());
			}
            else
            {
                winnerBox.text = "Dealer Wins Tie";
                StartCoroutine(WaitThreeSecondsThenRestart());
			}
        }
    }

    private void DealerTurn()
    {
        wheeler.myHand.hand = dealerHand;
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
    }

    private IEnumerator WaitThreeSecondsThenRestart()
    {
		GameManager.instance.currentBet = 0;
        yield return new WaitForSeconds(3f);
		GameManager.instance.BetUI.GetComponent<BetUIMenu>().UpdateValues();
		GameManager.instance.BetUI.GetComponent<BetUIMenu>().BetReset();
		RestartGame();
	}
}
