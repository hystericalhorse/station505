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

    [SerializeField] private UIMenu uIMenu;

    void Start()
    {
        
    }

    public void ConfirmBet()
    {
        currentMenu.SetActive(true);
        gameObject.SetActive(false);
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

    }

    public void Lower()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
