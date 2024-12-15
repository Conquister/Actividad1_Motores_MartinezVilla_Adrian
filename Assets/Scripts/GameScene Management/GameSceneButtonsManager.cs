using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneButtonsManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private Slider volumeSlider;
    private bool isPaused = false;
    private GameSceneAudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        // Asegurar que el menú está desactivado al inicio
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        audioManager = FindObjectOfType<GameSceneAudioManager>();
        if (audioManager == null)
        {
            Debug.LogError ("AudioManager no encontrado en la escena");
        }

        SetupVolumeSlider();
    }

    // Método para abrir o cerrar el menú
    public void TogglePauseMenu()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);

            if (pauseMenuPanel.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    private void SetupVolumeSlider()
    {
        if (volumeSlider != null && audioManager != null)
        {
            volumeSlider.value = audioManager.GetVolume();

            volumeSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
        else
        {
            Debug.LogWarning ("VolumeSlider o AudioManager no asignados.");
        }
    }

    public void SetVolume (float volume)
    {
        if (audioManager != null)
        {
            audioManager.SetVolume(volume);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;                    // Pausar el tiempo de juego
        isPaused = true;
        Debug.Log ("Juego pausado");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;                    // Reanudad el tiempo del juego
        isPaused = false;
        Debug.Log ("Juego reanudado");
    }

    // Método para cerrar el juego
    public void ExitGame()
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }
}
