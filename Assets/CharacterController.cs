/*
 * Controls player movement
 * 
 * Thanks to Holistic3d on YouTube for their tutorial
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float playerSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        // lock cursor to window and hide
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalMovement = 
            Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        float horizontalMovement = 
            Input.GetAxis("Horizontal") * playerSpeed  * Time.deltaTime;

        transform.Translate(horizontalMovement, 0, verticalMovement);

    }
}
