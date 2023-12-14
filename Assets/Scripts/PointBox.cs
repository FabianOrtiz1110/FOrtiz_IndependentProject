using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBox : MonoBehaviour
{
    public float speed = 20.0f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            DestroyPointBox();
        }
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
    private void DestroyPointBox()
    {
        Destroy(gameObject);
    }
}

