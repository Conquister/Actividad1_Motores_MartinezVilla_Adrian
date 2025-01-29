
using UnityEngine;

public class PunchingCylinderVert : MonoBehaviour
{
private Rigidbody rb;
    [SerializeField] private float RotationForce;
    [SerializeField] private Vector3 GlobalRotationAxis = Vector3.left;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
        rb.AddTorque(GlobalRotationAxis.normalized * RotationForce, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        
    }
}
