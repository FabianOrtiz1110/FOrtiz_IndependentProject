using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject [] EnemyPrefab;
    private float xPosRange = 21.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Enemy", 3, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Enemy()
    {
        float randXPos = Random.Range(-xPosRange, xPosRange);
        int EnemyPrefabIndex = Random.Range(0, EnemyPrefab.Length);
        Vector3 randPos = new Vector3(randXPos, 5.29f, 23);
        Instantiate(EnemyPrefab[EnemyPrefabIndex], randPos,
        EnemyPrefab[EnemyPrefabIndex].transform.rotation);
    }
}