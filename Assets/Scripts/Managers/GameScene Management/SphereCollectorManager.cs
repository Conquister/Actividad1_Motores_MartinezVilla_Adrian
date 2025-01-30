using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu (fileName = "SphereCollectorManager", menuName = "Managers/SphereCollectorManager")]
public class SphereCollectorManager : ScriptableObject
{
    public event Action OnPickUpSphereBoxesAndMessagesShow;     // Evento para activar los GameObjects Mensajes
    public event Action<int> OnSphereCollected;                 // Este es el evento que actualice la UI
    public event Action OnAllSpheresCollected;                  // Este es el evento para cuando se recojan todas las esferas
    public event Action <int> OnSpherePlaced;
    public event Action OnAllSpheresPlaced;
    public event Action OnPlacedSphereBoxesAndMessagesShow;     // Evento para activar los GameObjects relaciones con enseñar el mensaje "Pulsa B para colocar la esfera en el soporte"
    public event Action OnReadyToGetOutBoxesAndMessagesShow;
    private int totalSpheres = 7;                                // Total de esferas en la escena
    private int collectedSpheres = 0;
    private int placedSpheres = 0;
    
    public void CollectedSphere()
    {
        collectedSpheres++;
        Debug.Log("CollectedSphere llamado. collectedSpheres = " + collectedSpheres);
        OnSphereCollected?.Invoke(collectedSpheres);

        if (collectedSpheres == totalSpheres)
        {
            OnAllSpheresCollected?.Invoke();
        }
    }

    public void PlacedSphere()
    {
        placedSpheres ++;
        Debug.Log("CollectedSphere llamado. placedSpheres = " + placedSpheres);
        if (placedSpheres >= 0)
        {
            Debug.Log ("Lanzo evento para que el enemy venga a por mí");
            OnAllSpheresPlaced?.Invoke();
        }
    }

    public void ShowPickUpBoxesAndMessages()
    {
        OnPickUpSphereBoxesAndMessagesShow?.Invoke();
    }
    public void LetsGetOut()
    {
        OnReadyToGetOutBoxesAndMessagesShow?.Invoke();
    }

    public void ShowPlacedSphereBoxesAndMessages()
    {
        OnPlacedSphereBoxesAndMessagesShow?.Invoke();
    }

    public void ResetManager()
    {
        collectedSpheres = 0;
        placedSpheres = 0;
    }

    public int GetCollectedSpheres()
    {
        return collectedSpheres;
    }
}
