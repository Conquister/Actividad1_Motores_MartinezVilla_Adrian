using UnityEngine;
using UnityEngine.UI;

public class RadarController : MonoBehaviour
{
    public Transform player;        // Referencia al jugador (cápsula)
    public Transform target;        // Referencia a la esfera en la escena
    public RectTransform radar;     // El rectTransform de la imagen del radar
    public RectTransform blip;      // El rectTransform de la bolita en el radar
    public float radarRadius = 100f; // Radio del radar en píxeles
    public float detectionRadius = 10f; // Radio en metros que coincide con el trigger (SphereCollider)

    void Update()
    {
        // Obtener la diferencia de posición entre el jugador y el objetivo
        Vector3 toTarget = target.position - player.position;

        // Solo mostrar el blip si el objetivo está dentro del rango de detección
        if (toTarget.magnitude <= detectionRadius)
        {
            // Normalizar la dirección (para saber hacia dónde está la esfera respecto al jugador)
            Vector3 direction = toTarget.normalized;

            // Convertir la distancia en el mundo real a la escala de píxeles en el radar
            float scaledDistance = (toTarget.magnitude / detectionRadius) * radarRadius;

            // Calcular la posición en el radar en función de la distancia y dirección
            Vector2 radarPosition = new Vector2(direction.x, direction.z) * scaledDistance;

            // Limitar la posición al borde del radar si está fuera del rango
            if (radarPosition.magnitude > radarRadius)
            {
                radarPosition = radarPosition.normalized * radarRadius;
            }

            // Actualizar la posición de la bolita en el radar
            blip.anchoredPosition = radarPosition;

            // Mostrar el blip si estaba oculto
            blip.gameObject.SetActive(true);
        }
        else
        {
            // Ocultar el blip si el objetivo está fuera del rango
            blip.gameObject.SetActive(false);
        }
    }
}
