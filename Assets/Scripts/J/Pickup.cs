using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    [SerializeField] private GameObject thePickup;
    private bool hasBeenPickedUp = false;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Test
        if(Input.GetKey(KeyCode.Space) && !hasBeenPickedUp){
            Destroy(thePickup);
            Debug.Log("The Object has been picked up...");
        }
    }
}
