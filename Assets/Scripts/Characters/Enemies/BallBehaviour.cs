using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 impulseDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ApplyImpulse();
    }

    public void SetImpulseDirection (Vector3 direction)
    {
        impulseDirection = direction;
    }

    void ApplyImpulse()
    {
        rb.AddForce (impulseDirection, ForceMode.Impulse);
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
