using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BetUIMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerMoneyText;
    [SerializeField] TextMeshProUGUI betText;
    [SerializeField] ChairCamera playerCamera;

    [SerializeField] public AudioSource quitGame;

    private GameObject currentMenu;
    private int startingMoney = 0;

    [SerializeField] private UIMenu uIMenu;

    void Start()
    {
        playerMoneyText.text = GameManager.instance.GetMoney().ToString();
        startingMoney = GameManager.instance.GetMoney();
    }

    public void ConfirmBet()
    {
        currentMenu.SetActive(true);
        gameObject.SetActive(false);
        startingMoney = GameManager.instance.GetMoney();
    }
	public void SetGameMenu(GameObject menu)
	{
        currentMenu = menu;
        currentMenu.SetActive(false);
	}

    public void EndGame()
    {
        if (uIMenu.robotVoiceBlackJack.isPlaying)
        {
            uIMenu.robotVoiceBlackJack.Stop();
        }

		if (uIMenu.robotVoicePoker.isPlaying)
		{
			uIMenu.robotVoicePoker.Stop();
		}

		if (uIMenu.robotVoiceGoFish.isPlaying)
		{
			uIMenu.robotVoiceGoFish.Stop();
		}

        quitGame.Play();
        currentMenu.SetActive(false);
        gameObject.SetActive(false);
        playerCamera.anim.SetTrigger("TurnForward");
	}

	public void Raise()
    {
        if(GameManager.instance.GetMoney() > 0)
        {
            GameManager.instance.SetMoney(GameManager.instance.GetMoney() - 100);
		    playerMoneyText.text = GameManager.instance.GetMoney().ToString();
            betText.text = (int.Parse(betText.text) + 100).ToString();
            GameManager.instance.currentBet += 100;
        }
	}

    public void Lower()
    {
        if(GameManager.instance.GetMoney() < startingMoney)
        {
		    GameManager.instance.SetMoney(GameManager.instance.GetMoney() + 100);
		    playerMoneyText.text = GameManager.instance.GetMoney().ToString();
		    betText.text = (int.Parse(betText.text) - 100).ToString();
            GameManager.instance.currentBet -= 100;
        }
	}

	public void BetReset()
	{
		currentMenu.SetActive(false);
        gameObject.SetActive(true);
        betText.text = GameManager.instance.currentBet.ToString();
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
