using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAlertBar : MonoBehaviour
{
    public Image foregroundImage;
    public Image backgroundImage;

    public void SetForegroundFill(float newFill)
    {
        foregroundImage.fillAmount = newFill;
    }

    public void SetForegroundColor(Color newColor)
    {
        foregroundImage.color = newColor;
    }

    public void DisableAlertBar()
    {
        foregroundImage.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
    }

    public void EnableAlertBar()
    {
        foregroundImage.fillAmount = 0f;
        foregroundImage.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

    /*
     * Lerp version:
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
