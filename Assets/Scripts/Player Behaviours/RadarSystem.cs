using UnityEngine;
using UnityEngine.UI;

public class RadarSystem : MonoBehaviour
{
    [SerializeField] private Transform player;                                      // El jugador (cápsula)
    [SerializeField] private Transform [] spheres;                                  // Array de esferas
    [SerializeField] private RectTransform [] radarDots;                            // Array de bolitas en el radar
    [SerializeField] private RectTransform radarCenter;                             // El centro del radar (la flecha o jugador)
    [SerializeField] private float radarRadius = 50f;                               // El radio visual del radar en la UI
    [SerializeField] private float sphereColliderRadius = 10f;                      // El radio del SphereCollider de la cápsula (el jugador)

    void Update()
    {
        //----------------------------------------------COMPORTAMIENTO PARA EL RADAR--------------------------------------------------
        for (int i = 0; i < spheres.Length; i++) {
            Transform sphere = spheres[i];
            RectTransform radarDot = radarDots[i];
            
            
            if (sphere == null)                                                     // Si la esfera ya ha sido recogida (y destruida) la referencia en el array puede ser null
            {
                radarDot.gameObject.SetActive(false);                               // Aseguramos que la bolita del radar esté desactivada
                continue;
            }

            // Calcula la dirección y distancia desde el jugador hasta la esfera
            Vector3 direction = sphere.position - player.position;
            float distance = direction.magnitude;

            // Si la esfera está fuera del radio del collider, esconder la bolita del radar
            if (distance > sphereColliderRadius)
            {
                radarDot.gameObject.SetActive(false);
                continue;
            }

            radarDot.gameObject.SetActive(true);                                    // Asegúrate de que la bolita esté activa si la esfera está dentro del rango

            // Normalizar la dirección (2D, solo en X y Z para ignorar la altura)
            Vector2 directionNormalized = new Vector2(direction.x, direction.z).normalized;

            // Proyectar la posición de la esfera en el radar basado en la distancia, escalado según el radio del collider
            float scaledDistance = Mathf.Clamp01(distance / sphereColliderRadius);  // Escalar entre 0 y 1 según el radio del collider

            // Rotar la dirección según la rotación del jugador
            float playerRotationY = player.eulerAngles.y;                           // Obtiene la rotación Y del jugador
            float angleRadians = playerRotationY * Mathf.Deg2Rad;                   // Convertimos el ángulo a radianes

            // Rotamos la dirección utilizando trigonometría
            float rotatedX = directionNormalized.x * Mathf.Cos(angleRadians) - directionNormalized.y * Mathf.Sin(angleRadians);
            float rotatedY = directionNormalized.x * Mathf.Sin(angleRadians) + directionNormalized.y * Mathf.Cos(angleRadians);
            Vector2 rotatedDirection = new Vector2(rotatedX, rotatedY);

            // Calcular la posición de la bolita en el radar en la UI (ya ajustada con la rotación del jugador)
            Vector2 radarPosition = rotatedDirection * scaledDistance * radarRadius;

            // Asignar la posición relativa al centro del radar
            radarDot.anchoredPosition = radarPosition;
        }
        //-----------------------------------------------------------------------------------------------------------------------------
    }
}