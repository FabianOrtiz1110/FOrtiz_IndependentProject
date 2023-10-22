using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip Laser;
    private AudioSource asPlayer;
    private float HorizontalInput;
    private float VerticalInput;
    public float speed = 10.0f;
    private float xRange = 21.0f;

    private float zRange = 28.0f;

    public GameObject PlayerBullet;

    public float fireRate = 0.5f;

    private bool isShooting = false; 
    
    public Animator animator;
    
    void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * HorizontalInput * Time.deltaTime * speed);
        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(HorizontalInput), Mathf.Abs(VerticalInput)));
        
        VerticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * VerticalInput * Time.deltaTime * speed);

        if (transform.position.x > xRange)
            {
                transform.position = new Vector3 (xRange, transform.position.y, transform.position.z);
            }

        if (transform.position.x < -xRange)
            {
                transform.position = new Vector3 (-xRange, transform.position.y, transform.position.z);
            }

        if (transform.position.z > 24)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 24);
            }

        if (transform.position.z < -zRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
            }   

        if (Input.GetKeyDown(KeyCode.Space))
            {
        StartShooting();
            }
        else if (Input.GetKeyUp(KeyCode.Space))
            {
        StopShooting();
            }
    void StartShooting()
{
    if (!isShooting)
    {
        isShooting = true;
        InvokeRepeating("Fire", 0f, 1f / fireRate);
    }
}

void StopShooting()
{
    if (isShooting)
    {
        isShooting = false;
        CancelInvoke("Fire");
    }
}
    }
    void Fire()
    {
        asPlayer.PlayOneShot(Laser, 1.0f);
        Vector3 leftOffset = new Vector3(-0.65f, 0.0f, 0.0f);
        Vector3 rightOffset = new Vector3(0.65f, 0.0f, 0.0f);
        Instantiate(PlayerBullet, transform.position + leftOffset, PlayerBullet.transform.rotation);
        Instantiate(PlayerBullet, transform.position + rightOffset, PlayerBullet.transform.rotation);
    }
     void Start()
    {
        asPlayer = GetComponent<AudioSource>();
    }
  
}
