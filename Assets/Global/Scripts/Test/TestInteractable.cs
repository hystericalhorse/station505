using UnityEngine;

public class TestInteractable : MonoBehaviour, Interactable
{
    public void OnInteract()
    {
        Debug.Log("Interact!");
    }
}
