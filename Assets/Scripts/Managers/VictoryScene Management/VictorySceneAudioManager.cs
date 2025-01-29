using UnityEngine;

public class VictorySceneAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;                   // Declaramos la variable 'audioSource' de tipo AudioSource, que se asignará desde el Inspector.
    [SerializeField] private AudioClip victorySceneMusic;               // Declaramos la variable 'victorySceneMusic' de tipo AudioClip, que se asignará desde el Inspector.

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource != null && victorySceneMusic != null)           // Comprobamos que las variables 'audioSource' y 'victorySceneMusic' no sean nulas
        {
            audioSource.clip = victorySceneMusic;                       // Asignamos el archivo de audio almacenado en la variable 'victorySceneMusic' a la propiedad 'clip' del componente audioSource
            audioSource.loop = true;                                    // Establecemos la propiedad 'loop' como true en el componente audioSource
            audioSource.Play();                                         // Iniciamos la reproducción
        }    
    }

    public void SetVolume (float volume)                                // Este método ajustará el valor del componente 'AudioSource' al valor recibido como parámetro, la variable volume de tipo float.
    {
        if (audioSource != null)                                        // Si audioSource no es nulo... Es decir, está asginado...
        {
            audioSource.volume = volume;                                // Al valor de la propiedad 'volume' del componente 'AudioSource' se le asigna el valor del parámetro 'volume'
        }
    }

    public float GetVolume ()                                           // Este método devolverá el valor actual de la propiedad 'Volume' del componente 'AudioSource'
    {
        return audioSource != null ? audioSource.volume : 0f;           // Si el audio no es nulo, devuelve el volumen actual. Si es nulo, devuelve 0 como valor por defecto
    }
}
