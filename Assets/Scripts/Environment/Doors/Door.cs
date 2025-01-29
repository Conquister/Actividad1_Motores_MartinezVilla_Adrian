using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameManagerSO GM;
    [SerializeField] private int idDoor;
    [SerializeField] private AudioClip openingDoorSound;
    private AudioSource audioSource;
    public GameObject childToDisable;
    public BoxCollider solidCollider;
    private bool bajar = false;
    private float speed = 1f;

    void Start ()
    {
        GM.OnActivadorActivado += ActivarDoor;
        audioSource = GetComponent<AudioSource>();
    }

    private void ActivarDoor(int idPulsadorPulsado)
    {
        if (idPulsadorPulsado == idDoor)
        {
            bajar = true;
            Debug.Log("BlueDoor activada");

            if(audioSource != null && openingDoorSound != null)
            {
                audioSource.PlayOneShot(openingDoorSound);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bajar && childToDisable != null)
        {
            Vector3 newPosition = childToDisable.transform.position + Vector3.down * speed * Time.deltaTime;
            childToDisable.transform.position = newPosition;

            if (childToDisable.transform.position.y <= -2f)
            {
                childToDisable.SetActive(false);
                if(solidCollider != null)
                {
                    solidCollider.enabled = false;
                    Debug.Log("Collider sólido desactivado");
                }
                bajar = false;
                Debug.Log("BlueDoor abierta");  
            }                   
        }
    }
    
    private void OnDestoy ()
    {
        GM.OnActivadorActivado -= ActivarDoor;
    }
}

