using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float sprintSpeed = 10.0f;
    [SerializeField] private float strafe = 5.0f;
    [SerializeField] private float translation = 5.0f;

    [SerializeField] private Vector3 jump;
    [SerializeField] private float jumpForce = 2.0f;
    [SerializeField] private Rigidbody rigidbody;

    private bool isGrounded = true;
    private bool isSprinting = false;

    // TODO:
    // INTERACTION WITH OBJECTS
    // SHOOTING MECHANIC 
    // STAMINA



    private void Start() {
        // Hide Cursor from screen
        // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Grab Player's rigidbody
        rigidbody = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);

    }

    private void Update()
    {
        // Show Cursor + Trigger Nialls UI for Pause
        if(Input.GetKey(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.Locked;

            //////////////////////////////
            // TODO
            // Send Event to Nialls UI
            //////////////////////////////
        }

        isGrounded = CheckIsGrounded(4.0f);
        
        Move();
        Jump();

    }

    private void Move(){
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        if(!isSprinting) {
            translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        }
        else if(isSprinting && isGrounded) {
            translation = Input.GetAxis("Vertical") * speed * Time.deltaTime * sprintSpeed;
            strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime * sprintSpeed;
        }

        // Move
        transform.Translate(strafe, 0, translation);
    }

    private void Jump(){
        if(Input.GetKeyDown(KeyCode.Space) && CheckIsGrounded(4.0f)){
            rigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        } 
    }

    // REMEMBER TO SET GROUND TAG
    private bool CheckIsGrounded(float checkDistFromPlayerLocation){
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, checkDistFromPlayerLocation)){
            //Debug.Log(hit.collider.tag);

            // CONSIDER HOLDING A LIST OF TAG STRINGS TO CHECK AGAINST IF PLAYER CAN JUMP OFF OF
            if(hit.collider.tag == "Ground") {
                return true;            
            }
        }
        return false;
    }



}
