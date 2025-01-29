using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescendantPlatform : MonoBehaviour
{
    [SerializeField] private GameManagerSO GM;
    [SerializeField] private int idPlataforma;
    [SerializeField] private Transform maxHeight;
    private bool bajar = false;
    // Start is called before the first frame update
    void Start()
    {
        GM.OnActivadorActivado += Bajar;
    }

    private void Bajar(int idPulsadorPulsado)
    {
        if(idPulsadorPulsado == idPlataforma)
        {
            bajar = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(bajar)
        {
            transform.Translate(Vector3.down * 1 * Time.deltaTime);
        }
    }
}
