﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uiController : MonoBehaviour
{
    public GameObject healthBarObject;
    public GameObject staminaBarObject;
    public GameObject shipStatusBarObject;
    public GameObject ammoObject;
    public GameObject bossHealthBarObject;
    public GameObject bossBarObject;
    public GameObject pauseMenuObject;
    public GameObject deathScreenObject;
    public GameObject objectivesObject;

    public Transform messageParent;
    public Transform messageTransform;

    bool pauseMenuLocked;
    bool paused;

    float currHealth;
    float currParts;

    int level1Parts;
    int level2Parts;
    int level3Parts;
    int currentScene;

    objectiveController objCont;

    Text[] childTexts;
    Text weaponName;
    Text ammoText;

    // Start is called before the first frame update
    void Start()
    {
        //updateShipStatusBar(13);
        objCont = objectivesObject.GetComponent<objectiveController>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 2) //level 1 - forest
        {
            if (objCont.isEmpty(1))
            {
                level1Parts = 0;
                createLevel1Objectives();
            }
        } else if (currentScene == 3) //level 2 - ice
        {
            if (objCont.isEmpty(2))
            {
                level2Parts = 0;
                createLevel2Objectives();
            }
        } else if (currentScene == 4) //level 3 - lava
        {
            if (objCont.isEmpty(3))
            {
                level3Parts = 0;
                createLevel3Objectives();
            }
        }
        currHealth = 100;
        currParts = ShipPickupManager.currentShipPartsFound;
        level3Parts = 0;
        deathScreenObject.SetActive(false);
        pauseMenuLocked = false;
        paused = false;
        //updateHealthBar(50); //sets the health bar to 50%
        childTexts = ammoObject.GetComponentsInChildren<Text>();
        for (int i = 0; i < childTexts.Length; i++)
        {
            if (childTexts[i].name == "weapon")
            {
                weaponName = childTexts[i];
            } else if (childTexts[i].name == "ammoCount")
            {
                ammoText = childTexts[i];
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 2) //level 1 - forest
        {
            updateLevel1Objectives();
        }
        else if (currentScene == 3) //level 2 - ice
        {
            updateLevel2Objectives();
        }
        else if (currentScene == 4) //level 3 - lava
        {
            updateLevel3Objectives();
        }
        if (Input.GetKeyDown("escape") && !pauseMenuLocked)
        {
            if (paused) //if the game is already paused, resume the game
            {
                //.lockState = CursorLockMode.Locked;
                resumeGame();
            }
            else //if the game isn't paused, pause the game
            {
                //Cursor.lockState = CursorLockMode.None;
                pauseGame();
            }
        }
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    updateShipStatusBar(13);
        //}
        updateHealthBar(FirstPersonController.PlayerHealth);
        updateShipStatusBar(ShipPickupManager.currentShipPartsFound);
    }

    #region "objectives"

    void createLevel1Objectives()
    {
        objCont.createNewObjective("Where'd my ship go?", "Retrieve some of your ship parts 0/6", 2);
        objCont.createNewObjective("Onwards and upwards", "Find a way to the next area", 2);
    }

    void createLevel2Objectives()
    {
        objCont.createNewObjective("Picking up the pieces", "Retrieve some more of your ship parts 0/6", 3);
        objCont.createNewObjective("More onwards and more upwards", "Find a way to the next area", 3);
    }

    void createLevel3Objectives()
    {
        objCont.createNewObjective("The final piece of the puzzle", "Find the last part of your ship 0/1", 4);
        objCont.createNewObjective("Something's not right...", "Find the source of the honking", 4);
    }

    void updateLevel1Objectives()
    {
        objCont.updateLevel1Objs(level1Parts);
    }

    void updateLevel2Objectives()
    {
        objCont.updateLevel2Objs(level2Parts);
    }

    void updateLevel3Objectives()
    {
        objCont.updateLevel3Objs(level3Parts);
    }

    #endregion

    #region "pause menu"

    void pauseGame()
    {
        paused = true;
        Cursor.visible = true;
        Time.timeScale = 0f; //set the timescale to 0, freezing the game
        pauseMenuObject.SetActive(true); //show the pause menu
        MouseCamLook.LOCKCAMROT = true;
        Cursor.lockState = CursorLockMode.None;

    }

    void resumeGame()
    {
        paused = false;
        Cursor.visible = false;
        Time.timeScale = 1f; //set the timescale to 1, unfreezing the game
        pauseMenuObject.SetActive(false); //hide the pause menu
        MouseCamLook.LOCKCAMROT = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        FirstPersonController.PlayerHealth = 100;
        paused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    #endregion

    #region "death screen"

    public void showDeathScreen()
    {
        Time.timeScale = 0f; //pauses the game
        pauseMenuLocked = true;
        deathScreenObject.SetActive(true);
        MouseCamLook.LOCKCAMROT = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void retryButtonPressed()
    {
        FirstPersonController.showGAMEOVER = false;
        FirstPersonController.PlayerHealth = 100.0f;
        deathScreenObject.SetActive(false);
        Time.timeScale = 1f; //unpauses the game
        pauseMenuLocked = false;
        resumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //reloads the current scene
    }

    public void mainMenuButtonPressed()
    {
        Time.timeScale = 1f; //unpauses the game
        pauseMenuLocked = false;
        SceneManager.LoadScene(0); //loads the main menu scene
    }

    #endregion

    #region "bars and UI"
    
    /// <summary>
    /// Creates a new message and displays it on the screen
    /// </summary>
    /// <param name="name">The header of the message (optional, can leave blank)</param>
    /// <param name="desc">The main body of the message</param>
    public void createNewMessage(string name, string desc)
    {
        Text msgName = null;
        Text msgDesc = null;
        Transform newMessage = Instantiate(messageTransform, messageParent);
        Text[] msgChildTexts = newMessage.GetComponentsInChildren<Text>();
        for (int i = 0; i < msgChildTexts.Length; i++)
        {
            if (msgChildTexts[i].name == "name")
            {
                msgName = msgChildTexts[i];
            } else if (msgChildTexts[i].name == "desc")
            {
                msgDesc = msgChildTexts[i];
            }
        }
        msgName.text = name;
        msgDesc.text = desc;
        displayMessage(msgName, msgDesc);
    }

    void displayMessage(Text name, Text desc)
    {
        StartCoroutine(FadeIn(name, desc));
        StartCoroutine(FadeOut(name, desc));
    }

    /// <summary>
    /// Fades an image into the screen
    /// </summary>
    /// <param name="t1">The first text to be faded/param>
    /// <param name="t2">The second test to be faded</param>
    /// <returns></returns>
    IEnumerator FadeIn(Text t1, Text t2)
    {
        //fade from transparent to opaque
        // loop over 5 seconds
        for (float i = 0; i <= 5; i += Time.deltaTime)
        {
            // set color with i as alpha
            t1.color = new Color(0, 204, 255, i);
            t2.color = new Color(0, 204, 255, i);
            yield return null;
        }
    }

    /// <summary>
    /// Fades an out from the screen
    /// </summary>
    /// <param name="t1">The first text to be faded</param>
    /// <param name="t2">The second text to be faded</param>
    /// <returns></returns>
    IEnumerator FadeOut(Text t1, Text t2)
    {
        for (float i = 0; i < 3; i += Time.deltaTime)
        {
            //keeps the image on screen for 3 seconds
            yield return null;
        }
        // fade from opaque to transparent
        // loop over 5 seconds backwards
        for (float i = 5; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            t1.color = new Color(0, 204, 255, i);
            t2.color = new Color(0, 204, 255, i);
            yield return null;
        }
        t1.gameObject.transform.parent.gameObject.SetActive(false); //disabled the message
    }

    /// <summary>
    /// Show the bosses health bar
    /// </summary>
    public void showBossBar()
    {
        bossBarObject.SetActive(true);
    }

    /// <summary>
    /// Hide the bosses health bar
    /// </summary>
    public void hideBossBar()
    {
        bossBarObject.SetActive(false);
    }

    /// <summary>
    /// Updates the boss health bar to a given health
    /// </summary>
    /// <param name="health">The new health of the boss in percent</param>
    public void updateBossHealthBar(float health)
    {
        setBarWidth(bossHealthBarObject, health * 12, 60f);
    }

    /// <summary>
    /// Updates the health bar to a given health
    /// </summary>
    /// <param name="health">Player's new health in percentage of max health</param>
    public void updateHealthBar(float health)
    {
        if (health > currHealth)
        {
            //float healthDiff = health - currHealth;
            createNewMessage("Pickup", "You gained " + (health - currHealth) + " health!");
        }
        currHealth = health;
        setBarWidth(healthBarObject, health * 5, 100f);
    }

    /// <summary>
    /// Updates the stamina bar to a given stamina
    /// </summary>
    /// <param name="stamina">Player's new stamina in percentage of max stamina</param>
    public void updateStaminaBar(float stamina)
    {
        setBarWidth(healthBarObject, stamina * 3, 100f);
    }

    /// <summary>
    /// Updates the ship status bar to a given value
    /// </summary>
    /// <param name="shipStatus">The number of the ship parts collected (out of 13)</param>
    public void updateShipStatusBar(float shipStatus)
    {
        if (shipStatus > currParts)
        {
            switch (currentScene)
            {
                case 2:
                    level1Parts++;
                    break;
                case 3:
                    level2Parts++;
                    break;
                case 4:
                    level3Parts++;
                    break;
            }
            createNewMessage("Pickup", "You found a part of your ship! Only " + (13 - shipStatus) + " to go");
        }
        currParts = shipStatus;
        Text numberText = shipStatusBarObject.GetComponentInChildren<Text>();
        setBarWidth(shipStatusBarObject, shipStatus * 100, 30f);
        numberText.text = shipStatus + "/13";
        if (shipStatus == 13)
        {
            goHome();
        }
    }

    void goHome()
    {
        clearObjectives();
        objCont.createNewObjective("Go home", "");
    }

    void clearObjectives()
    {
        objCont.clearObjs();
    }

    /// <summary>
    /// Updates the name of the currently equipped weapon
    /// </summary>
    /// <param name="newWeaponName">The name of the new weapon</param>
    public void updateWeapon(string newWeaponName)
    {
        weaponName.text = newWeaponName;
    }

    /// <summary>
    /// Updates the current and max ammo
    /// </summary>
    /// <param name="currAmmo">The current ammo of the player's weapon</param>
    /// <param name="maxAmmo">The maximum ammo of the player's weapon</param>
    public void updateAmmo(int currAmmo, int maxAmmo)
    {
        ammoText.text = currAmmo + "/" + maxAmmo;
    }

    /// <summary>
    /// Retrieves the width of a given bar
    /// </summary>
    /// <param name="bar">The bar to retrieve the width of</param>
    /// <returns>The width of the given bar</returns>
    float getBarWidth(GameObject bar)
    {
        RectTransform rt = (RectTransform)bar.transform;
        float barWidth = rt.rect.width;
        return barWidth;
    }

    /// <summary>
    /// Sets a given bar's width to a given value
    /// </summary>
    /// <param name="bar">Bar that will have its width changed</param>
    /// <param name="newWidth">The new width of the bar</param>
    void setBarWidth(GameObject bar, float newWidth, float newHeight)
    {
        RectTransform rt = (RectTransform)bar.transform;
        rt.sizeDelta = new Vector2(newWidth, newHeight);
    }

    #endregion

}
