using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header ("WINDOWS")]
    [SerializeField] private GameObject pauseMenu;             // Declaramos la variable 'pauseMenuPanel' de tipo GameObject para pasarle el GameObject 'PauseMenuPanel' de la escena en el Inspector
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject exitoMainMenuButton;
    
    [Header ("AUDIO")]
    [SerializeField] AudioMixer mainMixer;
    private GameSceneAudioManager audioManager;                     // Declaramos la variable 'audioManager' de tipo GameSceneAudioManager. Este es un script asignado a este mismo gameObject
    private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip damagedSound;


    [Header("QUALITY")]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    
    [Header("RESOLUTION")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;                           // Array de resoluciones disponibles en nuestra pantalla
    private List<string> resolutionOptions = new List<string>();

    [Header("HUD")]
    [SerializeField] private GameManagerSO gameManagerSO;
    [SerializeField] private Image[] hearts;
    [SerializeField] private GameObject gameOverScreen;
    private int lives;

    private bool isPaused = false;                                  // Declaramos la variable 'isPaused' de tipo Bool, e inicializamos a false
    

    void Start()
    {
        Time.timeScale = 1f;
        lives = hearts.Length;

        if (gameManagerSO != null)
        {
            gameManagerSO.OnPlayerHit += PlayerHit;
            gameManagerSO.OnPlayerCured += PlayerCured;
        }

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
        
        if (pauseMenu != null)                                 // Comprobamos si el GameObject 'PauseMenuPanel' de la UI está activado. 
        {
            pauseMenu.SetActive(false);                        // Como si entra aquí significa que está activado, la desactivamos. Queremos activarlo/desactivarlo por botón
        }

        if (settingsMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        if (hud != null)
        {
            hud.SetActive(true);
        }

        audioManager = GetComponent<GameSceneAudioManager>();       // Inicializamos la variable 'audioManager' asignándole el componente GameSceneAudioManager, otro script de este mismo gameObject
        audioSource = GetComponent<AudioSource>();


        // MANERA PARA INICIALIZAR EL DROPDOWN QUALITY CON EL VALOR POR DEFECTO DE NIVEL DE CALIDAD DE NUESTRA CONFIGURACIÓN DE PANTALLA
        qualityDropdown.value = QualitySettings.GetQualityLevel();  
        qualityDropdown.RefreshShownValue();                        // Refrescamos los valores

        InitResolutionDropdown();
        // MANERA PARA INICIALIZAR DEL DROPDOWN QUALITY CON EL VALOR POR DEFECTO DE NUESTRA PANTALLA.
        // AQUÍ SÍ QUE ES NECESARIO TENER EL MÉTODO GETDEFAULTRESOLUTIO()
        resolutionDropdown.value = GetDefaulResolution();
        resolutionDropdown.RefreshShownValue();
    }

    public void PlaySound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
    // MÉTODOS DE QUALITY, RESOLUTION Y SLIDERS DE SONIDO
    private int GetDefaulResolution()
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                return i;
            }
        }
        return 0;
    }

    private void InitResolutionDropdown()
    {
        resolutions = Screen.resolutions;
        foreach (var resolution in resolutions)
        {
            resolutionOptions.Add(resolution.width + "x" + resolution.height);
        }
        resolutionDropdown.AddOptions(resolutionOptions);
    }

    public void SetNewResolution (int resolutionIndex)
    {
        Resolution chosenResolution = resolutions[resolutionIndex];
        Screen.SetResolution(chosenResolution.width, chosenResolution.height, Screen.fullScreen);
    }

    public void SetNewFullScreenState(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetNewQualityLevel(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void TogglePauseMenu()                                   // Este método activa/desactiva el 'PauseMenuPanel' de la UI. Se asignará al método OnClick del gameObject Button 'PauseButton' de la UI
    {
        if (pauseMenu != null)                                 // Si el 'PauseMenu' de la UI no es nulo... Es decir, existe...
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);         // Con SetActive cambiamos el estado del PauseMenuPanel a activo/inactivo según corresponda. 
                                                                    // !pauseMenuPanel desactiva si el GameObject está activo, y lo activa si está inactivo.
            pauseButton.SetActive(!pauseButton.activeSelf);
            if (pauseMenu.activeSelf)                          // Una vez cambiado el estado del GameObject 'PauseMenuPanel' comprobamos si está activo o no
            {
                PauseGame();                                        // Si está activo, llamamos al método PauseGame()
            }
            else                                                    // Si está inactivo, llamamos al método ResumeGame()
            {
                ResumeGame();
            }
        }else
        {
            pauseMenu.SetActive(false);
        }
    }

    public void ToggleSettingsMenu()
    {
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(!settingsMenu.activeSelf);
        }
    }

    // AJUSTES DE MÚSICA Y SONIDO
    public void SetNewVolumeToMusic(float volume)                                
    {
        mainMixer.SetFloat("musicVolume", volume);                                                     
    }

    public void SetNewVolumeToSounds(float volume)                                
    {
        mainMixer.SetFloat("soundsVolume", volume);                                                     
    }

    // AJUSTES DE ESTADO DEL JUEGO
    private void PauseGame()                                        // Este método sirve para controlar el estado del juego cuando el Player hace clic sobre el Botón "PauseButton" de la UI
    {
        Time.timeScale = 0f;                                        // El timeScale es una propiedad de la clase Time. El valor en ejecución es 1f. Al ponerlo en 0f, detenemos aspectos que 
                                                                    // dependan del tiempo de nuestro juego
        isPaused = true;                                            // Cambiamos el valor de nuestra variable de tipo Bool a true

        if(hud != null)
        {
            hud.SetActive(!hud.activeSelf);
        }
    }

    private void ResumeGame()                                       // Este método sirve para controlar el estado del juego cuando el Player hace clic de nuevo sobre el Botón "PauseButton" de la UI
    {
        Time.timeScale = 1f;                                        // Al hacer clic de nuevo en el botón 'PauseButton' de la UI, volvemos a establecer el timeScale a 1f para que siga ejecutándose
        isPaused = false;                                           // Cambiamos el valor de nuestra variable de tipo Bool a false
        
        if(hud != null)
        {
            hud.SetActive(!hud.activeSelf);
        }
    }

    public void ExitGame()                                          // Este método cierra la aplicación, pero solo en la Build. De momento no está en uso.
    {
        SceneManager.LoadScene(0);
    }

    private void PlayerHit()
    {
        
        if (lives > 0)
        {
            if (audioSource != null && damagedSound != null)
            {
                audioSource.PlayOneShot(damagedSound);
            }
            lives --;
            hearts[lives].enabled = false;

            if (lives <= 0)
            {
                ShowGameOverScreen();
            }
        }
    }

    private void PlayerCured()
    {
        Debug.Log("He entrado aquí");
        if (lives < hearts.Length)
        {
            hearts[lives].enabled = true;
            lives ++;
        }
    }

    private void ShowGameOverScreen()
    {
        Debug.Log ("GAME OVER");
        PauseGame();
        gameOverScreen.SetActive(true);
    }

    private void OnDestroy()
    {
        if (gameManagerSO != null)
        {
            gameManagerSO.OnPlayerHit -= PlayerHit;
            gameManagerSO.OnPlayerCured -= PlayerCured;
        }
    }
}

