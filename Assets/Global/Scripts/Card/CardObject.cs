using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    public Card card;
    public Renderer cardRenderer;  // Reference to the Renderer component for setting the texture
    public Material[] cardTextures; // Array of textures for each card

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
        Material material = cardTextures[0];
        if (card.suit == Suit.Spades)
        {
            if (card.rank == Rank.Ace)
            {
                material = cardTextures[0];
            }
            else if (card.rank == Rank.Two)
            {
                material = material = cardTextures[4];
            }
            else if (card.rank == Rank.Three)
            {
                material = material = cardTextures[8];
            }
            else if (card.rank == Rank.Four)
            {
                material = material = cardTextures[12];
            }
            else if (card.rank == Rank.Five)
            {
                material = material = cardTextures[16];
            }
            else if (card.rank == Rank.Six)
            {
                material = material = cardTextures[20];
            }
            else if (card.rank == Rank.Seven)
            {
                material = material = cardTextures[24];
            }
            else if (card.rank == Rank.Eight)
            {
                material = material = cardTextures[28];
            }
            else if (card.rank == Rank.Nine)
            {
                material = material = cardTextures[32];
            }
            else if (card.rank == Rank.Ten)
            {
                material = material = cardTextures[36];
            }
            else if (card.rank == Rank.Jack)
            {
                material = material = cardTextures[40];
            }
            else if (card.rank == Rank.Queen)
            {
                material = material = cardTextures[44];
            }
            else if (card.rank == Rank.King)
            {
                material = material = cardTextures[48];
            }
        }

        if (card.suit == Suit.Clubs)
        {
            if (card.rank == Rank.Ace)
            {
                material = cardTextures[1];
            }
            else if (card.rank == Rank.Two)
            {
                material = material = cardTextures[5];
            }
            else if (card.rank == Rank.Three)
            {
                material = material = cardTextures[9];
            }
            else if (card.rank == Rank.Four)
            {
                material = material = cardTextures[13];
            }
            else if (card.rank == Rank.Five)
            {
                material = material = cardTextures[17];
            }
            else if (card.rank == Rank.Six)
            {
                material = material = cardTextures[21];
            }
            else if (card.rank == Rank.Seven)
            {
                material = material = cardTextures[25];
            }
            else if (card.rank == Rank.Eight)
            {
                material = material = cardTextures[29];
            }
            else if (card.rank == Rank.Nine)
            {
                material = material = cardTextures[33];
            }
            else if (card.rank == Rank.Ten)
            {
                material = material = cardTextures[37];
            }
            else if (card.rank == Rank.Jack)
            {
                material = material = cardTextures[41];
            }
            else if (card.rank == Rank.Queen)
            {
                material = material = cardTextures[45];
            }
            else if (card.rank == Rank.King)
            {
                material = material = cardTextures[49];
            }
        }

        if (card.suit == Suit.Hearts)
        {
            if (card.rank == Rank.Ace)
            {
                material = cardTextures[2];
            }
            else if (card.rank == Rank.Two)
            {
                material = material = cardTextures[6];
            }
            else if (card.rank == Rank.Three)
            {
                material = material = cardTextures[10];
            }
            else if (card.rank == Rank.Four)
            {
                material = material = cardTextures[14];
            }
            else if (card.rank == Rank.Five)
            {
                material = material = cardTextures[18];
            }
            else if (card.rank == Rank.Six)
            {
                material = material = cardTextures[22];
            }
            else if (card.rank == Rank.Seven)
            {
                material = material = cardTextures[26];
            }
            else if (card.rank == Rank.Eight)
            {
                material = material = cardTextures[30];
            }
            else if (card.rank == Rank.Nine)
            {
                material = material = cardTextures[34];
            }
            else if (card.rank == Rank.Ten)
            {
                material = material = cardTextures[38];
            }
            else if (card.rank == Rank.Jack)
            {
                material = material = cardTextures[42];
            }
            else if (card.rank == Rank.Queen)
            {
                material = material = cardTextures[46];
            }
            else if (card.rank == Rank.King)
            {
                material = material = cardTextures[50];
            }
        }

        if (card.suit == Suit.Diamonds)
        {
            if (card.rank == Rank.Ace)
            {
                material = cardTextures[3];
            }
            else if (card.rank == Rank.Two)
            {
                material = material = cardTextures[7];
            }
            else if (card.rank == Rank.Three)
            {
                material = material = cardTextures[11];
            }
            else if (card.rank == Rank.Four)
            {
                material = material = cardTextures[15];
            }
            else if (card.rank == Rank.Five)
            {
                material = material = cardTextures[19];
            }
            else if (card.rank == Rank.Six)
            {
                material = material = cardTextures[23];
            }
            else if (card.rank == Rank.Seven)
            {
                material = material = cardTextures[27];
            }
            else if (card.rank == Rank.Eight)
            {
                material = material = cardTextures[31];
            }
            else if (card.rank == Rank.Nine)
            {
                material = material = cardTextures[35];
            }
            else if (card.rank == Rank.Ten)
            {
                material = material = cardTextures[39];
            }
            else if (card.rank == Rank.Jack)
            {
                material = material = cardTextures[43];
            }
            else if (card.rank == Rank.Queen)
            {
                material = material = cardTextures[47];
            }
            else if (card.rank == Rank.King)
            {
                material = material = cardTextures[51];
            }
        }


        cardRenderer.material = material;
    }
}
