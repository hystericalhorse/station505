using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public TMP_Text cardText;
    public enum CardType
    {
        Hearts,
        Clubs,
        Diamonds,
        Spades
    }

    // Start is called before the first frame update
    void Start()
    {
        int cardNumber = Random.Range(1, 14);
        CardType randomCardType = (CardType)Random.Range(0, 4);

        Debug.Log("Card Type: " + randomCardType);
        Debug.Log("Card Number: " + cardNumber);

        FaceCamera();
        SetText(cardNumber, randomCardType);
    }

    void FaceCamera()
    {

        Camera mainCamera = Camera.main;


        if (mainCamera != null)
        {
            transform.LookAt(mainCamera.transform);
        }
        else
        {
            Debug.LogWarning("Main camera not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        FaceCamera();
    }

    public void SetText(int cardNumber, CardType randomCardType)
    {

        if (cardNumber == 11)
        {
            cardText.text = "Jack" + " " + randomCardType;
        }
        else if (cardNumber == 12)
        {
            cardText.text = "Queen" + " " + randomCardType;
        }
        else if (cardNumber == 13)
        {
            cardText.text = "King" + " " + randomCardType;
        }
        else
        {
            cardText.text = cardNumber.ToString() + " " + randomCardType;
        }
    }
}
