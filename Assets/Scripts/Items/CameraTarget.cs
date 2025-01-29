using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    private bool isActive = false;
    
    private void LateUpdate()
    {
        if(isActive && target != null)
        {
            transform.LookAt(target);
            Debug.Log($"Cámara mirando al objectivo: {target.name} en {target.position}");
        }
    }

    public void ActivateLookAt (Transform newTarget)
    {
        target = newTarget;
        isActive = true;
    }

    public void DeactivateLookAt ()
    {
        isActive = false;
    }
}
