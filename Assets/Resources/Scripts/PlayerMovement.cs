/*
 * Controls player movement
 * 
 * Thanks to Holistic3d on YouTube for their tutorial
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 15f;

    CharacterController controller;
    Vector3 movement;
    bool cursorLocked = true;


    void Start()
    {
        // lock cursor to window and hide
        Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovePlayer();
        UnlockCursor();
    }

    private void UnlockCursor()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                cursorLocked = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                cursorLocked = true;
            }
        }
    }

    private void MovePlayer()
    {
        movement.z = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");

        movement = transform.TransformDirection(movement); // change vector from local to global space

        controller.SimpleMove(movement * playerSpeed);
    }
}
