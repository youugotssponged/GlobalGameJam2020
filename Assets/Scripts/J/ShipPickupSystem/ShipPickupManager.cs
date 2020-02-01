using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPickupManager : MonoBehaviour
{
    public static int currentShipPartsFound = 0;
    private int totalShipParts = 13;

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
        currentShipPartsFound += 1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current parts: " + currentShipPartsFound);
        if(currentShipPartsFound == totalShipParts)
        {
            Debug.Log("ALL PARTS COLLECTED!!!!");
        }
    }

    private int CheckPartsFound()
    {
        return currentShipPartsFound;
    }
}
