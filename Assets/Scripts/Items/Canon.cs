using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManagerSO;
    [SerializeField] private SphereCollectorManager sphereCollectorManagerSO;
    private Rigidbody otherRB;
    [SerializeField] private float impulseForce = 100f;
    private AudioSource audioSource;
    [SerializeField] private AudioClip canonExplosionSound;
    
    private bool isPlayerHere = false;
    private bool isActive = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerHere && Input.GetKeyDown(KeyCode.E) && isActive)
        {
            if (audioSource != null && canonExplosionSound != null)
            {
                audioSource.PlayOneShot(canonExplosionSound);
            }
            ThrowPlayer();
            gameManagerSO.MissionAccomplished();
        }
        
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && isActive)
        {
            sphereCollectorManagerSO.LetsGetOut();
            otherRB = other.GetComponent<Rigidbody>();
            isPlayerHere = true;
        }
    }

    private void ThrowPlayer()
    {
        if (otherRB != null)
        {
            otherRB.AddForce (new Vector3(0, 1, 1) * impulseForce , ForceMode.Impulse);
        }
        isPlayerHere = false;
    }

     private void OnEnable()
    {
        if (sphereCollectorManagerSO != null)
        {
            sphereCollectorManagerSO.OnAllSpheresPlaced += ActivateCanon;
        }
    }

    private void OnDisable()
    {
        if (sphereCollectorManagerSO != null)
        {
            sphereCollectorManagerSO.OnAllSpheresPlaced -= ActivateCanon;
        }
    }

    private void ActivateCanon()
    {
        isActive = true;
    }
}
