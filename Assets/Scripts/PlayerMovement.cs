/*
 * Controls player movement
 * 
 * Thanks to Holistic3d on YouTube for their tutorial
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 15f;

    void Start()
    {
        // lock cursor to window and hide
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float verticalMovement = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        float horizontalMovement = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        transform.Translate(horizontalMovement, 0, verticalMovement);
    }
}
