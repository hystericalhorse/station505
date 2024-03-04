using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour, Interactable
{
	[SerializeField] string sound;

	public void OnInteract()
	{
		AudioManager.instance.PlayOverlappingSound(sound);
	}
}
