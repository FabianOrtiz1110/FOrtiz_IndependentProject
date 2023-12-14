using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float speed = 20.0f;
    public int standStillFrames = 30;
    private bool isPaused = false;
    public int HP = 50;
    private int HPTaken = 0;
    public ParticleSystem Sparks;
    public GameObject enemyBulletPrefab;
    public float bulletSpeed = 10.0f;
    public float timeBetweenShots = 3.0f;
    public float timeBetweenShots2 = 0.1f;
    public float Angle = 50f;
    GameObject player;
    private Vector3 targetPosition = new Vector3(-12.0f, 5.29f, 15.0f);
    private Vector3 targetPosition2 = new Vector3(11.0f, 5.29f, 15.0f);
    private Vector3 targetPosition3 = new Vector3(0.0f, 5.29f, 15.0f);
    private float timeSinceWallCollision = 0.0f;

    public bool secondPosition =  false;
    public bool thirdPosition = false;
    public bool end = false;
    public delegate void EnemyDeathAction();
    public static event EnemyDeathAction OnEnemyDeath;
   


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(ShootCoroutine());
        StartCoroutine(Shoot2Coroutine());
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
            timeSinceWallCollision += Time.deltaTime;

            if (timeSinceWallCollision <= 15 && timeSinceWallCollision >= 12.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                {
                    isPaused = true; 

                    secondPosition = true;
                }
            }
        }
        if (secondPosition && timeSinceWallCollision >= 20.0f  && timeSinceWallCollision <= 30.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition2, speed * Time.deltaTime);
            thirdPosition = true;
        }



         if (thirdPosition && timeSinceWallCollision >= 31.0f  && timeSinceWallCollision <= 40.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition3, speed * Time.deltaTime);
            timeBetweenShots = 2.0f;
            end = true;
            
        }

        if (end && timeSinceWallCollision >= 42.0f  && timeSinceWallCollision <= 1000.0f)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
            speed = 7.0f;
        }

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") && !end)
        {
            timeSinceWallCollision = 0.0f;

            Vector3 newPosition = transform.position;
            newPosition.z = 19.35f;
            transform.position = newPosition;
            isPaused = true;
            Shoot();
            Shoot2();
        }

        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            HPTaken++;
            Sparks.GetComponent<ParticleSystem>().Play();
            Debug.Log("Bullet Hit");

        }

        if (HPTaken >= HP)
        {
            Score.instance.AddPoint2();
            if (OnEnemyDeath != null)
            {
                OnEnemyDeath();
            }
            Destroy(gameObject);
        }
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Death"))
        {
            if (OnEnemyDeath != null)
            {
                OnEnemyDeath();
            }
            Destroy(gameObject);

        }
    }

    void Shoot()
    {
        for (int i = 0; i <= 72; i++)
        {
            float NewAngle = Angle + i * 5;
            Vector3 shootDirection = new Vector3(Mathf.Cos(NewAngle * Mathf.Deg2Rad), 0f, Mathf.Sin(NewAngle * Mathf.Deg2Rad));
            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.LookRotation(shootDirection));
            bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;
        }
    }

    void Shoot2()
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

    IEnumerator Shoot2Coroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots2);
            Shoot2();
        }
    }
    
    
}
