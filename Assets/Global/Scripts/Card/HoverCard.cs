using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoverCard : MonoBehaviour
{
    private Vector3 originalPosition;
    private float hoverDistance;
    public float hoverSpeed = 5f; // Speed of the card movement

    private bool isHovering = false;

    // Initialize the HoverCard script with the hover distance
    public void Initialize(float distance)
    {
        originalPosition = transform.position;
        hoverDistance = distance;
	}

    void OnMouseEnter()
    {
        isHovering = true;
    }
    
    void OnMouseExit()
    {
        isHovering = false;
    }

    public void Update()
    {
        // Move the card forward towards the camera over time when hovering
        if (isHovering)
        {
            float step = hoverSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, originalPosition - Camera.main.transform.forward * hoverDistance, step);
        }
        else
        {
            // Return the card to its original position over time when the mouse exits
            float step = hoverSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, originalPosition, step);
        }
    }
}