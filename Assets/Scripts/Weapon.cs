using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for a player weapon
/// </summary>
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
        // TODO move to a player input class
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    /// <summary>
    /// Fires the weapon and raycast, damaging enemies
    /// </summary>
    public void Fire()
    {
        gunShot.Play();
        RaycastWeapon();
        StartCoroutine(MuzzleFlash());
    }

    private void RaycastWeapon()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
            if (enemy)
            {
                enemy.DamageHealth(weaponDamage);
                Debug.Log(enemy.HealthPercentage().ToString()); // TODO remove this debug
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
