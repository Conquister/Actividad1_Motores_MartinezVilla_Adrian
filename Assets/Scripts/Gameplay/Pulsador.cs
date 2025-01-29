using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Pulsador : MonoBehaviour
{
    [SerializeField] private GameManagerSO GM;
    [SerializeField] private int idPulsador;
    private bool isActive = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
        if(collision.transform.TryGetComponent(out Player player))
        {
            if (!isActive)
            {
                isActive = true;
                GM.PulsadorPulsado(idPulsador); 
            }
               
        }
    }
}
