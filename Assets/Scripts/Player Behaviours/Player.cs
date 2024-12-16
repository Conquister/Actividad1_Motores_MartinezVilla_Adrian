using UnityEngine;

public class Player : MonoBehaviour
{
    //-------------------------------------------------------------------------DECLARACIÓN DE VARIABLES Y CREACIÓN DE REFERENCIAS--------------------------------------------------------------------------
    [SerializeField, Range(20,300)]                                                 
    [Tooltip("Velocidad de rotación del Player")]         
    private float rotationSpeed;                                      
    /* 
    Variable 'rotationSpeed'
    Además del SerializeField, para ver la variable en el Inspector, añadimos el atributo 'Range', otra técnica de serialización que sirve principalmente, para que alguien ajeno al código pueda testear diferentes 
    valores de una característica de una manera controlada. Sin el 'SerializeField' y solo con 'Range', la variable tendría que ser pública para ser visible desde el Inspector
    Con el atributo 'Tooltip', podemos crear una descripción de lo que hace el campo en cuestión desde el Inspector. Basta con mantener la flecha encima para que se despliegue un pop-up.    
    Declaramos la variable 'rotationSpeed' como privada y de tipo float. No la inicializamos, pues al estar serializada, se podrá configurar a gusto del consumidor desde el propio Inspector.
    */
    

    [SerializeField, Range(1,20)]                                                           // Serializamos la variable "movementSpeed "para que aparezca en el Inspector
    [Tooltip("Variable de la velocidad de movimiento de la cápsula")]       // Para que salga un mensaje de lo que hace esa variable al pasar con la flecha por encima en el Inspector
    private float movementSpeed;                                        // Declaración e inicialización de la variable "rotationSpeed"
    /* 
    Variable 'movementSpeed'
    Serializamos la variable con los atributos 'SerializeField y 'Range'.
    Volvemos a utilizar el atributo 'Tooltip'  
    Declaramos la variable 'movementSpeed' como privada y de tipo float, también sin inicializar.
    */
    
    private CharacterController characterController;                        
    // Creamos una referencia al componente CharacterController del objecto que lleva el script, en este caso el Player, para controlar su movimiento posteriormente
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // En el 1er frame, obtenemos el componente CharacterController del gameObject Player, lo que nos permitirá manipular el movimiento del mismo a través de este script          
    }


    void Update()
    {
        //----------------------------------------------------ASIGNACIÓN DEL INPUT HORIZONTAL Y VERTICAL A LOS EJES HORIZONTAL Y VERTICAL, RESPECTIVAMENTE------------------------------------------------------
        float hInput = Input.GetAxisRaw("Horizontal");  
        float vInput = Input.GetAxisRaw("Vertical");    
        /*
        Almacenamos en la variable de tipo float 'hInput' la entrada horizontal (A / D / <-- / -->). Es decir, -1 si pulsamos 'A' o '<--',  0 si no pulsamos nada, o 1 si pulsamos 'D' o '-->' 
        Almacenamos en la variable de tipo float 'vInput' la entrada vertical (W / S / ^ / v). Es decir, -1 si pulsamos 'S' o 'v', 0 si no pulsamos nada, o 1 si pulsamos 'W' o '^'
        */
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        //-------------------------------------------------------------------CALCULAMOS EL GIRO DEL PERSONAJE USANDO EL INPUT HORIZONTAL------------------------------------------------------------------------
        float rotation = hInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
        /*
        Qué estamos haciendo en estas 2 líneas?
        1ero.   Almacenamos un valor tipo float en la variable 'rotation' que va a ser igual a hInput (-1, 0 o 1) multiplicado por rotationSpeed multiplicado por Time.deltaTime (segundo/s)
        2o.     Rotamos el Player en el Y (La bailarina) según el valor de la variable 'rotation'
        En otras palabras, imaginemos una rotationSpeed de 5 grados por segundo:
            - Si pulsamos 'A' / '<--' : Giramos hacia nuestra izquierda a 5 grados/segundo --> -1 * 5 * s
            - Si no pulsamos nada : No rotamos --> 0 * 5 * s
            - SI pulsamos 'D' / '-->' : Giramos hacia nuestra derecha a 5 grados/segundo --> 1 * 5 * s
        */
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        //-------------------------------------------------------------------CALCULAMOS EL AVANCE DEL PERSONAJE USANDO EL INPUT VERTICAL------------------------------------------------------------------------
        Vector3 forwardMovement = transform.TransformDirection(Vector3.forward) * vInput * movementSpeed * Time.deltaTime;
        characterController.Move(forwardMovement);
        /*
        Qué estamos haciendo en estas 2 líneas?
        1ero.   Almacenamos el valor tipo Vector3 en la variable 'forwardMovement' que va a ser el Vector3.forward (0,0,1) multiplicado por vInput (-1, 0 o 1) multiplicado por movementSpeed multiplicado por Time.deltaTime (segundo/s):
                    *transform.TransformDirection(Vector3.forward) :  'TransformDirection' transforma la dirección local a global. Al pasarle Vector3.forward como vector, aseguramos que el Player siempre se mueva hacia delante, hacia donde esté rotado. 
        2o.     Utilizamos el método Move, nativo del componente characterController, pasándole como parámetro nuestro Vector3 'forwardMovement', que se encargará de aplicar el movimiento al componente CharacterController de Player para moverlo por la escena.
        En otras palabras, imaginemos un movementSpeed de 5 metros por segundo, trasladaremos a nuestro Player a través del método Move:
            - Si pulsamos 'W' / '^' : Avanzaremos hacia adelante a 5 m/s --> (0,0,1) * 1 * 5 * s
            - Si no pulsamos nada : No nos movemos --> (0,0,0) * 0 * 5 * s
            - Si pulsamos 'S' / 'v' : Retrocederemos hacia atrás a 5 m/s --> (0,0,1) * -1 * 5 * s
        */
         //-------------------------------------------------------------------CALCULAMOS EL AVANCE DEL PERSONAJE USANDO EL INPUT VERTICAL------------------------------------------------------------------------
    }
}