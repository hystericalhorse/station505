using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] Canvas mainMenu;
    [SerializeField] Canvas settingsMenu;
    [SerializeField] Canvas cardMenu;
    [SerializeField] Canvas deckMenu;

    [SerializeField] TextMeshProUGUI currentDeck;
    [SerializeField] TextMeshProUGUI currentCardType;

    private Canvas currentMenu;

    [SerializeField] ChairCamera chair;

	public void Awake()
	{
        currentMenu = mainMenu;
        settingsMenu.gameObject.SetActive(false);
        cardMenu.gameObject.SetActive(false);
        deckMenu.gameObject.SetActive(false);
	}

    public void GameSelected()
    {
        chair.anim.SetTrigger("TurnBack");
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
    }

    public void HoloCardSelect()
    {
        currentCardType.text = "Holographic";
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


}
