using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuObject;
    bool paused;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (paused) //if the game is already paused, resume the game
            {
                resumeGame();
            } else //if the game isn't paused, pause the game
            {
                pauseGame();
            }
        }
    }

    void pauseGame()
    {
        paused = true;
        Time.timeScale = 0f; //set the timescale to 0, freezing the game
        pauseMenuObject.SetActive(true); //show the pause menu
    }

    void resumeGame()
    {
        paused = false;
        Time.timeScale = 1f; //set the timescale to 1, unfreezing the game
        pauseMenuObject.SetActive(false); //hide the pause menu
    }

    public void resumeButtonPressed()
    {
        //resume the game
        resumeGame();
    }

    public void settingsButtonPressed()
    {
        //open the settings menu
    }

    public void returnToMenuButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

}
