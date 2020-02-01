using System.Collections;
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
    public Transform messageParent;
    public Transform messageTransform;
    bool pauseMenuLocked;
    bool paused;
    Text[] childTexts;
    Text weaponName;
    Text ammoText;
    // Start is called before the first frame update
    void Start()
    {
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
        //updateWeapon("Ray gun"); //testing
        //updateAmmo(100, 100);
        //createNewMessage("Tutorial", "Do stuff");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape") && !pauseMenuLocked)
        {
            if (paused) //if the game is already paused, resume the game
            {
                resumeGame();
            }
            else //if the game isn't paused, pause the game
            {
                pauseGame();
            }
        }
    }

    #region "pause menu"

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
    }

    public void retryButtonPressed()
    {
        Time.timeScale = 1f; //unpauses the game
        pauseMenuLocked = false;
        deathScreenObject.SetActive(false);
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
            t1.color = new Color(0, 0, 0, i);
            t2.color = new Color(0, 0, 0, i);
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
        for (float i = 0; i < 10; i += Time.deltaTime)
        {
            //keeps the image on screen for 10 seconds
            yield return null;
        }
        // fade from opaque to transparent
        // loop over 5 seconds backwards
        for (float i = 5; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            t1.color = new Color(0, 0, 0, i);
            t2.color = new Color(0, 0, 0, i);
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
        setBarWidth(bossHealthBarObject, health * 12);
    }

    /// <summary>
    /// Updates the health bar to a given health
    /// </summary>
    /// <param name="health">Player's new health in percentage of max health</param>
    public void updateHealthBar(float health)
    {
        if (health == 0)
        {
            showDeathScreen();
        }
        setBarWidth(healthBarObject, health * 5);
    }

    /// <summary>
    /// Updates the stamina bar to a given stamina
    /// </summary>
    /// <param name="stamina">Player's new stamina in percentage of max stamina</param>
    public void updateStaminaBar(float stamina)
    {
        setBarWidth(healthBarObject, stamina * 3);
    }

    /// <summary>
    /// Updates the ship status bar to a given value
    /// </summary>
    /// <param name="shipStatus">The percantage of the ship that is fixed</param>
    public void updateShipStatusBar(float shipStatus)
    {
        setBarWidth(shipStatusBarObject, shipStatus * 10);
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
    void setBarWidth(GameObject bar, float newWidth)
    {
        RectTransform rt = (RectTransform)bar.transform;
        rt.sizeDelta = new Vector2(newWidth, 100f);
    }

    #endregion

}
