using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneAudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameSceneMusic;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource != null && gameSceneMusic != null)
        {
            audioSource.clip = gameSceneMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogError ("AudioSource o GameSceneMusic no asignados en el inspector.");
        }    
    }

    public void SetVolume (float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    public float GetVolume()
    {
        return audioSource != null ? audioSource.volume : 0f;
    }
}
