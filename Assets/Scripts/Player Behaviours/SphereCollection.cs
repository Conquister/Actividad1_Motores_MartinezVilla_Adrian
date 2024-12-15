using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereCollection : MonoBehaviour
{
    [SerializeField] private Image spheresCounterImage;                             // Referencia al componente Image del Canvas
    [SerializeField] private Sprite[] numberSprites;                                // Array de Sprites para los números 
    [SerializeField] private GameObject muroFinal;
    [SerializeField] private ParticleSystem successVFX;
    [SerializeField] private AudioClip rewardSound;                                 // Sonido de recompensa
    //[SerializeField] private ParticleSystem rewardBeam;
    
    public GameObject pickUpMessage;                                                // Mensaje "Pulsa E para recoger la esfera"
    public GameObject finalMessage;                                                 // Mensaje al recolectar las 7 esferas
    private int spheresCollected = 0;                                                // Contador de esferas recogidas
    private int totalSpheres = 7;                                                    // Total de esferas necesarias
    private GameObject currentSphere;                                                // Referencia a la esfera cercana
    private AudioSource audioSource;                                                // AudioSource para reproducir el sonido

    private void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Esfera")){
            currentSphere = other.gameObject;                                       // Guardamos la referencia de la esfera más cercana 
            pickUpMessage.SetActive(true);                                          // Mostrar el mensaje "Pulsa E para recoger esfera"
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Esfera"))
        {
            currentSphere = null;                                                   // Limpiamos la referencia al salir del Trigger
            pickUpMessage.SetActive (false);                                        // Ocultar el mensaje al salir del Trigger
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();                                  // Obtenemos el componente AudioSource                    
        UpdateCollectedSprite();
        pickUpMessage.SetActive(false);
        finalMessage.SetActive(false);
    }

    void Update()
    {
        if (currentSphere != null && Input.GetKeyDown(KeyCode.E))
        {
            CollectedSphere();
        }
    }
     private void CollectedSphere()
    {
        spheresCollected++;                                                         // Incrementamos el contador de esferas recogidas en 1
        UpdateCollectedSprite();                                                      // Actualizamos el texto en la UI
        audioSource.PlayOneShot(rewardSound);
        ParticleSystem[] sphereParticleSystems = currentSphere.GetComponentsInChildren<ParticleSystem>();
        
        foreach (var ps in sphereParticleSystems)
        {
            if(ps.name == "RewardBeam")
            {
                ps.Play();
                Destroy(currentSphere, ps.main.duration);
            }
        }
        
        //Destroy(currentSphere, rewardBeam.main.duration);                                                     // Destruimos la esfera recogida
        currentSphere = null;                                                       // Limpiamos la referencia de la esfera cercana
        pickUpMessage.SetActive(false);                                             // Ocultamos el mensaje de recogida

        if (spheresCollected == totalSpheres)
        {
            Destroy(muroFinal);
            finalMessage.SetActive(true);
            successVFX.Play();
        }
    }

    private void UpdateCollectedSprite()
    {
        if (spheresCollected >= 0 && spheresCollected < numberSprites.Length)
        {
            spheresCounterImage.sprite = numberSprites[spheresCollected];
        }
    }
}
