using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public string zoomButton = "Fire2";
    public string scopeAnimationBool = "isScoped";

    public float scopeDelay = 0.15f;

    public GameObject scopeOverlay;

    Animator animator;
    bool isScoped = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        scopeOverlay.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown(zoomButton))
        {
            // toggle scope animation
            isScoped = !isScoped;
            animator.SetBool(scopeAnimationBool, isScoped);

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

    IEnumerator ToggleOverlayOn()
    {
        Debug.Log("here");
        yield return new WaitForSeconds(scopeDelay);
        scopeOverlay.SetActive(true);
    }

    void ToggleOverlayOff()
    {
        scopeOverlay.SetActive(false);
    }
}
