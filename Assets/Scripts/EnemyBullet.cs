using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 25 || transform.position.z < -30 || transform.position.x > 21 || transform.position.x < -21)
    {
        Destroy(gameObject);
    }
}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
        }
        
    }
}
