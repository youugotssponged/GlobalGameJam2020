using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float strafe;
    [SerializeField] private float translation;

    private void Start() {
        // Hide Cursor from screen
        Cursor.lockState = CursorLockMode.Locked;   
    }

    private void Update()
    {
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // Move
        transform.Translate(strafe, 0, translation);

        // Show Cursor + Trigger Nialls UI
        if(Input.GetKey(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.Locked;

            //////////////////////////////
            // TODO
            // Send Event to Nialls UI
            //////////////////////////////
        }


    }

}
