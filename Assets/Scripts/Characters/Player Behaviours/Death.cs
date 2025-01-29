using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManagerSO;
    private Transform respawnPosition;
    private int enemyLayer = 3;
    private int powerUpLayer = 7; 
    private Rigidbody rb;
    private Renderer[] renderers;
    private float blinkInterval = 0.2f;
    private bool isInvulnerable = false;

    // Start is called before the first frame update
    void Start()
    {
        respawnPosition = new GameObject("RespawnPosition").transform;
        respawnPosition.position = transform.position;
        respawnPosition.rotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.layer == enemyLayer && !isInvulnerable)
        {
            gameManagerSO.PlayerHit();
            StartCoroutine(Respawn());
            StartCoroutine(Invulnerability(2f));
        }
        else if (other.gameObject.layer == powerUpLayer)
        {
            gameManagerSO.PlayerCured();
            Destroy(other.gameObject);
        }
    }
    
    private IEnumerator Respawn()
    {
        Debug.Log ("Respawneando jugador");
        rb.isKinematic = true;
        transform.position = respawnPosition.position;
        transform.rotation = respawnPosition.rotation;
        
        
        float timer = 0f;
        while (timer < 2f)
        {
            ToggleRenderers(false); // Apagar mallas
            yield return new WaitForSeconds(blinkInterval / 2);
            ToggleRenderers(true); // Encender mallas
            yield return new WaitForSeconds(blinkInterval / 2);
            timer += blinkInterval;
        }

        rb.isKinematic = false;
    }

    private void ToggleRenderers (bool state)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = state;
        }
    }

    private IEnumerator Invulnerability (float duration)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds (duration);
        isInvulnerable = false;
    }
}
