using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPickupManager : MonoBehaviour
{
    public int currentShipPartsFound = 0;
    private int totalShipParts = 13;

    private void Awake()
    {
        DontDestroyOnLoad(this);
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
