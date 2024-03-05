using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    public Card card;
    public Renderer cardRenderer;  // Reference to the Renderer component for setting the texture
    public Texture[] cardTextures; // Array of textures for each card

    // Start is called before the first frame update
    void Start()
    {
        FaceCamera();
        SetTexture(card.rank, card.suit);
    }

    void FaceCamera()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
            transform.LookAt(mainCamera.transform);
        else
            Debug.LogWarning("Main camera not found.");
    }

    // Update is called once per frame
    void Update()
    {
        FaceCamera();
    }

    public void SetTexture(Rank rank, Suit suit)
    {
        if (cardRenderer == null)
        {
            Debug.LogError("No Renderer component assigned.");
            return;
        }

        if (card.suit == Suit.Clubs)
        {
            
        }
    }
}
