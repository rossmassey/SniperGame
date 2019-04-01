using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string name;
    public float damage;
    public int clipSize;

    int currentClipAmount;
    int ammunition;

    public bool Fire()
    {
        return false;
    }

    void DamageEnemy(Enemy enemy)
    {

    }

}
