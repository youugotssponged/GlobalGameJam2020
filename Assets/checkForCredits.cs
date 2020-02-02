using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkForCredits : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (ShipPickupManager.canAccessCredits) {
            SceneManager.LoadScene(5);
        }
    }
}
