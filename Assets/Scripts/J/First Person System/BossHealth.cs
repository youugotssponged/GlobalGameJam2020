using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public static float BOSSHEALTH = 100.0f;
    public float bossHealthAddTo100 = 0;

    public bool isActive;

    private void Start()
    {
        isActive = true;
        BOSSHEALTH += bossHealthAddTo100;
    }

    private void Update()
    {
        if(BOSSHEALTH <= 0 && isActive) {
            isActive = false;
            ShipPickupManager.currentShipPartsFound += 1;
            Destroy(gameObject);
        }
    }

    public void Damage(float damageGiven)
    {
        BOSSHEALTH -= damageGiven;
        Debug.Log("Boss Damaged...");
    }
}
