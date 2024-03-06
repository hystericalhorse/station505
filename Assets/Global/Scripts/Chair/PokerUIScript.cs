using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokerUIScript : MonoBehaviour
{
    [SerializeField] Button hitBtn;
    [SerializeField] Button standBtn;
    [SerializeField] Button foldBtn;
    [SerializeField] Button dealBtn;
    // Start is called before the first frame update
    void Start()
    {
        hitBtn.gameObject.SetActive(false);
        standBtn.gameObject.SetActive(false);
        foldBtn.gameObject.SetActive(false);
    }

    public void deal()
    {
        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        foldBtn.gameObject.SetActive(true);
    }

    public void Stand()
    {
        hitBtn.gameObject.SetActive(false);
        standBtn.gameObject.SetActive(false);
        foldBtn.gameObject.SetActive(false);
        Invoke(nameof(UIReset), 3);
    }

	public void UIReset()
	{
        dealBtn.gameObject.SetActive(true);

	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
