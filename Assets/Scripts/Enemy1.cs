using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
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
    GameObject player;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootCoroutine());
        player=GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    if (rb == null)
    {
        Debug.LogError("Rigidbody component not found on the enemy object.");
    }
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
            Score.instance.AddPoint();
            Destroy(gameObject);
        }
    }
    
    void Shoot()
    {
    Vector3 seekDirection = (player.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
    bullet.GetComponent<Rigidbody>().velocity = seekDirection * bulletSpeed;
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

