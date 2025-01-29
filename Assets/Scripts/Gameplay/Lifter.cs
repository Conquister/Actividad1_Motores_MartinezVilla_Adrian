
using UnityEngine;

public class VerticalLifter : MonoBehaviour
{
    
    private float currentHeight;
    private float maxHeight;
    private float minHeight;
    private float speed = 1f;
    private bool lifting = false;

    void Start()
    {
        currentHeight = transform.position.y;
        maxHeight = currentHeight + 2f;
        minHeight = currentHeight - 2f;

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
