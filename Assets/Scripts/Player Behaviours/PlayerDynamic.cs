using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDynamic : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float force;

    private float hInput;
    private float vInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal"); 
        vInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(hInput, 0, vInput).normalized * force, ForceMode.Force);
    }
}
