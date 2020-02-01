using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFORotator : MonoBehaviour
{
    [SerializeField] private GameObject UFO;
    [SerializeField] private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UFO.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
