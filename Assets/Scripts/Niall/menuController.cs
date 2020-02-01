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
    }

    public void instructionsButtonPressed()
    {
        mainMenuObject.SetActive(false);
        instructionsObject.SetActive(true);
    }

    public void exitButtonPressed()
    {
        Application.Quit();
    }

    public void returnToMenu()
    {
        mainMenuObject.SetActive(true);
        instructionsObject.SetActive(false);
    }

}
