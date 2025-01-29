using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SphereCollection : MonoBehaviour
{
    [SerializeField] private Image spheresCounterImage;                             // Declaramos la variable 'spheresCounterImage' de tipo Image que referencia al GameObject 'SpheresCounterImage' de la UI
    [SerializeField] private Sprite[] numberSprites;                                // Declaramos el array 'numberSprites' de tipo Sprite que referencian a los Sprites del contador de esferas recogidas 
    [SerializeField] private GameObject muroFinal;                                  // Decelaramos la variable 'muroFinal' de tipo GameObject que hace referencia al GameObject 'MuroFinal' de la escena
    [SerializeField] private ParticleSystem successVFX;                             // Declaramos la variable 'successVFX' de tipo ParticleSystem que hace referencia al ParticleSystem 'SuccessVFX de la escena
    [SerializeField] private AudioClip rewardSound;                                 // Declaramos la variable 'rewardSound' de tipo AudioClip que hace referencia al AudioClip que se reproducirá al recoger una esfera
    
    public GameObject notificationsBox;
    public GameObject notificationsBackgroundImage;
    public GameObject pickUpMessage;                                                // Declaramos la variable pickUpMessage de tipo GameObject que hace referencia al GameObject 'PickUpMessageTEXT' de la UI
    public GameObject finalMessage;                                                 // Declaramos la variable 'finalMessage' de tipo GameObject que hace referencia al GameObject 'FinalMessageTEXT' de la UI
    public int spheresCollected = 0;                                               // Declaramos la variable 'spheresCollected' e inicializamos su valor a 0. Será el contador de esferas recogidas
    private int totalSpheres = 7;                                                   // Declaramos la variable 'totalSpheres' e incializamos su valor a 7. Será el número total de esferas
    private GameObject currentSphere;                                               // Declaramos la variable 'currentSphere' de tipo GameObject que hará referencia a la esfera que estemos gestionando en un momento concreto
    private AudioSource audioSource;                                                // Declaramos la variable 'audioSource' de tipo AudioSource

    private void OnTriggerEnter (Collider other)                                    // Este evento se dispara cuando otro GameObject de la escena entra en contacto con el componente 'Collider' asignado al GameObject que lleva este script
    {
        if(other.CompareTag("Esfera")){                                             // Comprobamos si la etiqueta del GameObject que entra en contacto con este GameObject es "Esfera" 
            SphereState sphereState = other.GetComponent<SphereState>();
            
            if (sphereState != null && !sphereState.isPlaced)
            {
            currentSphere = other.gameObject;                                       // Si lo es, guardamos la referencia de este GameObject con etiqueta "Esfera" en la variable 'currentSphere'
            notificationsBox.SetActive(true);
            notificationsBackgroundImage.SetActive(true);  
            pickUpMessage.SetActive(true);                                          // Si lo es, se activa el GameObject 'PickUpMessageTEXT' de la UI. Que muestra el mensaje: "Pulsa E para recoger esfera"
            }
                                                     
        }
    }

    private void OnTriggerExit (Collider other)                                     // Este evento se dispara cuando otro GameObject, ya dentro del radio nuestro componente 'Collider', sale de este 
    {
        if (other.CompareTag("Esfera"))                                             // Comprobamos si la etiqueta del GameObject que sale del radio de nuestro componente 'Collider' es "Esfera"
        {
            currentSphere = null;                                                   // Si lo es, el valor de la variable 'currentSphere' vuelve a ser null. Es decir, limpiamos la referencia de esa esfera
            notificationsBox.SetActive(false);
            notificationsBackgroundImage.SetActive(false); 
            pickUpMessage.SetActive (false);                                        // Si lo es, se desactiva el GameObject 'PickUpMessageTEXT' de la UI. 
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();                                  // Inicializamos la variable 'audioSource' asignándole el componente AudioSource, añadido también a este GameObject                   
        UpdateCollectedSprite();                                                    // Llamamos al método UpdateCollectedSprite()
        notificationsBox.SetActive(false);
        notificationsBackgroundImage.SetActive(false);                                              
        pickUpMessage.SetActive(false);                                             // Desactivamos el GameObject 'PickUpMessageTEXT' de la UI (por si estuviera activo por descuido)
        finalMessage.SetActive(false);                                              // Desactivamos el GameObject 'FinalMessageTEXT' de la UI (por si estuviera activo por descuido)
    }

    void Update()
    {
        if (currentSphere != null && Input.GetKeyDown(KeyCode.E))                   // Comprobamos si la referencia de currentSphere no es nula y evento en la tecla E. Es decir, si una esfera ha entrado
        {                                                                           // en el rango de nuestro componente 'Collider', currentSphere tendrá como referencia esa esfera.
            CollectedSphere();                                                      // Si una esfera entra en el rango de nuestro 'Collider', se disparará el OnTriggerEnter y se asigna una referencia a 'currentSphere'
        }                                                                           // Si, además, pulsamos E, entramos en el if. Y aquí llamamos al método CollectedSphere()
    }

     private void CollectedSphere()                                                 // Este método se llama cuando recogemos una esfera
    {
        spheresCollected++;                                                         // Incrementamos el contador de esferas recogidas, 'spheresCollected', en 1
        UpdateCollectedSprite();                                                    // Llamamos al método UpdateCollectedSprite(), que actualiza el Sprite del array 'numberSprites'
        audioSource.PlayOneShot(rewardSound);                                       // Se reproduce el método PlayOneShot del componente 'AudioSource' que contiene como parámetro el AudioClip 'rewardSound'
        ParticleSystem[] sphereParticleSystems = currentSphere.GetComponentsInChildren<ParticleSystem>();   // Se obtienen todos los ParticleSystem hijos de la esfera referenciada en 'currentSphere' y 
                                                                                                            // se almacenan en el array 'shpereParticleSystems' de tipo ParticleSystem 
        foreach (var parSystem in sphereParticleSystems)                                   // Para cada cada sistema de particulas encontrado en la línea anterior...
        {
            if(parSystem.gameObject.CompareTag("RewardBeam"))                                              // Si este se llama "RewardBeam"
            {
                parSystem.Play();                                                           // Lo reproducimos
            }
        }
        StartCoroutine(DesactivateSphereAfterDelay());
        

        if (spheresCollected == totalSpheres)                                       // Si el número de esferas recogidas, albergado en la variable 'spheresCollected' es igual a 'totalSpheres', 7, entramos en este if
        {
            Destroy(muroFinal);                                                     // El GameObject 'MuroFinal' de la escena se destruye
            notificationsBox.SetActive(true);
            notificationsBackgroundImage.SetActive(true);    
            finalMessage.SetActive(true);                                           // Se activa el GameObject FinalMessageTEXT
            successVFX.Play();                                                      // Se reproduce el ParticleSystem 'SuccessVFX' de la escena. El que contiene el 'Collider' con el Trigger para cambiar a la escena "Victory"
        }
    }

    private IEnumerator DesactivateSphereAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log($"Esfera recogida: {currentSphere.name}");
        currentSphere.SetActive(false);                                             // Desactivamos la esfera para que no se vea en escena
        Debug.Log($"Esfera desactivada: {currentSphere.name}");
        currentSphere = null;                                                       // Limpiamos la referencia de la esfera cercana
        notificationsBox.SetActive(false);
        notificationsBackgroundImage.SetActive(false);    
        pickUpMessage.SetActive(false);                                             // Ocultamos el mensaje de recogida
    }

    private void UpdateCollectedSprite()                                            // Este método se utiliza para actualizar el Sprite del array 'numberSprites'
    {
        if (spheresCollected >= 0 && spheresCollected < numberSprites.Length)       // Si el contador de esferas 'spheresCollected' es mayor o igual a 0 y menor a la cantidad del número de Sprites en el array numberSprites entramos en el if
        {
            spheresCounterImage.sprite = numberSprites[spheresCollected];           // Se asigna el Sprite del array 'numberSprites' que corresponda a las esferas recogias a la propiedad 'sprite' de la Imagen 'spheresCounterImage'
        }
    }
}
