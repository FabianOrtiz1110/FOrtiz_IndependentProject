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
    public GameObject enemyBulletPrefab;
    public float bulletSpeed = 10.0f;
    public float timeBetweenShots = 12.0f;
    public float Angle = 50f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

        if (!isPaused)
        {

            transform.Translate(Vector3.forward * Time.deltaTime * speed);
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
                    newPosition.z = 19.0f;
                    transform.position = newPosition;
                    isPaused = true;
                    Shoot();
            }
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            HPTaken++;
            Sparks.GetComponent<ParticleSystem>().Play();
            Debug.Log("Bullet Hit");
        }

        if (HPTaken>= HP)
        {
            Destroy(gameObject);
        }
    }
    
    void Shoot()
    {      
        for (int i = 0; i<=12; i++)
        {
        float NewAngle = Angle + i * 5;
        Vector3 shootDirection = new Vector3(Mathf.Cos(NewAngle * Mathf.Deg2Rad), 0f, Mathf.Sin(NewAngle * Mathf.Deg2Rad));
        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.LookRotation(shootDirection));
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;
        }
    }
     IEnumerator ShootCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            Shoot();
        }
    }
}

