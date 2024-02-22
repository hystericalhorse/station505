using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public GameObject cardPrefab; 
    public int maxCards = 10; 
    public float cardSpacing = 0.5f; 
    public float cardSpawnHeight = 1f; 
    public float cardSlantAngle = 10f; 
    public float cardHoverDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard();
        }
    }

    void DrawCard()
    {
        if (transform.childCount < maxCards)
        {
            
            float xOffset = (float)transform.childCount * cardSpacing - (float)(maxCards - 1) / 2.0f * cardSpacing;

            
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 6f + new Vector3(xOffset, -cardSpawnHeight, 0f);

            GameObject newCard = Instantiate(cardPrefab, spawnPosition, Quaternion.identity);

            
            newCard.transform.rotation = Quaternion.Euler(0f, 0f, cardSlantAngle);

            
            newCard.transform.parent = transform;

            // Attach the HoverCard script to the new card
            HoverCard hoverCard = newCard.AddComponent<HoverCard>();
            hoverCard.Initialize(cardHoverDistance); 
        }
    }
}