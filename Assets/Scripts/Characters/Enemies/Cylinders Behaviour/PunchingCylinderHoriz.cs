using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingCylinderHoriz : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float RotationForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
        rb.AddTorque(Vector3.up * RotationForce, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        
    }
}
