using UnityEngine;
using TMPro;

public class RadarSystem : MonoBehaviour
{
    [SerializeField] private Transform player;  // El jugador (cápsula)
    [SerializeField] private Transform [] spheres;  // Array de esferas
    [SerializeField] private RectTransform [] radarDots;  // Array de bolitas en el radar
    [SerializeField] private RectTransform radarCenter;  // El centro del radar (la flecha o jugador)
    [SerializeField] private float radarRadius = 50f;  // El radio visual del radar en la UI
    [SerializeField] private float sphereColliderRadius = 10f;  // El radio del SphereCollider de la cápsula (el jugador)



//-------------------------------------------------------FUTURAS ACTUALIZACIONES-------------------------------------------------------
    public GameObject messageText;  //Referencia al gameObject Text - TextMeshPro "FuturasActualizacionesTEXT" del Canvas
    public float displayTime = 3f;  //Duración del mensaje en pantalla
    private float timer;            //Temporizador

    private void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag("Esfera")){
            messageText.SetActive(true);            //Activa el mensaje
            timer = displayTime;                    //Resetea el temporizador
        }
        Debug.Log("Trigger activado por: " + other.name);
        
        if (other.CompareTag("Esfera"))
        {
        Debug.Log("Es la esfera");
        messageText.SetActive(true);
        timer = displayTime;
        }
    }
//--------------------------------------------------------------------------------------------------------------------------------------

    void Update()
    {

        //-----------------------------------------------FUTURAS ACTUALIZACIONES--------------------------------------------------------
        if (messageText.activeSelf){
            timer -= Time.deltaTime;                //Reduce el tiempo restante
            if (timer <= 0){
                messageText.SetActive(false);       //Desactiva el mensaje cuando se termine el tiempo (3f)
            }
        }        
        //------------------------------------------------------------------------------------------------------------------------------

        for (int i = 0; i < spheres.Length; i++) {
            Transform sphere = spheres[i];
            RectTransform radarDot = radarDots[i];
            // Calcula la dirección y distancia desde el jugador hasta la esfera
            Vector3 direction = sphere.position - player.position;
            float distance = direction.magnitude;

            // Si la esfera está fuera del radio del collider, esconder la bolita del radar
            if (distance > sphereColliderRadius)
            {
                radarDot.gameObject.SetActive(false);
                continue;
            }

            radarDot.gameObject.SetActive(true);  // Asegúrate de que la bolita esté activa si la esfera está dentro del rango

            // Normalizar la dirección (2D, solo en X y Z para ignorar la altura)
            Vector2 directionNormalized = new Vector2(direction.x, direction.z).normalized;

            // Proyectar la posición de la esfera en el radar basado en la distancia, escalado según el radio del collider
            float scaledDistance = Mathf.Clamp01(distance / sphereColliderRadius);  // Escalar entre 0 y 1 según el radio del collider

            // Rotar la dirección según la rotación del jugador
            float playerRotationY = player.eulerAngles.y;  // Obtiene la rotación Y del jugador
            float angleRadians = playerRotationY * Mathf.Deg2Rad;  // Convertimos el ángulo a radianes

            // Rotamos la dirección utilizando trigonometría
            float rotatedX = directionNormalized.x * Mathf.Cos(angleRadians) - directionNormalized.y * Mathf.Sin(angleRadians);
            float rotatedY = directionNormalized.x * Mathf.Sin(angleRadians) + directionNormalized.y * Mathf.Cos(angleRadians);
            Vector2 rotatedDirection = new Vector2(rotatedX, rotatedY);

            // Calcular la posición de la bolita en el radar en la UI (ya ajustada con la rotación del jugador)
            Vector2 radarPosition = rotatedDirection * scaledDistance * radarRadius;

            // Asignar la posición relativa al centro del radar
            radarDot.anchoredPosition = radarPosition;
        }
    }
}