using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : MonoBehaviour
{
    private bool Paused = true;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("YourTargetTag"))
        {
            if (!timePaused)
            {
                Time.timeScale = 0f;
                timePaused = true;
            }
        }
    }
}
