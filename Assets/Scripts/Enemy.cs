using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 20.0f;
    public int standStillFrames = 30;
    private bool isPaused = false;
    public int HP = 2;
    private int HPTaken =0;
    public ParticleSystem Sparks;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {

            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            Debug.Log("Forward");
        }

        else
        {
            standStillFrames--;

            if (standStillFrames <= 0)
            {
                isPaused = false;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        Time.timeScale = 0f;
        Debug.Log("Game Over");
        }

        if (other.gameObject.CompareTag("Wall"))
            {
                   Vector3 newPosition = transform.position;
                    newPosition.z = 18.2f;
                    transform.position = newPosition;
                    isPaused = true;
                    Debug.Log("Pause");
            }
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            HPTaken++;
            Sparks.Play();
        }
            if (HPTaken>= HP)
            {
                Destroy(gameObject);
            }
    } 
}

