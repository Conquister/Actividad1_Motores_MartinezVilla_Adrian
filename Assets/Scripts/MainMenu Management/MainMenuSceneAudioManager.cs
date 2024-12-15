using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneAudioManager : MonoBehaviour
{

    [SerializeField] private AudioClip mainMenuMusic;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && mainMenuMusic != null)
        {
            audioSource.clip = mainMenuMusic;
            audioSource.loop = true;
            audioSource.Play();
        }    
    }

}
