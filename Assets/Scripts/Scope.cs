﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Script to add a scope overlay and "zoom" for a weapon
/// </summary>
public class Scope : MonoBehaviour
{
    [Header("Required Components")]
    [Tooltip("UI Image to use as scope overlay")]
    public GameObject scopeOverlay;
    [Tooltip("Main camera")]
    public Camera mainCamera;
    [Tooltip("Separate camera that renders the weapon")]
    public Camera weaponCamera;

    [Header("Scope settings")]
    public float scopeDelay = 0.15f; // should match animator
    public float scopedFOV = 15f;
    [Range(0.0f, 1.0f)] public float scopedZoomScale = 0.5f;

    [Header("Editor strings")]
    public string zoomButton = "Fire2";

    private Animator animator;
    private bool isScoped = false;
    private float defaultFOV;
    private PlayerCamera playerCamera;

    private void Start()
    {
        animator = GetComponent<Animator>();
        scopeOverlay.SetActive(false);
        playerCamera = mainCamera.GetComponent<PlayerCamera>();
        defaultFOV = playerCamera.cameraFOV;
    }

    private void Update()
    {
        if (Input.GetButtonDown(zoomButton))
        {
            // toggle scope animation
            isScoped = !isScoped;
            animator.SetBool("isScoped", isScoped);

            // toggle overlay
            if (isScoped)
            {
                StartCoroutine(ToggleOverlayOn());
            }
            else
            {
                ToggleOverlayOff();
            }
        }
    }

    private IEnumerator ToggleOverlayOn()
    {
        yield return new WaitForSeconds(scopeDelay);
        scopeOverlay.SetActive(true);
        weaponCamera.gameObject.SetActive(false);
        mainCamera.fieldOfView = scopedFOV;
        playerCamera.SetCameraScale(scopedZoomScale);
    }

    private void ToggleOverlayOff()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.gameObject.SetActive(true);
        mainCamera.fieldOfView = defaultFOV;
        playerCamera.SetCameraScale(1.0f);
    }
}