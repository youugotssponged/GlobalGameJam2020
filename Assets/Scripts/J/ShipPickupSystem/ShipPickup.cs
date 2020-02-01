using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPickup : MonoBehaviour
{
    public bool isActive;
    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
            Animate();
        else
        {
            isActive = false;
            HandlePartCollected();
            Destroy(gameObject);
        }
    }

    public void Animate()
    {

    }

    public void HandlePartCollected()
    {
        var _shipPickupManager = GameObject.Find("ShipPartsManager");
        //var _script = _shipPickupManager.GetComponent<ShipPickupManager>();

        ShipPickupManager.currentShipPartsFound += 1;

    }
}
