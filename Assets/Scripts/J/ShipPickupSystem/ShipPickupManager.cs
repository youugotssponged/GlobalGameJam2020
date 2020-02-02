using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPickupManager : MonoBehaviour
{
    public static int currentShipPartsFound = 0;
    private int totalShipParts = 13;

    public static bool canAccessCredits = false;

    private static ShipPickupManager instance = null;
    public static ShipPickupManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current parts: " + currentShipPartsFound);
        if(currentShipPartsFound == totalShipParts)
        {
            Debug.Log("ALL PARTS COLLECTED!!!!");
        }
        if(BossHealth.BOSSHEALTH <= 0) {
            canAccessCredits = true;
        }
    }

    private int CheckPartsFound()
    {
        return currentShipPartsFound;
    }
}
