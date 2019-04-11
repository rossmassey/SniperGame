using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player input related to movement, and moves the player
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 15f;
    public GameObject weaponHolder;

    Animator weaponAnimator;
    bool cursorLocked = true;
    bool isMovementKeyPressed;
    CharacterController controller;
    Vector3 movement;

    void Start()
    {
        // lock cursor to window and hide
        Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
        weaponAnimator = weaponHolder.GetComponent<Animator>();
    }

    private void Update()
    {
        MovePlayer();
        AnimateWeapon();
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

    private void AnimateWeapon()
    {
        isMovementKeyPressed = !(Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0);
        weaponAnimator.SetBool("isMoving", isMovementKeyPressed);
    }
}
