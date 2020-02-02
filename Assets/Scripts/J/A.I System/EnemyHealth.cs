using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public AudioSource src;
    public AudioClip enemyHurtClip;

    public float health = 100;

    private void Start() {
        src = GetComponent<AudioSource>();
        src.volume = 0.7f;
        src.clip = enemyHurtClip;    
    }

    public void Damage(float damageGiven){
        src.clip = enemyHurtClip;
        health -= damageGiven;
        Debug.Log("Damaged...");
    }
}
