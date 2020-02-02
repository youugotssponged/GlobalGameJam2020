using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerUI;
    uiController uicont;

    //Singleton
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }


    private void Awake(){
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        uicont = PlayerUI.GetComponent<uiController>();
    }

    private void Update()
    {
        if (FirstPersonController.showGAMEOVER)
        {
            uicont.showDeathScreen();
        }



    }

}
