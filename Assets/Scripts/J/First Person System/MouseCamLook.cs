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
    }

}
