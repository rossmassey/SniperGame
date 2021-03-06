﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI element indication enemy state
/// </summary>
public class EnemyAlertBar : MonoBehaviour
{
    public Image foregroundImage;
    public Image backgroundImage;

    /// <summary>
    /// Change alert bar fill percentage
    /// </summary>
    /// <param name="newFill">new fill amount</param>
    public void SetForegroundFill(float newFill)
    {
        foregroundImage.fillAmount = newFill;
    }

    /// <summary>
    /// Change alert bar color
    /// </summary>
    /// <param name="newColor">new color</param>
    public void SetForegroundColor(Color newColor)
    {
        foregroundImage.color = newColor;
    }

    /// <summary>
    /// Hide the alert bar
    /// </summary>
    public void DisableAlertBar()
    {
        foregroundImage.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
    }

    /// <summary>
    /// Show the alert bar
    /// </summary>
    public void EnableAlertBar()
    {
        foregroundImage.fillAmount = 0f;
        foregroundImage.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);
    }

    // LateUpdate is used to ensure this is done after player camera is moved
    private void LateUpdate()
    {
        // canvas always facing player
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

    /*
     * Lerp version:
     * (use for player health? not needed for alert bar)
     * 
            public float smoothTime = 0.2f;
      
            public void SetForegroundFill(float newFill)
            {
                StartCoroutine(LerpFillAmount());
            }

            public void SetToggle(bool toggle)

            private IEnumerator LerpFillAmount(float newFill)
            {
                float currentFill = foregroundImage.fillAmount;
                float elapsedTime = 0f;

                while (elapsedTime < smoothTime)
                {
                    elapsedTime += Time.deltaTime;
                    foregroundImage.fillAmount = Mathf.Lerp(currentFill, newFill, elapsedTime / smoothTime);
                    yield return null;
                }

                foregroundImage.fillAmount = newFill;
            }
    */

    // TODO add Awake to all scripts
    private void Awake()
    {
        if (!foregroundImage || !backgroundImage)
        {
            Debug.LogError("EnemyAlertBar missing image");
        }
    }
}
