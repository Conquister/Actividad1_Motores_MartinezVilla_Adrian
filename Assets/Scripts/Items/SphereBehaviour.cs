using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour
{
    [SerializeField] private SphereCollectorManager sphereCollectorManagerSO;
    [SerializeField] private ParticleSystem rewardBeam;                         // Efecto visual al recogerse la esfera que contiene este script
    [SerializeField] private AudioClip rewardSound;                             // Sonido al recogerse la esfera que contiene este script
    private AudioSource audioSource;
    private bool isPlayerNearby = false;                                        // Bandera para controlar si el Player está cerca
    private bool isCollected = false;

    private void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            SphereState sphereState = GetComponent<SphereState>();
            
            if (sphereState != null && !sphereState.isPlaced)
            {
            // currentSphere = other.gameObject;                                       // Si lo es, guardamos la referencia de este GameObject con etiqueta "Esfera" en la variable 'currentSphere'
            sphereCollectorManagerSO.ShowPickUpBoxesAndMessages();                      // Notifica al Manager
            }
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown (KeyCode.E))
        {
            Collected();
        }
    }
    
    private void Collected()
    {
        if (isCollected) return;                                            // Si la esfera ya ha sido recogida, no hacer nada
        // Si enetramos aquí es porque la esfera se ha recodigo
        // Reproduce su VFX y sonido respectivo
        
        isCollected = true;
        Debug.Log ("Esfera recogida: " + gameObject.name);
        
        if (rewardBeam != null)
        {
            rewardBeam.Play();
        }    

        if (audioSource != null && rewardSound != null)
        {
            audioSource.PlayOneShot(rewardSound);
        }

        sphereCollectorManagerSO.CollectedSphere();         // Notificamos al sphereCollector ManagerSO que nos han recogido
        StartCoroutine (DeactivateAfterDelay());
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
