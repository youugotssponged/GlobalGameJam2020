using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100;

    public void Damage(float damageGiven){
        health -= damageGiven;
        Debug.Log("Damaged...");
    }
}
