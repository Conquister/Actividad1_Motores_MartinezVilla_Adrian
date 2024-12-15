using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySceneAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip victorySceneMusic;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource != null && victorySceneMusic != null)
        {
            audioSource.clip = victorySceneMusic;
            audioSource.loop = true;
            audioSource.Play();
        }    
        else
        {
            Debug.LogError ("AudioSource o VictorySceneMusic no asignados.");
        }
    }

    public void SetVolume (float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    public float GetVolume ()
    {
        return audioSource != null ? audioSource.volume : 0f;
    }
}
