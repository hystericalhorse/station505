using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class UIMenu : MonoBehaviour
{
    [SerializeField] Canvas mainMenu;
    [SerializeField] Canvas settingsMenu;
    [SerializeField] Canvas cardMenu;
    [SerializeField] Canvas deckMenu;

    [SerializeField] GameObject blackJackUI;
    [SerializeField] GameObject pokerUI;
    [SerializeField] GameObject WarUI;
    [SerializeField] GameObject betUI;

    [SerializeField] public AudioSource robotVoiceBlackJack;
    [SerializeField] public AudioSource robotVoicePoker;
    [SerializeField] public AudioSource robotVoiceGoFish;

    private GameObject currentUI;

    public Transform spawnPoint;

    [SerializeField] TextMeshProUGUI currentDeck;
    [SerializeField] TextMeshProUGUI currentCardType;

    private Canvas currentMenu;

    [SerializeField] ChairCamera chair;

    [SerializeField] private BetUIMenu betMenu;

    public AudioMixer audioMixer;

    public PlayerHand hand;

    public GameObject HoloCard;
    public GameObject NormalCard;


	public void Awake()
	{
        currentMenu = mainMenu;
        settingsMenu.gameObject.SetActive(false);
        cardMenu.gameObject.SetActive(false);
        deckMenu.gameObject.SetActive(false);

        blackJackUI.gameObject.SetActive(false);
        pokerUI.gameObject.SetActive(false);
        WarUI.gameObject.SetActive(false);
	}

    public void GameSelected()
    {
        chair.anim.SetTrigger("TurnBack");
    }

    public void BlackJackSelected()
    {
        chair.anim.SetTrigger("TurnBack");
        //blackJackUI.gameObject.SetActive(true);
        betMenu.SetGameMenu(blackJackUI);
        betMenu.gameObject.SetActive(true);
        currentUI = blackJackUI;

        if (betMenu.quitGame.isPlaying)
        {
            betMenu.quitGame.Stop();
        }

        robotVoiceBlackJack.Play();
    }

	public void GoFishSelected()
	{
		chair.anim.SetTrigger("TurnBack");
        //goFishUI.gameObject.SetActive(true);
        betMenu.SetGameMenu(WarUI);
        betMenu.gameObject.SetActive(true);
        currentUI = WarUI;

		if (betMenu.quitGame.isPlaying)
		{
			betMenu.quitGame.Stop();
		}

		robotVoiceGoFish.Play();
	}

	public void PokerSelected()
	{
		chair.anim.SetTrigger("TurnBack");
		//pokerUI.gameObject.SetActive(true);
        betMenu.SetGameMenu(pokerUI);
        betMenu.gameObject.SetActive(true);
        currentUI = pokerUI;

		if (betMenu.quitGame.isPlaying)
		{
			betMenu.quitGame.Stop();
		}

		robotVoicePoker.Play();
	}

    public void disableCurrentGame()
    {
        currentUI.gameObject.SetActive(false);
    }

	public void Settings()
    {
		currentMenu.gameObject.SetActive(false);
		settingsMenu.gameObject.SetActive(true);
		currentMenu = settingsMenu;
	}

    public void Menu()
    {
        currentMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        currentMenu = mainMenu;
    }

    public void CardSettings()
    {
		currentMenu.gameObject.SetActive(false);
		cardMenu.gameObject.SetActive(true);
		currentMenu = cardMenu;
	}

    public void DeckSettings()
    {
		currentMenu.gameObject.SetActive(false);
		deckMenu.gameObject.SetActive(true);
		currentMenu = deckMenu;
	}

    public void Eject()
    { 
        EndGame();
    }

    public void NormalCardSelect()
    {
        currentCardType.text = "Normal";

        hand.cardPrefab = NormalCard;
    }

    public void HoloCardSelect()
    {
        currentCardType.text = "Holographic";

       hand.cardPrefab = HoloCard;
    }

    public void DeckOneSelect()
    {
        GameManager.instance.deckCount = 1;
        currentDeck.text = "1";
    }

    public void DeckTwoSelect()
    {
        GameManager.instance.deckCount = 2;
        currentDeck.text = "2";
    }

    public void DeckThreeSelect()
    {
        GameManager.instance.deckCount = 3;
        currentDeck.text = "3";
    }

    public void DeckFourSelect()
    {
        GameManager.instance.deckCount = 4;
        currentDeck.text = "4";
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }



}
