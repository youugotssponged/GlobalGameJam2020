using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gateway2 : MonoBehaviour
{
    public bool unlock = false;
    public bool reverseDoor = false; 
    private int numNeededToUnlock = 6;
    private int fullToReverse = 13;

    public GameObject block1;
    public GameObject block2;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ShipPickupManager.currentShipPartsFound == numNeededToUnlock){
            unlock = true;
            block1.SetActive(false);
            block2.SetActive(false);
        } else if(ShipPickupManager.currentShipPartsFound == fullToReverse){
            unlock = false;
            reverseDoor = true;
            block1.SetActive(false);
            block2.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && unlock){
            // TELEPORT TO LEVEL 2
            SceneManager.LoadScene(3);
        } else if(other.tag == "Player" && reverseDoor){
            SceneManager.LoadScene(1);
        }
    }
}
