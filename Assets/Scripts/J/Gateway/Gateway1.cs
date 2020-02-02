using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gateway1 : MonoBehaviour
{
    public bool unlock = false;
    private int numNeededToUnlock = 6;

    public GameObject block1;
    public GameObject block2;

    private void Start()
    {
        //ShipPickupManager.currentShipPartsFound = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShipPickupManager.currentShipPartsFound == numNeededToUnlock){
            unlock = true;
            block1.SetActive(false);
            block2.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && unlock){
            // TELEPORT TO LEVEL 2
            SceneManager.LoadScene(2);
        }
    }
}
