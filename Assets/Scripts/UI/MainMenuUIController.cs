using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    [Header ("WINDOWS")]
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject HowToPlayMenu;
   
    
    [Header ("AUDIO")]
    [SerializeField] AudioMixer mainMixer;
    //private GameSceneAudioManager audioManager;                     // Declaramos la variable 'audioManager' de tipo GameSceneAudioManager. Este es un script asignado a este mismo gameObject
    [SerializeField] private AudioSource soundsAudioSource;
    [SerializeField] private AudioClip buttonClickSound;

    [Header("QUALITY")]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    
    [Header("RESOLUTION")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;                           // Array de resoluciones disponibles en nuestra pantalla
    private List<string> resolutionOptions = new List<string>();


    void Start()
    {
        if (HowToPlayMenu != null)                                 // Comprobamos si el GameObject 'PauseMenuPanel' de la UI está activado. 
        {
            HowToPlayMenu.SetActive(false);                        // Como si entra aquí significa que está activado, la desactivamos. Queremos activarlo/desactivarlo por botón
        }

        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }

        //audioManager = GetComponent<GameSceneAudioManager>();       // Inicializamos la variable 'audioManager' asignándole el componente GameSceneAudioManager, otro script de este mismo gameObject

        // MANERA PARA INICIALIZAR EL DROPDOWN QUALITY CON EL VALOR POR DEFECTO DE NIVEL DE CALIDAD DE NUESTRA CONFIGURACIÓN DE PANTALLA
        qualityDropdown.value = QualitySettings.GetQualityLevel();  
        qualityDropdown.RefreshShownValue();                        // Refrescamos los valores

        InitResolutionDropdown();
        // MANERA PARA INICIALIZAR DEL DROPDOWN QUALITY CON EL VALOR POR DEFECTO DE NUESTRA PANTALLA.
        // AQUÍ SÍ QUE ES NECESARIO TENER EL MÉTODO GETDEFAULTRESOLUTIO()
        resolutionDropdown.value = GetDefaulResolution();
        resolutionDropdown.RefreshShownValue();
    }

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
        Debug.Log ("Pantalla full screen");
        Screen.fullScreen = isFullScreen;
    }

    public void SetNewQualityLevel(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void OnHowToPlayMenuButtonClicked()                                   // Este método activa/desactiva el 'PauseMenuPanel' de la UI. Se asignará al método OnClick del gameObject Button 'PauseButton' de la UI
    {
        if (HowToPlayMenu != null)                                 // Si el 'PauseMenu' de la UI no es nulo... Es decir, existe...
        {
            HowToPlayMenu.SetActive(!HowToPlayMenu.activeSelf);         // Con SetActive cambiamos el estado del PauseMenuPanel a activo/inactivo según corresponda. 
                                                                    // !pauseMenuPanel desactiva si el GameObject está activo, y lo activa si está inactivo.
        }
    }

    public void OnSettingsButtonClicked()
    {
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(!settingsMenu.activeSelf);
        }
    }

    public void OnHTPBackButtonClicked()
    {
        HowToPlayMenu.SetActive(!HowToPlayMenu.activeSelf);
        
    }

    public void OnSettingsBackButtonClicked()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        
    }

    public void PlaySound()
    {
        if (soundsAudioSource != null && buttonClickSound != null)
        {
            soundsAudioSource.PlayOneShot(buttonClickSound);
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
   public void OnPlayButtonClicked()
   {
    Time.timeScale = 1f;
    SceneManager.LoadScene(1);
   }

    public void OnExitButtonClicked()                                          // Este método cierra la aplicación, pero solo en la Build. De momento no está en uso.
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }
}

