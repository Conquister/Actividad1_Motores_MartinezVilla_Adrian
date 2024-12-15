using UnityEngine;
using UnityEngine.UI;

public class GameSceneButtonsManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;             // Declaramos la variable 'pauseMenuPanel' de tipo GameObject para pasarle el GameObject 'PauseMenuPanel' de la escena en el Inspector
    [SerializeField] private Slider volumeSlider;                   // Declaramos la variable 'volumeSlider' de tipo Slider para pasarle el GameObject 'VolumeSlider' de la escena en el Inspector
    private bool isPaused = false;                                  // Declaramos la variable 'isPaused' de tipo Bool, e inicializamos a false
    private GameSceneAudioManager audioManager;                     // Declaramos la variable 'audioManager' de tipo GameSceneAudioManager. Este es un script asignado a este mismo gameObject

    void Start()
    {
        if (pauseMenuPanel != null)                                 // Comprobamos si el GameObject 'PauseMenuPanel' de la UI está activado. 
        {
            pauseMenuPanel.SetActive(false);                        // Como si entra aquí significa que está activado, la desactivamos. Queremos activarlo/desactivarlo por botón
        }

        audioManager = GetComponent<GameSceneAudioManager>();       // Inicializamos la variable 'audioManager' asignándole el componente GameSceneAudioManager, otro script de este mismo gameObject

        SetupVolumeSlider();                                        // Llamamos al méotodo SetupVolumeSlider()
    }

    public void TogglePauseMenu()                                   // Este método activa/desactiva el 'PauseMenuPanel' de la UI. Se asignará al método OnClick del gameObject Button 'PauseButton' de la UI
    {
        if (pauseMenuPanel != null)                                 // Si el 'PauseMenuPanel' de la UI es nulo... Es decir, está inactivo...
        {
            pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);   // Con SetActive cambiamos el estado del PauseMenuPanel a activo/inactivo según corresponda. 
                                                                    // !pauseMenuPanel desactiva si el GameObject está activo, y lo activa si está inactivo.
            if (pauseMenuPanel.activeSelf)                          // Una vez cambiado el estado del GameObject 'PauseMenuPanel' comprobamos si está activo o no
            {
                PauseGame();                                        // Si está activo, llamamos al método PauseGame()
            }
            else                                                    // Si está inactivo, llamamos al método ResumeGame()
            {
                ResumeGame();
            }
        }
    }

    private void SetupVolumeSlider()                                // Con este método configuramos la función del GameObject 'VolumeSlider' de la UI
    {
        if (volumeSlider != null && audioManager != null)           // Comprobamos si el Slider 'VolumeSlider' y el Script 'GameSceneAudioManager' están correctamente asignados en el Inspector. 
        {
            volumeSlider.value = audioManager.GetVolume();          // Obtenemos el valor de la propiedad 'Volume', que sacamos del script 'GameSceneAudioManager' y lo asignamos a la propiedad
                                                                    // 'Value' del Slider 'VolumeSlider' de la UI
            volumeSlider.onValueChanged.RemoveAllListeners();       // Añadimos un evento al 'volumeSlider' para que elimine cualquier método previamente vinculado a este
            volumeSlider.onValueChanged.AddListener(SetVolume);     // Ahora que está reseteamos, volvemos a añadir un evento al 'volumeSlider' para que, cada vez que cambie el valor del Slider, 
        }                                                           // llame al método SetVolume y actualice
    }

    public void SetVolume (float volume)                            // Este método ajustará el valor del componente 'AudioSource' al valor recibido como parámetro, la variable volume de tipo float.
    {
        if (audioManager != null)                                   // Si audioSource no es nulo... Es decir, el componente 'AudioSource' está asginado...
        {
            audioManager.SetVolume(volume);                         // Al valor de la propiedad 'Volume' de este componente 'AudioSource' se le asigna el valor del parámetro 'volume'
        }
    }

    private void PauseGame()                                        // Este método sirve para controlar el estado del juego cuando el Player hace clic sobre el Botón "PauseButton" de la UI
    {
        Time.timeScale = 0f;                                        // El timeScale es una propiedad de la clase Time. El valor en ejecución es 1f. Al ponerlo en 0f, detenemos aspectos que 
                                                                    // dependan del tiempo de nuestro juego
        isPaused = true;                                            // Cambiamos el valor de nuestra variable de tipo Bool a true
    }

    private void ResumeGame()                                       // Este método sirve para controlar el estado del juego cuando el Player hace clic de nuevo sobre el Botón "PauseButton" de la UI
    {
        Time.timeScale = 1f;                                        // Al hacer clic de nuevo en el botón 'PauseButton' de la UI, volvemos a establecer el timeScale a 1f para que siga ejecutándose
        isPaused = false;                                           // Cambiamos el valor de nuestra variable de tipo Bool a false
    }

    public void ExitGame()                                          // Este método cierra la aplicación, pero solo en la Build. De momento no está en uso.
    {
        Debug.Log("Cerrando el juego...");
        Application.Quit();
    }
}
