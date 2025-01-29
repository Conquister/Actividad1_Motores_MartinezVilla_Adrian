using UnityEngine;

public class GameSceneAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip gameSceneMusic;              // Declaramos la variable 'gameSceneMusic' de tipo AudioClip
    private AudioSource audioSource;                                // Declaramos la variable 'audioSource' de tipo AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>();                  // Inicializamos la variable 'audioSource' asignándole el componente AudioSource, que está en este mismo gameObject
        if (audioSource != null && gameSceneMusic != null)          // Comprobamos que las variables 'audioSource' y 'gameSceneMusic' no sean nulas
        {
            audioSource.clip = gameSceneMusic;                      // Asignamos el archivo de audio almacenado en la variable 'gameSceneMusic' a la propiedad 'clip' del componente audioSource
            audioSource.loop = true;                                // Establecemos la propiedad 'loop' como true en el componente audioSource
            audioSource.Play();                                     // Iniciamos la reproducción
        }
    }

    public void SetVolume (float volume)                            // Este método ajustará el valor del componente 'AudioSource' al valor recibido como parámetro, la variable volume de tipo float.
    {
        if (audioSource != null)                                    // Si audioSource no es nulo... Es decir, está asginado...
        {
            audioSource.volume = volume;                            // Al valor de la propiedad 'volume' del componente 'AudioSource' se le asigna el valor del parámetro 'volume'
        }
    }

    public float GetVolume()                                        // Este método devolverá el valor actual de la propiedad 'Volume' del componente 'AudioSource'
    {
        return audioSource != null ? audioSource.volume : 0f;       // Si el audio no es nulo, devuelve el volumen actual. Si es nulo, devuelve 0 como valor por defecto
    }
}
