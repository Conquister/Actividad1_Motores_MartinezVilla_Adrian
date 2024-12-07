using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
        // VARIABLES "rotationSpeed" y "moviementSpeed"
    [Range(20,300)]                                                         // Serializamos la variable "rotationSpeed "para que aparezca en el Inspector
    [Tooltip("Variable de la velocidad de rotación de la cápsula")]         // Para que salga un mensaje de lo que hace esa variable al pasar con la flecha por encima en el Inspector
    public float rotationSpeed;                                      // Declaración e inicialización de la variable "rotationSpeed"
    
    [Range(1,20)]                                                           // Serializamos la variable "movementSpeed "para que aparezca en el Inspector
    [Tooltip("Variable de la velocidad de movimiento de la cápsula")]       // Para que salga un mensaje de lo que hace esa variable al pasar con la flecha por encima en el Inspector
    public float movementSpeed;                                        // Declaración e inicialización de la variable "rotationSpeed"

    // Referencia al CharacterController
    private CharacterController characterController;                        // Se crea una referncia al componente CharacterController del objecto que lleva el script, la Capsula, para controlar su movimiento

    void Start()
    {
        // Obtener el componente CharacterController
        characterController = GetComponent<CharacterController>();          // Tras el método Start, obtenemos el componente CharacterController del gameObject Capsula, que lleva este script
                                                                            // Esto nos permitirá manipular el movimiento de la cápsula a través de este script
    }

    void Update()                                                           // El método Update se ejecuta en cada frame
    {
        // Obtener valores de giro (Horizontal) y avance (Vertical) del input del jugador
        float horizontalInput = Input.GetAxis("Horizontal");                // Obtiene la entrada horizontal (A/D o Flechas izquierda/derecha).
        float verticalInput = Input.GetAxis("Vertical");                    // Obtiene la entrada vertical (W/S o Flechas arriba/abajo).

        // Calcular el giro del personaje usando el input horizontal
        float rotation = horizontalInput * rotationSpeed * Time.deltaTime;  // Calcula cuánto rotar la cápsula en base a la entrada horizontal multiplicada por la velocidad de rotación y Time.deltaTime.
                                                                            // Time.deltaTime asegura que el giro sea consistente independientemente de la tasa de fotogramas.
        transform.Rotate(0, rotation, 0);                                   // Rotar el GameObject en el eje Y (arriba/abajo) según el valor calculado de rotación.


        // Calcular el avance usando el input vertical
        Vector3 forwardMovement = transform.TransformDirection(Vector3.forward) * verticalInput * movementSpeed * Time.deltaTime;   // Calcula el movimiento hacia adelante de la cápsula en base a la entrada vertical y la velocidad de movimiento.
                                                                                                                                    // `transform.TransformDirection(Vector3.forward)` asegura que siempre se mueva en la dirección hacia adelante del objeto, sin importar su rotación.

        // Aplicar el movimiento al CharacterController
        characterController.Move(forwardMovement);                          // Esta línea aplica el movimiento calculado al CharacterController para mover el personaje en la escena.
                                                                            // El CharacterController maneja la física del movimiento, evitando colisiones o interferencias con otros objetos.
    }
}