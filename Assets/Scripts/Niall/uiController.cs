using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiController : MonoBehaviour
{
    public GameObject healthBarObject;
    public GameObject staminaBarObject;
    public GameObject shipStatusBarObject;
    public GameObject ammoObject;
    public GameObject bossHealthBarObject;
    public GameObject bossBarObject;
    public Transform messageParent;
    public Transform messageTransform;
    Text[] childTexts;
    Text weaponName;
    Text ammoText;
    // Start is called before the first frame update
    void Start()
    {
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
        updateWeapon("Ray gun"); //testing
        updateAmmo(100, 100);
        createNewMessage("Tutorial", "Do stuff");
    }

    // Update is called once per frame
    void Update()
    {

    }

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
        fadeInMessage(newMessage);
    }

    void fadeInMessage(Transform message)
    {
        Image img = message.GetComponentInChildren<Image>();
        StartCoroutine(FadeImage(img, true));
    }

    /// <summary>
    /// Fades an image in or out
    /// </summary>
    /// <param name="img">The image to be faded</param>
    /// <param name="fadeAway">True means the image will fade away, false means the image will fade in</param>
    /// <returns></returns>
    IEnumerator FadeImage(Image img, bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 2 seconds backwards
            for (float i = 2; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 2 seconds
            for (float i = 0; i <= 2; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
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

}
