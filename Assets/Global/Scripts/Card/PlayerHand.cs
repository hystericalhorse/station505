using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public GameObject cardPrefab;
    public int maxCards = 10;
    public float cardSpacing = 0.5f;
    public float cardSpawnHeight = 1f;
    public float cardSlantAngle = 10f;
    public float cardHoverDistance = 0.5f;
    public float maxSpreadDistance = 5f;

    private Deck deck;
    private List<GameObject> cards = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        deck = new Deck();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCardFromDeck();
        }

        // Update card positions based on mouse distance
        UpdateCardPositions();
    }

    void DrawCardFromDeck()
    {
        if (deck.Cards.Count > 0 && cards.Count < maxCards)
        {
            float xOffset = (float)cards.Count * cardSpacing - (float)(maxCards - 1) / 2.0f * cardSpacing;
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 6f + new Vector3(xOffset, -cardSpawnHeight, 0f);

            Card drawnCard = deck.Draw();
            GameObject newCard = Instantiate(cardPrefab, spawnPosition, Quaternion.identity);
            newCard.transform.rotation = Quaternion.Euler(0f, 0f, cardSlantAngle);
            newCard.transform.parent = transform;

            HoverCard hoverCard = newCard.AddComponent<HoverCard>();
            hoverCard.Initialize(cardHoverDistance);

            cards.Add(newCard);
        }
    }
    void UpdateCardPositions()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found.");
            return;
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Vector3.Distance(transform.position, mainCamera.transform.position);
        Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);

        GameObject closestCard = null;
        float closestDistance = float.MaxValue;

        // Find the closest card
        foreach (var card in cards)
        {
            float distanceToMouse = Vector3.Distance(worldMousePos, card.transform.position);

            if (distanceToMouse < closestDistance)
            {
                closestCard = card;
                closestDistance = distanceToMouse;
            }
        }

        // Adjust card positions based on the closest card
        foreach (var card in cards)
        {
            float spreadFactor = Mathf.Clamp01(1f - closestDistance / maxSpreadDistance);

            // Move other cards away from the closest one
            if (card != closestCard)
            {
                Vector3 targetPosition = card.transform.position + (card.transform.position - closestCard.transform.position) * spreadFactor;

                // Check if the card is behind the camera
                if (Vector3.Dot(targetPosition - mainCamera.transform.position, mainCamera.transform.forward) > 0)
                {
                    // Smoothly interpolate card positions towards the target position
                    card.transform.position = Vector3.Lerp(card.transform.position, targetPosition, Time.deltaTime * 5f);
                }
            }
        }
    }

}
