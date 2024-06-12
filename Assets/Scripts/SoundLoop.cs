using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundLoop : MonoBehaviour
{
    public AudioClip[] clips; // Array of your sounds
    public AudioSource audioSource; // Reference to an AudioSource component
    public float volume = 0.5f; // Volume level

    private int currentClipIndex = 0; // Index of the current clip

    void Start()
    {
        // Set the initial volume
        audioSource.volume = volume;

        // Start playing the first sound
        StartCoroutine(PlayNextSound());
    }

    IEnumerator PlayNextSound()
    {
        // Set the clip of the AudioSource
        audioSource.clip = clips[currentClipIndex];

        // Play the clip
        audioSource.Play();

        // Wait for 30 seconds
        yield return new WaitForSeconds(30);

        // Move to the next clip index, looping back to 0 if it's the end of the array
        currentClipIndex = (currentClipIndex + 1) % clips.Length;

        // Play the next sound
        StartCoroutine(PlayNextSound());
    }
}
