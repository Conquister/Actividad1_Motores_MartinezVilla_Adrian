using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLifter : MonoBehaviour
{
    private float currentPosition;
    private float maxPosition;
    private float minPosition;
    private float speed = 1f;
    private bool goingForward = false;

    void Start()
    {
        currentPosition = transform.position.z;
        maxPosition = currentPosition + 2f;
        minPosition = currentPosition - 2f;

    }

    void Update()
    {
        if (goingForward)
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
            if (transform.position.z >= maxPosition)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxPosition);
                goingForward = false;
            }
        }
        else
        {
            transform.position -= Vector3.forward * speed * Time.deltaTime;
            if(transform.position.z <= minPosition)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minPosition);
                goingForward = true;
            }
        }
       
    }
}
