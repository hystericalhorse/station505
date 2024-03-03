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
    public float maxSpreadDistance = 5f;

    public Transform tableTransform; // Reference to the table object

    private Deck deck;
    private List<GameObject> cards = new List<GameObject>();
    private bool isMovingCardToTable = false;

    void Start()
    {
        deck = new Deck();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCardFromDeck();
        }

        UpdateCardPositions();
    }

    void DrawCardFromDeck()
    {
        if (deck.Cards.Count > 0 && cards.Count < maxCards)
        {
            float xOffset = (float)cards.Count * cardSpacing - (float)(maxCards - 1) / 2.0f * cardSpacing;

            Vector3 spawnPosition = new Vector3(xOffset, cardSpawnHeight, 6f);

            Card drawnCard = deck.Draw();
            GameObject newCard = Instantiate(cardPrefab, spawnPosition, Quaternion.identity);
            newCard.transform.rotation = Quaternion.Euler(cardSlantAngle, 0f, 0f);
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

        foreach (var card in cards)
        {
            float distanceToMouse = Vector3.Distance(worldMousePos, card.transform.position);

            if (distanceToMouse < closestDistance)
            {
                closestCard = card;
                closestDistance = distanceToMouse;
            }
        }

        foreach (var card in cards)
        {
            float spreadFactor = Mathf.Clamp01(1f - closestDistance / maxSpreadDistance);

            if (card != closestCard)
            {
                Vector3 targetPosition = card.transform.position + (card.transform.position - closestCard.transform.position) * spreadFactor;

                if (Vector3.Dot(targetPosition - mainCamera.transform.position, mainCamera.transform.forward) > 0)
                {
                    card.transform.position = Vector3.Lerp(card.transform.position, targetPosition, Time.deltaTime * 5f);
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && closestCard != null && !isMovingCardToTable)
        {
            cards.Remove(closestCard);
            StartCoroutine(MoveCardToTable(closestCard));
        }
    }

    IEnumerator MoveCardToTable(GameObject card)
    {
        isMovingCardToTable = true;

        float duration = 1f;
        float elapsed = 0f;
        Vector3 startPosition = card.transform.position;
        Vector3 targetPosition = tableTransform.position;

        while (elapsed < duration)
        {
            card.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.fixedDeltaTime;
            yield return null;
        }

        card.transform.rotation = Quaternion.identity;
        card.transform.SetParent(tableTransform);

        HoverCard hoverCard = card.GetComponent<HoverCard>();
        if (hoverCard != null)
        {
            hoverCard.StopHovering();
        }

        isMovingCardToTable = false;
    }
}
