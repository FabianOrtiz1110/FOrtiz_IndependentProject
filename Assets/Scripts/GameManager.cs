using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverText;

    public GameObject [] EnemyPrefab;
    public GameObject [] PointBoxPrefab;
    private float xPosRange = 21.0f;
    public GameObject gameOverMenu;
    private bool gameOver = false;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private float enemySpawnInterval = 1.5f;
    private float elapsedTime = 0.0f;
    private bool bossSpawned = false;
    public GameObject[] BossPrefab;
    public GameObject Victory;

    // Start is called before the first frame update
    void Start()
    {
       InvokeRepeating("Enemy", 2.8f, enemySpawnInterval);

        InvokeRepeating("PointBox", 15.0f, 15.0f);
        
        Boss.OnEnemyDeath += HandleBossDeath;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= 60.0f && !bossSpawned)
            {
                CancelInvoke("Enemy"); 
                InvokeRepeating("Enemy", 2.8f, enemySpawnInterval * 3); 

                if (!bossSpawned)
                {
                    SpawnBoss();
                    bossSpawned = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
    }

    void Enemy()
    {
        if(!gameOver){

        float randXPos = Random.Range(-xPosRange, xPosRange);
        int EnemyPrefabIndex = Random.Range(0, EnemyPrefab.Length);
        Vector3 randPos = new Vector3(randXPos, 5.29f, 23);
        Instantiate(EnemyPrefab[EnemyPrefabIndex], randPos,
        EnemyPrefab[EnemyPrefabIndex].transform.rotation);
        }
    }
    void PointBox()
    {
        if(!gameOver){

        float randXPos = Random.Range(-xPosRange, xPosRange);
        int PointBoxPrefabIndex = Random.Range(0, PointBoxPrefab.Length);
        Vector3 randPos = new Vector3(randXPos, 5.29f, 23);
        Instantiate(PointBoxPrefab[PointBoxPrefabIndex], randPos,
        PointBoxPrefab[PointBoxPrefabIndex].transform.rotation);
        }
    }

    private void OnEnable()
    {
       PlayerController.OnPlayerDeath += EnableGameOverMenu;
    }
    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= EnableGameOverMenu;
        Boss.OnEnemyDeath -= HandleBossDeath;
    }

   public void EnableGameOverMenu()
{
    GameOverText.gameObject.SetActive(true);
    gameOver = true;
}

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
         Time.timeScale = 1.0f;
         GameIsPaused  = false;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1.0f;
        GameIsPaused  = false;
    }
    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused  = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused  = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1.0f;
    }

    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused  = false;
    }
     void SpawnBoss()
    {
        if (!gameOver)
        {
            Vector3 randPos = new Vector3(1.5f, 5.29f, 30.0f);
            int bossPrefabIndex = Random.Range(0, BossPrefab.Length);
            Instantiate(BossPrefab[bossPrefabIndex], randPos, BossPrefab[bossPrefabIndex].transform.rotation);
        }
    }
    void HandleBossDeath()
    {


        Debug.Log("Boss has been defeated!");
        Victory.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused  = true;
    }

}

