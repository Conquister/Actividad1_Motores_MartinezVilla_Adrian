using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;           // Referencia al AudioSource principal

    [Header("Melodías de cada escena")]
    [SerializeField] private AudioClip mainMenuSceneMusic;       // Música del MainMenuScene
    [SerializeField] private AudioClip gameSceneMusic;           // Música del gameSceneMusic
    [SerializeField] private AudioClip VictorySceneMusic;          // Música del VictoryScene

 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource no asignado al AudioManager.");
        }
        Debug.Log("Escena activa al inciiar: " + SceneManager.GetActiveScene().name);

        UpdateMusic(SceneManager.GetActiveScene().name);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        UpdateMusic(scene.name);        // Cambiar la melodía según la escena
    }


    private void UpdateMusic (string sceneName)
    {
        Debug.Log("Cambiando música para la escena: " + sceneName);
        AudioClip clipToPlay = null;

        // Determinar qué melodía reproducir según la escena
        switch (sceneName)
        {
            case "MainMenu":
                clipToPlay = mainMenuSceneMusic;
                break;
            case "Game":
                clipToPlay = gameSceneMusic;
                break;
            case "Victory":
                clipToPlay = VictorySceneMusic;
                break;
            default:
                Debug.LogWarning("No hay música asignada para esta escena).");
                return;
        }

        if (audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
            Debug.Log ("Reproduciendo: " + clipToPlay.name);
        }
    }


    // Método para establecer el volumen actual
    public void SetVolume(float volume)
    {
        if(audioSource != null)
        {
            audioSource.volume = volume;                            // Ajustar el volumen del AudioSource
        }
    }

    // Método para obtener el volumen actual
    public float GetVolume ()
    {
        return audioSource != null ? audioSource.volume : 0f;
    }


    // Método para reproducir un efecto de sonido
    public void PlayAudio (AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);                          // Reproducir un efecto de sonido
        }
    }
}
