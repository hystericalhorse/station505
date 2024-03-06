using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHand : MonoBehaviour
{
    public GameObject cardPrefab;
    public int maxCards = 10;
    public float cardSpacing = 0.5f;
    public float cardSpawnHeight = 1f;
    public float cardSlantAngle = 10f;
    public float cardHoverDistance = 0.5f;
    public float maxSpreadDistance = 5f;
    public bool Holo = false;

    public Transform tableTransform; // Reference to the table object

    public Deck deck;
    public List<GameObject> cards = new List<GameObject>();
    private bool isMovingCardToTable = false;

    void Start()
    {
        deck = new Deck();
    }

    void Update()
    {

        //UpdateCardPositions();
    }
    public void DrawCardFromDeck(Card drawnCard)
    {


        if (cards.Count < maxCards)
        {
            float xOffset = (float)cards.Count * cardSpacing - (float)(maxCards - 1) / 2.0f * cardSpacing;

            // Calculate the spawn position based on the current hand position
            Vector3 spawnPosition = transform.position + new Vector3(-0.5f, cardSpawnHeight, xOffset); ;

            // Instantiate a new card GameObject
            GameObject newCard = Instantiate(cardPrefab, spawnPosition, Quaternion.identity);
            newCard.transform.rotation = Quaternion.Euler(0f, 0f, cardSlantAngle);
            newCard.transform.parent = transform;

            // Add HoverCard component to the new card
            HoverCard hoverCard = newCard.AddComponent<HoverCard>();
            hoverCard.Initialize(cardHoverDistance);

            // Add the new card to the list of cards in the player's hand
            cards.Add(newCard);

            // Update the CardObject with the drawn card's information
            UpdateCardObject(newCard, drawnCard);
        }
    }


	public void UpdateCardObject(GameObject cardObject, Card card)
    {
        CardObject cardObjectScript = cardObject.GetComponent<CardObject>();

        if (cardObjectScript != null)
        {
            cardObjectScript.card = card;
        }
        else
        {
            Debug.LogWarning("CardObject script not found on the card GameObject.");
        }
    }

    public void FlipCard(uint index)
    {
        if (cards.Count <= index) return;

        cards[(int)index].GetComponent<CardObject>().FlipCard();
    }

    public void DeleteCard(GameObject cardObj)
    {
        cards.Remove(cardObj);
        Destroy(cardObj);

        UpdateHand();
    }

    public void UpdateHand()
    {
		//float xOffset = (float)cards.Count * cardSpacing - (float)(maxCards - 1) / 2.0f * cardSpacing;
		for (int i = 0; i < cards.Count; i++)
        {
            float xOffset = (float)(i+1) * cardSpacing - (float)(maxCards - 1) / 2.0f * cardSpacing;
            cards[i].transform.position = transform.position + new Vector3(-0.5f, cardSpawnHeight, xOffset);
            cards[i].GetComponent<HoverCard>().Initialize(cardHoverDistance);
		}
    }

    //void UpdateCardPositions()
    //{
    //    Camera mainCamera = Camera.main;

    //    if (mainCamera == null)
    //    {
    //        Debug.LogError("Main camera not found.");
    //        return;
    //    }

    //    Vector3 mousePos = Input.mousePosition;
    //    mousePos.z = Vector3.Distance(transform.position, mainCamera.transform.position);
    //    Vector3 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);

    //    GameObject closestCard = null;
    //    float closestDistance = float.MaxValue;

    //    foreach (var card in cards)
    //    {
    //        float distanceToMouse = Vector3.Distance(worldMousePos, card.transform.position);

    //        if (distanceToMouse < closestDistance)
    //        {
    //            closestCard = card;
    //            closestDistance = distanceToMouse;
    //        }
    //    }

    //    foreach (var card in cards)
    //    {
    //        float spreadFactor = Mathf.Clamp01(1f - closestDistance / maxSpreadDistance);

    //        if (card != closestCard)
    //        {
    //            Vector3 targetPosition = card.transform.position + (card.transform.position - closestCard.transform.position) * spreadFactor;
    //            targetPosition.y = cardSpawnHeight; // Keep the Y position fixed

    //            if (Vector3.Dot(targetPosition - mainCamera.transform.position, mainCamera.transform.forward) > 0)
    //            {
    //                card.transform.position = Vector3.Lerp(card.transform.position, targetPosition, Time.deltaTime * 5f);
    //            }
    //        }
    //    }

    //    //if (Input.GetMouseButtonDown(0) && closestCard != null && !isMovingCardToTable)
    //    //{
    //    //    cards.Remove(closestCard);
    //    //    StartCoroutine(MoveCardToTable(closestCard));
    //    //}
    //}

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

    public void DeleteAllCards()
    {
        foreach (var card in cards)
        {
            Destroy(card);
        }

        cards.Clear();
    }
}
