using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectiveScript : MonoBehaviour
{
    GameObject body;
    int index;
    string objName;
    string objDesc;
    bool completed;
    bool onScreen;
    // Start is called before the first frame update
    void Start()
    {
        completed = false;
        onScreen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region "getters and setters"

    public int getIndex()
    {
        return index;
    }

    public GameObject getGameObject()
    {
        return body;
    }

    public bool getCompleted()
    {
        return completed;
    }

    public bool getOnScreen()
    {
        return onScreen;
    }

    public void setOnScreen(bool s)
    {
        onScreen = s;
    }

    #endregion

}
