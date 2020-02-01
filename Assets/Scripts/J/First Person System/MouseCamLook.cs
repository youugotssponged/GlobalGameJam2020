using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamLook : MonoBehaviour
{
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    public GameObject character;

    private Vector2 mouseLook;
    private Vector2 smoothV;

    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private LayerMask healthPickupLayerMask;
    [SerializeField] private LayerMask shipPickupLayerMask;

    //[SerializeField] private float lookYMax = 25;
    //[SerializeField] private float lookYMin = 10; 

    private void Start()
    {
        character = this.transform.parent.gameObject;
    }

    private void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

        mouseLook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(-mouseLook.y, -90.0f, 90.0f), Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

        HandleShooting();
        HandleHealthPickup();
        HandleShipPartPickup();
    }

    // TODO: ADD SOUND
    private void HandleShooting(){
        // Left click 
        if(Input.GetMouseButton(0)){
            RaycastHit hit; 
            if(Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 1000, enemyLayerMask)){
                Debug.Log("You Hit an Enemy!!!!");

                // Deal the enemy damage

                var shotEnemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if(shotEnemyHealth == null) return;
                
                shotEnemyHealth.Damage(10);
                Debug.Log("Working");
            }
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward, Color.red, 1000);

        }
        // Send Raycast
    }

    // TODO: ADD SOUND
    private void HandleHealthPickup()
    {
        if (Input.GetKey(KeyCode.E)){
            Debug.Log("E WAS PRESSED");
            RaycastHit hit;

            if(Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 1000, healthPickupLayerMask)){
                Debug.Log("You picked up a health pickup");

                var healthPickupFound = hit.collider.GetComponent<HealthPickup>();
                if (healthPickupFound == null) return;
                healthPickupFound.isActive = false;
            }
        }
    }

    private void HandleShipPartPickup()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("E WAS PRESSED");
            RaycastHit hit;

            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 1000, shipPickupLayerMask))
            {
                Debug.Log("You picked up a SHIP PART");

                var shipPickup = hit.collider.GetComponent<ShipPickup>();
                if (shipPickup == null) return;
                shipPickup.isActive = false;
            }
        }
    }

}
