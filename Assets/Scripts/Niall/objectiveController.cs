using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectiveController : MonoBehaviour
{
    public Transform parentTransform;
    public GameObject objPrefab;
    List<Objective> currentObjList = new List<Objective>();
    // Start is called before the first frame update
    void Start()
    {
        createNewObjective("Where's that bloody stick?", "Find the ship's control stick");
        createNewObjective("What about the engine?", "Find the ship's engine rotor");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentObjList.Count > 0)
        {
            updateObjectivesList();
        }
    }

    /// <summary>
    /// Creates a new objective
    /// </summary>
    /// <param name="objName">Name of the objective</param>
    /// <param name="objDesc">Description of the objective's requirements</param>
    void createNewObjective(string objName, string objDesc)
    {
        Objective obj = new Objective(currentObjList.Count, objName, objDesc, objPrefab.transform, parentTransform);
        currentObjList.Add(obj);
        addObjectiveToScreen(obj);
    }

    /// <summary>
    /// Updates the list of objectives on the screen
    /// </summary>
    void updateObjectivesList()
    {
        foreach (Objective obj in currentObjList) //loops through the player's current objectives
        {
            if (obj.getCompleted()) //if the objective is completed remove it from the list
            {
                removeObjective(obj);
                break; //breaks out of the foreach loop so the program doesn't loop through an element that isn't there (stops an enumeration error)
            } else //if the objective is not completed, make sure it's in the right place on the screeni
            {
                obj.setIndex(currentObjList.BinarySearch(obj)); //sets the objective's new index in the on screen list to its index in the list
                obj.updatePosition();
            }
        }
    }

    void addObjectiveToScreen(Objective obj)
    {
        obj.instantiateSelf();
    }

    void removeObjective(Objective obj)
    {
        obj.setOnScreen(false);
        obj.getTransform().gameObject.SetActive(false);
        currentObjList.Remove(obj);
    }

    public void testCompleteObj()
    {
        currentObjList[0].setCompleted(true);
    }

}

class Objective : MonoBehaviour, IComparable<Objective>
{
    Transform body;
    Transform prefab;
    Transform parentTransform;
    int index;
    string objName;
    string objDesc;
    bool completed;
    bool onScreen;
    public Objective(int i, string name, string desc, Transform prefab, Transform parent)
    {
        parentTransform = parent;
        this.prefab = prefab;
        index = i;
        objName = name;
        objDesc = desc;
        completed = false;
        onScreen = false;
    }

    int IComparable<Objective>.CompareTo(Objective obj) //this is used for binary searching and stops an error pls no remove >:(
    {
        if (this.index > obj.index)
            return 1;
        else if (this.index == obj.index)
            return 0;
        else
            return -1;
    }

    public void instantiateSelf()
    {
        body = Instantiate(prefab, parentTransform);
        updatePosition();
        displayDetails();
    }

    /// <summary>
    /// Updates the objective's position on the screen
    /// </summary>
    public void updatePosition()
    {
        body.localPosition = new Vector3(0, -100 - (index * 100), 0);
    }

    void displayDetails()
    {
        Text[] childTexts = body.GetComponentsInChildren<Text>();
        for (int i = 0; i < childTexts.Length; i++)
        {
            if (childTexts[i].name == "name")
            {
                childTexts[i].text = objName;
            } else if (childTexts[i].name == "desc")
            {
                childTexts[i].text = objDesc;
            }
        }
    }

    #region "getters and setters"

    public void setIndex(int i)
    {
        index = i;
    }

    public int getIndex()
    {
        return index;
    }

    public Transform getTransform()
    {
        return body;
    }

    public bool getCompleted()
    {
        return completed;
    }

    public void setCompleted(bool c)
    {
        completed = c;
        if (c)
        {
            Toggle tg = body.GetComponent<Toggle>();
            tg.isOn = true;
        }
    }

    public bool getOnScreen()
    {
        return onScreen;
    }

    public void setOnScreen(bool s)
    {
        body.gameObject.SetActive(s);
        onScreen = s;
    }

    #endregion

}
