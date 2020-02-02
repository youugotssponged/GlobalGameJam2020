using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMeteorFX : MonoBehaviour
{
    [SerializeField] private GameObject[] sparksParticles;

    private void OnCollisionEnter(Collision collision)
    {
        for(int i = 0; i < sparksParticles.Length; i++)
        {
            sparksParticles[i].SetActive(true);
        }
    }
}
