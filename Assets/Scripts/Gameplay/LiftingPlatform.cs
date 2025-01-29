using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftingPlatform : MonoBehaviour
{
    [SerializeField] private GameManagerSO GM;
    [SerializeField] private int idPlataforma;
    [SerializeField] private Transform maxHeight;
    private bool subir = false;

    void Start()
    {
        GM.OnActivadorActivado += Subir;
    }

    private void Subir(int idPulsadorPulsado)
    {
        if(idPulsadorPulsado == idPlataforma)
        {
            subir = true;
        }
    }

    void Update()
    {
        if(subir && transform.position.y < maxHeight.transform.position.y)
        {
            transform.Translate(Vector3.up * 2 * Time.deltaTime);
        }
        else
        {
            subir = false;
        }
    }

    void OnDestroy ()
    {
        GM.OnActivadorActivado -= Subir;
    }
}
