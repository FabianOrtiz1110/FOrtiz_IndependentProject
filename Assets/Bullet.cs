using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{


    void OnCollisionEnter(Collision otherObj) {
    if (otherObj.gameObject.tag == "Player") {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
