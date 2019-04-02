using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera mainCamera;

    public string weaponName;
    public float damage;
    public int clipSize;

    int currentClipAmount;
    int ammunition;

    private void Start()
    {
        mainCamera = Camera.main;
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
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy)
            {
                Debug.Log(enemy.enemyName + " hit!");
            }
        }
    }

    void DamageEnemy(Enemy enemy)
    {

    }

}
