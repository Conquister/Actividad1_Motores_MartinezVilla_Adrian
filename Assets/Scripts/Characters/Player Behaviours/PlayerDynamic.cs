using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDynamic : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private float movementForce = 20f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float airControlForce = 5f;
    [SerializeField] private float rotationSpeed = 1f;
    private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    
    
    private float groundRadius = 0.4f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float playerHeight = 2f;

    private bool isGrounded;

    private float hInput;
    private float vInput;
    private float rotationY = 0f;

    private float distanciaDeteccionInteractuable = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento WASD
        hInput = Input.GetAxisRaw("Horizontal"); 
        vInput = Input.GetAxisRaw("Vertical");


        // Raycast para comprobar si el Player está en el suelo

        Vector3 sphereOrigin = transform.position + Vector3.down * (playerHeight / 2);
        isGrounded = Physics.CheckSphere(sphereOrigin, groundRadius, groundLayer);


        // Salto Space
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
            rb.AddForce(Vector3.up.normalized * jumpForce, ForceMode.Impulse);
        }

        // Rotación Mouse
        float mouseX = Input.GetAxis("Mouse X");
        Quaternion deltaRotation = Quaternion.Euler (0, mouseX * rotationSpeed, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);

        // Interactuar mediante raycast
        if(Input.GetKeyDown(KeyCode.E))
        {
            ThrowRaycast();
            
        }
    }

    void ThrowRaycast ()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distanciaDeteccionInteractuable))
            {
                if(hit.transform.TryGetComponent(out Palanca palanca))
                {
                    palanca.Interactuar();
                }
            }
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = transform.forward * vInput + transform.right * hInput;
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * movementForce, ForceMode.Force);
        }
        else
        {
            Vector3 airControl = Vector3.zero;
            if ((vInput < 0 && rb.velocity.z > 0) || (vInput > 0 && rb.velocity.z < 0))
            {
                airControl += transform.forward * vInput * airControlForce;
            }

            if ((hInput < 0 && rb.velocity.x > 0) || (hInput > 0 && rb.velocity.x < 0))
            {
                airControl += transform.right * hInput * airControlForce;
            }

            rb.AddForce(airControl, ForceMode.Force);
        }
    }
}
