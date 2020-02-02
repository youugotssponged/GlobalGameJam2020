using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFORotator : MonoBehaviour
{
    [SerializeField] private GameObject UFO;
    [SerializeField] private float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        UFO.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
