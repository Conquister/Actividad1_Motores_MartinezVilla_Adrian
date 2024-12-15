using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneButtonsManager : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPopup;             // Declaramos la variable 'howToPlayPop' de tipo GameObject para pasarle el GameObject 'HowToPlayPopup' de la escena en el Inspector
    [SerializeField] private Slider volumeSlider;                   // Declaramos la variable 'volumeSlider' de tipo Slider para pasarle el GameObject 'VolumeSlider' de la escena en el Inspector
    private AudioSource audioSource;                                // Declaramos la variable 'audioSource' de tipo AudioSource 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();                  // Inicializamos la variable 'audioSource' asignándole el componente AudioSource, que está en este mismo gameObject
        if (audioSource != null && volumeSlider != null)            // Comprobamos que las variables 'audioSource' y 'volumeSlider' no sean nulas
        {
            volumeSlider.value = audioSource.volume;                // Inicializamos el valor del 'volumeSlider' asignándole el volumen actual de audioSource
            volumeSlider.onValueChanged.AddListener(SetVolume);     // Añadimos un evento al 'volumeSlider' para que, cada vez que cambie el valor del Slider, llame al método SetVolume 
        }
       
        if (howToPlayPopup != null)                                 // Comprobamos si el GameObject 'HowToPlayPopup' de la UI está activado. 
        {
            howToPlayPopup.SetActive(false);                        // Como si entra aquí significa que está activado, la desactivamos. Queremos activarlo/desactivarlo por botón
        }
    }

    public void SetVolume (float volume)                            // Este método ajustará el valor del componente 'AudioSource' al valor recibido como parámetro, la variable volume de tipo float.
    {
        if (audioSource != null)                                    // Si audioSource no es nulo... Es decir, está asginado...
        {   
            audioSource.volume = volume;                            // Al valor de la propiedad 'Volume' del componente 'AudioSource' se le asigna el valor del parámetro 'volume'
        }
    }

    public void ToggleHowToPlayPopup ()                             // Este método activa/desactiva el 'HowToPlayPopup' de la UI. Se asignará al método OnClick del gameObject Button 'HowToPlayButton' de la UI
    {                                                       
        if (howToPlayPopup != null)                                 // Si el 'HowToPlayPopup' de la UI es nulo... Es decir, está inactivo...
        {
            howToPlayPopup.SetActive(!howToPlayPopup.activeSelf);   // Actívalo
        }
    }
    
    public void LoadGameScene()                                     // Este método carga la escena "Game". Se asigna al método OnClick del gameObject Button 'StartButton' de la UI
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame ()                                         // Este método cierra la aplicación, pero solo en la Build. De momento no está en uso.
    {
        Application.Quit();
        Debug.Log ("Cerrando el juego...");
    }
}
