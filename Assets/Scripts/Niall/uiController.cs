using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiController : MonoBehaviour
{
    public GameObject healthBarObject;
    public GameObject staminaBarObject;
    // Start is called before the first frame update
    void Start()
    {
        //updateHealthBar(50); //sets the health bar to 50%
    }

    // Update is called once per frame
    void Update()
    {

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
