using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    public GameObject shield;
     private bool isShieldActive = false;
    private float shieldUptime = 5f;
    private float shieldTimer = 0f;
    private bool isShieldOnCooldown = false;
private float shieldCooldownDuration = 15f;
private float currentCooldownTimer = 0f;
     private GameObject currentShield;

    public float fireRate = 0.5f;

    public bool isShooting = false; 
    
    public Animator animator;
    public int level2= 3;
    public int PointAdded = 0;
    bool ExtraShot = false;
    public bool isGameOver = false;
    public static event Action OnPlayerDeath;
    public TextMeshProUGUI Shield;
    
    void Update()
    {
        if (!isGameOver){

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
           if (!isGameOver)
            {
    if (Input.GetKeyDown(KeyCode.Space) && !GameManager.GameIsPaused)
    {
        StartShooting();
    }
    else if (Input.GetKeyUp(KeyCode.Space) && !GameManager.GameIsPaused)
    {
        StopShooting();
    }



if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.F) && !isShieldActive && !isShieldOnCooldown && !GameManager.GameIsPaused)
    {
        ActivateShield();
        isShieldOnCooldown = true;
        currentCooldownTimer = shieldCooldownDuration;
        
    }

    // Update shield status if active
    if (isShieldActive)
    {
        shieldTimer -= Time.deltaTime;

        if (shieldTimer <= 0f)
        {
            Shield.color = Color.red;
            Shield.text = "--Inactive--";
            DeactivateShield();
        }
        else
        {
            Shield.color = Color.green;
            Shield.text = "--Active--";
        }
    }
    if (isShieldOnCooldown)
    {
        currentCooldownTimer -= Time.deltaTime;

        if (currentCooldownTimer <= 0f)
        {
            isShieldOnCooldown = false;
            Shield.color = Color.green;
        }
    }
}

        }
    }
void ActivateShield()
{
    if (!isShieldActive && !isShieldOnCooldown)
    {
        isShieldActive = true;
        shieldTimer = shieldUptime;
        currentShield = Instantiate(shield, transform.position, transform.rotation, transform);
        currentShield.transform.rotation = Quaternion.Euler(0f, 0f, 0);
    }
}


void DeactivateShield()
{
    isShieldActive = false;
    Destroy(currentShield);
}
    void StartShooting()
{
    if (!isGameOver && !isShooting)
    {
        isShooting = true;
        InvokeRepeating("Fire", 0f, 1f / fireRate);
    }
}

void StopShooting()
    {
        if (!isGameOver && isShooting)
        {
            isShooting = false;
            CancelInvoke("Fire");
        }
    }
    }
    void Fire()
    {
        if (ExtraShot == false){
        asPlayer.PlayOneShot(Laser, 1.0f);
        Instantiate(PlayerBullet, transform.position, PlayerBullet.transform.rotation);
        }

        if (ExtraShot == true)
        {
            asPlayer.PlayOneShot(Laser, 1.0f);
            Vector3 leftOffset = new Vector3(-0.65f, 0.0f, 0.0f);
        Vector3 rightOffset = new Vector3(0.65f, 0.0f, 0.0f);
        Instantiate(PlayerBullet, transform.position + leftOffset, PlayerBullet.transform.rotation);
        Instantiate(PlayerBullet, transform.position + rightOffset, PlayerBullet.transform.rotation);
        }
    }
     void Start()
    {
        asPlayer = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("PointBox"))
        {
            PointAdded++;
            Debug.Log("+1 Point");
        }

        if (PointAdded >= level2)
        {
            ExtraShot = true;
        }

        if (other.gameObject.CompareTag("Projectile"))
        {
            GameOver();
            Destroy(this);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameOver();
            Destroy(this);
        }
    }



    public void GameOver()
{
    isGameOver = true;
    Debug.Log("Game Over");
    OnPlayerDeath?.Invoke();
}
}




