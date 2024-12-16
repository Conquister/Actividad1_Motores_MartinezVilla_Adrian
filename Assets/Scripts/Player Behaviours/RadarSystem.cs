using UnityEngine;
using UnityEngine.UI;

public class RadarSystem : MonoBehaviour
{
    [SerializeField] private Transform player;                                      // Se declara la variable ‘player’ de tipo Transform. Necesaria para calcular la posición del player respecto a 
                                                                                    //la posición de las esferas y así transmitirlo en el radar
    [SerializeField] private Transform [] spheres;                                  // Se declara el array ‘spheres’ de tipo Transform. Es decir, el componente Transform de las esferas de la escena.
                                                                                    // Necesarias para calcular la posición de las esferas que el radar de la UI detectará
    [SerializeField] private RectTransform [] radarDots;                            // Declaramos el arra y ‘radarDots’ de tipo RectTransform, que representarán los sprites de círculos asociados a 
                                                                                    // las esferas en el radar de la UI
    [SerializeField] private RectTransform radarCenter;                             // Declaramos la variable radarCenter de tipo RectTransform. Representa el centro del radar de la UI, que será la 
                                                                                    // posición del player en el radar.
    [SerializeField] private float radarRadius = 50f;                               // Declaramos la variable ‘radarRadius’ de tipo float y la inicializamos en 50. Esta representa la distancia a la 
                                                                                    // que se dibujarán los radarDots en relación al player en el radar de la UI. Es decir, es el radio visual.
    [SerializeField] private float sphereColliderRadius = 10f;                      // Declaramos la variable ‘sphereColliderRadius’ de tipo float y la inicializamos en 10. Aquí definimos el radio 
                                                                                    // de detección del radar en el mundo 3D. Es decir, que si una esfera está fuera de ese rango, no se mostrará. 
                                                                                    // En otras palabras, en el mundo 3D, cuando una esfera acabe de entrar en ese radio (10), en el radar de la UI se
                                                                                    // mostrará al extremo del todo, que implicará un valor de 50.

    void Update()
    {
        //--------------------------------------------------------------------------COMPORTAMIENTO PARA EL RADAR--------------------------------------------------------------------------------------
        for (int i = 0; i < spheres.Length; i++) {                                  // En este FOR recorremos todas las esferas del array ‘spheres’ de Transforms.
            Transform sphere = spheres[i];                                          // Declaramos la variable ‘sphere’ de tipo Transform y le asignamos la esfera actual del bucle.
            RectTransform radarDot = radarDots[i];                                  // Declaramos la variable ‘radarDot’ de tipo RectTransform y le asignamos el radarDot correspondiente a esa esfera 
                                                                                    // en el radar de la UI. De esta forma, nos aseguramos de que cada esfera se procese con su bolita de la UI simult.
            
            
            if (sphere == null)                                                     // Comprobamos si la esfera actual del bucle es nula. Es decir, si ha sido destruida al haberse recogido y posteriormente eliminada.
            {
                radarDot.gameObject.SetActive(false);                               // En el caso de haber sido destruida, desactivamos el radarDot asignado a esa esfera para que no muestre en el radar de la UI
                continue;                                                           // Con el continue, volvemos a iterar en el for sin ejecutar el resto del código
            }

                                                                                    // Si llegamos a aquí es porque la esfera no ha sido destruida (no la hemos recogido). 
            Vector3 direction = sphere.position - player.position;                  // Declaramos la variable ‘direction’ de tipo Vector3 y la inicializamos con el valor (de tipo Vector3) que obtenemos
                                                                                    // al restar la posición del player a la posición de la esfera. Esto nos dirá en qué dirección se encuentra.
            float distance = direction.magnitude;                                   // Declaramos la variable ‘distance’ de tipo float, en la que almacenamos la magnitud (longitud) de la variable 
                                                                                    // ‘direction’ de tipo Vector3. Es decir, la distancia en el mundo 3D entre el jugador y la esfera.
                                                                                    // Con estas dos líneas ya tenemos la dirección (un Vector3) y la distancia (un float) a la que se encuentra la esfera en 
                                                                                    // relación a nuestra posición.
            
            if (distance > sphereColliderRadius)                                    // Comprobamos si la distancia entre jugador y esfera es mayor al radio de detección que hemos definido en la variable 
            {                                                                       // ‘sphereColliderRadius’ de tipo float. Es decir, 10f. Si lo es, significa que la esfera está fuera de rango.
                radarDot.gameObject.SetActive(false);                               // Ocultamos el radarDot de la esfera en el radar de la UI estableciéndolo a false.
                continue;                                                           // De nuevo, con el continue, volvemos a iterar en el for con la siguiente esfera sin ejecutar el resto del código

            }

        /*
        Recapitulemos, si hemos llegado hasta aquí dentro del bucle FOR, significa:
            1. La esfera no ha sido recogida
            2. La esfera está fuera del radio de detección
        Es decir, que la esfera actual del bucle no ha sido recogida, y esta está dentro de nuestro radio de detección. Con estas dos premisas, ahora hay que mostrar el radarDot de la esfera actual 
        del bucle en el radar de la UI, y cambiar el vector3 direction a un vector2 para que se pueda dibujar en nuestro radar de la UI
        */

            radarDot.gameObject.SetActive(true);                                    // Activamos el radarDot de la esfera actual del bucle, para que se muestre en el radar de la UI

            Vector2 directionNormalized = new Vector2(direction.x, direction.z).normalized;     // Declaramos la variable ‘directionNormalized’ de tipo Vector2. Los vector2 son vectores 2D en los 
                                                                                                // que se ignora el eje Z). Este vector lo normalizamos para convertir la dirección en un vector unitario 
                                                                                                // que solo indica dirección (y no distancia).

            float scaledDistance = Mathf.Clamp01(distance / sphereColliderRadius);  // Declaramos la variable 'scaledDistance' de tipo float. Mathf.Clamp01 se encarga de limitar un valor numérico al rango 
                                                                                    // [0,1]. Dividir 'distance / sphereColliderRadius' nos da una relación proporcional entre la posición de la esfera
                                                                                    // y el radio de detección del radar. Así pues, 'scaledDistance' tomará como valor la distancia entre esfera y jugador
                                                                                    // en función del radio del radar. '0' si está justo en la posición del jugador, '1' si está justo en el límite del radio,
                                                                                    // y '0,_' si está por medio

            float playerRotationY = player.eulerAngles.y;                           // Declaramos la variable ‘playerRotationY’ de tipo float y la inicializamos con el ángulo de rotación del jugador en el 
                                                                                    // eje Y. Es decir, hacia donde está mirando.

            float angleRadians = playerRotationY * Mathf.Deg2Rad;                   // Declaramos la variable ‘angleRadians’ de tipo float. Al multiplicar el ‘playerRotationY por la función Mathf.Deg2Rad 
                                                                                    // convertimos el ángulo del jugador de grados a radianes. Este valor en radianes se lo asignamos a la variable ‘angleRadians’.

            float rotatedX = directionNormalized.x * Mathf.Cos(angleRadians) - directionNormalized.y * Mathf.Sin(angleRadians);
            float rotatedY = directionNormalized.x * Mathf.Sin(angleRadians) + directionNormalized.y * Mathf.Cos(angleRadians);
            // Declaramos las variables ‘rotatedX’ y rotatedY’, con las que rotaremos el radarDot de la esfera actual del Bucle en el radar de la UI en función de la orientación del jugador (de su rotación) 

            Vector2 rotatedDirection = new Vector2(rotatedX, rotatedY);                 // Declaramos la variable rotatedDirection de tipo Vector2, que será el nuevo vector 2D que representará la posición 
                                                                                        // del radarDot en el radar de la UI en función de la orientación del jugador.

            Vector2 radarPosition = rotatedDirection * scaledDistance * radarRadius;    // Declaramos la variable radarPosition de tipo Vector2 y la inicializamos con el Vector2 rotatedDirection, que era 
                                                                                        // el Vector2 que representaba la posición del radarDot en el radar de la UI, y lo multiplicamos por ‘scaledDistance’ y 
                                                                                        // ‘radarRadius’ para determinar dónde debe aparecer el radarDot en el radar de la UI.

            radarDot.anchoredPosition = radarPosition;                                  // A la propiedad ‘anchoredPosition’ del componente RectTransform del radarDot le asignamos el Vector2 radarPosition, que
                                                                                        // contiene la posición calculada que debe ocupar el radarDot en función del centro del mismo.
        }
        //-----------------------------------------------------------------------------------------------------------------------------
    }
}