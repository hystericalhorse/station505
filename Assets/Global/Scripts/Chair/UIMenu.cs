using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    [SerializeField] Canvas Menu1;
    [SerializeField] Canvas Menu2;

    [SerializeField] ChairCamera chair;

	public void Awake()
	{

	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlackJack()
    {
        chair.anim.SetTrigger("TurnBack");
    }

    public void Poker()
    {
        chair.anim.SetTrigger("TurnBack");
    }

    public void GoFish()
    {
        chair.anim.SetTrigger("TurnBack");
    }

    public void Settings()
    {
        chair.anim.SetTrigger("TurnLeft");
    }

    public void Menu()
    {
        chair.anim.SetTrigger("TurnRight");
    }

    public void Eject()
    { 
        EndGame();
    }

    public void NormalCardSelect()
    {

    }

    public void HoloCardSelect()
    {

    }

    public void DeckOneSelect()
    {

    }

    public void DeckTwoSelect()
    {

    }

    public void DeckThreeSelect()
    {

    }

    public void DeckFourSelect()
    {

    }

    public void EndGame()
    {
        Application.Quit();
    }


}
