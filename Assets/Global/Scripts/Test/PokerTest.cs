using UnityEngine;

public class PokerTest : MonoBehaviour, Interactable
{
	public Card[] cards = new Card[5];

	[ContextMenu("EvaluateHand")]
	public void EvaluateHand()
	{
		var evaluation = CardAlgorithms.EvaluateHand(cards);

		Debug.Log(evaluation.ToString());
	}

	public void OnInteract()
	{
		EvaluateHand();
	}
}