using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOB : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    if (transform.position.z > 33 || transform.position.z < -30 || transform.position.x > 21 || transform.position.x < -21)
    {
        Destroy(gameObject);
    }

    }
    private void OnEnable()
    {
       PlayerController.OnPlayerDeath += Destroy;
    }
    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= Destroy;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }


}
