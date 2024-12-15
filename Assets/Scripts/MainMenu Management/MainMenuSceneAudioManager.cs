using UnityEngine;

public class MainMenuSceneAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip mainMenuMusic;           // Declaramos la variable 'mainMenuMusic' de tipo AudioClip
    private AudioSource audioSource;                            // Declaramos la variable 'audioSource' de tipo AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>();              // Inicializamos la variable 'audioSource' asignándole el componente AudioSource, que está en este mismo gameObject
        if (audioSource != null && mainMenuMusic != null)       // Comprobamos que las variables 'audioSource' y 'mainMenuMusic' no sean nulas
        {   
            audioSource.clip = mainMenuMusic;                   // Asignamos el archivo de audio almacenado en la variable 'mainMenuMusic' a la propiedad 'clip' del componente audioSource
            audioSource.loop = true;                            // Establecemos la propiedad 'loop' como true en el componente audioSource
            audioSource.Play();                                 // Iniciamos la reproducción
        }    
    }
}
