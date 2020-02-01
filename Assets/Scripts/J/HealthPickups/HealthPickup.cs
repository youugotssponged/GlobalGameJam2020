using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public bool isActive;
    public float extraHealth = 10.0f;

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
            givePlayerHealth();
            Destroy(gameObject);
        }
    }

    public void Animate()
    {
        transform.Rotate(0, Mathf.Cos(Mathf.Clamp(5.0f, 0, 5.0f)), 0);
    }

    public void givePlayerHealth()
    {
        var playerObject = GameObject.FindWithTag("Player");
        //var playerScript = playerObject.GetComponent<FirstPersonController>();

        FirstPersonController.PlayerHealth += extraHealth;
        Debug.Log("Player Health: " + FirstPersonController.PlayerHealth);
    }
}
