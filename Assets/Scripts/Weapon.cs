using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Camera mainCamera;

    [Header("Attributes")]
    public string weaponName;
    public float weaponDamage;
    public int clipSize;

    [Header("Effects")]
    public GameObject muzzleFlash;
    public float flashTime = 0.1f;
    public AudioSource gunShot;



    // TODO
    int currentClipAmount;
    int ammunition;

    private void Start()
    {
        mainCamera = Camera.main;
        muzzleFlash.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    public void Fire()
    {
        StartCoroutine(MuzzleFlash());
        gunShot.Play();

        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.DamageHealth(weaponDamage);
                Debug.Log(enemy.HealthPercentage().ToString());
            }
        }


    }

    IEnumerator MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        muzzleFlash.SetActive(false);
    }

}
