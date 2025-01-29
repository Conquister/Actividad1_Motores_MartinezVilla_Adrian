using UnityEngine;

public class SphereSupport : MonoBehaviour
{
    [SerializeField] private SphereCollectorManager sphereCollectorManagerSO;
    [SerializeField] Transform spherePosition;          // Empty Object hijo, donde se instanciará la Esfera
    private bool playerDetected = false;
    private bool occupiedSupport = false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip spherePlacedSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            PlaceSphere();
        }    
    }

    private void PlaySound()
    {
        if (audioSource != null && spherePlacedSound != null)
        {
            audioSource.PlayOneShot(spherePlacedSound);
        }
    }
    void PlaceSphere()
    {
        foreach (GameObject sphere in SphereManager.allSpheres)
        {
            SphereState sphereState = sphere.GetComponent<SphereState>();

            Debug.Log($"Esfera encontrada: {sphere.name}, Activa: {sphere.activeSelf}");
            
            if (sphereState != null && !sphere.activeSelf && !sphereState.isPlaced && !occupiedSupport)                                         // Comprobamos si la esfera está desactivada
            {
                sphere.transform.position = spherePosition.position;        // La instanciamos en la posición del objeto hijo del soporte
                PlaySound();
                sphere.SetActive(true);                                     // La activamos para que se vea
                sphereState.isPlaced = true;                             
                sphereCollectorManagerSO.PlacedSphere();
                occupiedSupport = true;
                Debug.Log ($"Esfera colocada: {sphere.name}");
                return;
            }
        }
        Debug.LogWarning ("No se encontraron esferas desactivadas para colocar");
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerDetected = true;
            if (!occupiedSupport)
            {
                sphereCollectorManagerSO.ShowPlacedSphereBoxesAndMessages();
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
}
