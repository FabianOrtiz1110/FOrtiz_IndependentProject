using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        float xMovement = Input.GetAxis ("Horizontal") * Time.deltaTime;
		float yMovement = Input.GetAxis ("Vertical")  * Time.deltaTime;
		transform.Translate((xMovement*15), (yMovement*15), 0f);
    }
}
