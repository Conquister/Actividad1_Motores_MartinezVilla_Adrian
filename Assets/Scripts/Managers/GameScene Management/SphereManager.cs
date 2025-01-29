using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereManager : MonoBehaviour
{
    public static List<GameObject> allSpheres = new List<GameObject>();

    void Awake ()
    {
        allSpheres.Clear();
        allSpheres.AddRange(GameObject.FindGameObjectsWithTag("Esfera"));
    }
}
