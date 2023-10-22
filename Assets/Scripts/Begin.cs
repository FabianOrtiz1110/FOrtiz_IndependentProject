using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : MonoBehaviour
{
    private bool Running = false;
    public KeyCode startGame = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Running && Input.GetKeyDown(startGame))
        {
            StartGame();
        }
    }   

    void StartGame()
    {
        Time.timeScale = 1f;
        Running = true;
        Destroy(gameObject);
    }
}

