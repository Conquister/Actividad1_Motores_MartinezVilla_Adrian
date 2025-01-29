using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLifter1 : MonoBehaviour
{
    private float currentHeight;
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;
    private float speed = 1f;
    private bool lifting = false;

    void Start()
    {
        currentHeight = transform.position.y;
        maxHeight = currentHeight + 3f;
        minHeight = currentHeight - 3f;

    }

    void Update()
    {
        if (lifting)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
            if (transform.position.y >= maxHeight)
            {
                transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
                lifting = false;
            }
        }
        else
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
            if(transform.position.y <= minHeight)
            {
                transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
                lifting = true;
            }
        }
       
    }
}
