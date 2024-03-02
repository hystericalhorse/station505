using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource backgroundMusic;
    [Range(0f, 1f)]
    [SerializeField] float volume = 0.15f;

    private void Start()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
            backgroundMusic.volume = volume;
        }
    }

    private void Update()
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume;
        }
    }
}
