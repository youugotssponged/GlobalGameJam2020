using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    public GameObject mainMenuObject;
    public GameObject instructionsObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playButtonPressed()
    {
        SceneManager.LoadScene(1); //change the number to main game scene number in build order
        Debug.Log("Play");
    }

    public void instructionsButtonPressed()
    {
        mainMenuObject.SetActive(false);
        instructionsObject.SetActive(true);
        Debug.Log("Instructions");
    }

    public void exitButtonPressed()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void returnToMenu()
    {
        mainMenuObject.SetActive(true);
        instructionsObject.SetActive(false);
    }

}
