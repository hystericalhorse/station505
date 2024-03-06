using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackUI : MonoBehaviour
{
    [SerializeField] Button dealBTN;
    [SerializeField] Button standBTN;
    [SerializeField] Button hitBTN;
    [SerializeField] Button DealBTN;
    // Start is called before the first frame update
    void Start()
    {
        standBTN.gameObject.SetActive(false);
        hitBTN.gameObject.SetActive(false);
    }

    public void Deal()
    {
        standBTN.gameObject.SetActive(true);
        hitBTN.gameObject.SetActive(true);
        dealBTN.gameObject.SetActive(false);
        dealBTN.gameObject.SetActive(false);
    }

    public void Stand()
    {
        hitBTN.gameObject.SetActive(false);
        standBTN.gameObject.SetActive(false);
        Invoke(nameof(UIReset), 3);
    }

	public void UIReset()
	{
        dealBTN.gameObject.SetActive(true);
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
