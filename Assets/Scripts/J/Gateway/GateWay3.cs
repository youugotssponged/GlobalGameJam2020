using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateWay3 : MonoBehaviour
{
    // NEEDS TO BE SET BY THE BOSS WHEN THE BOSS DIES
    public static bool bossDead = false; 

    public GameObject block1;
    public GameObject block2;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(bossDead){
            
            block1.SetActive(false);
            block2.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && bossDead){
            // TELEPORT TO LEVEL 2
            SceneManager.LoadScene(2);
        } 
    }
}
