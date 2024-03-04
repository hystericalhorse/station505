using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleTest : MonoBehaviour, Interactable
{
    Deck deck;

    void Start()
    {
        deck = new();
    }

    public void OnInteract()
    {
        deck.Shuffle();
    }
}
