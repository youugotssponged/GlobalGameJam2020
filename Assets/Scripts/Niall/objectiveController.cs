using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class objectiveController : MonoBehaviour
{
    public Transform parentTransform;
    public GameObject objPrefab;
    List<Objective> currLevelObjList = new List<Objective>();
    List<Objective> level1ObjList = new List<Objective>();
    List<Objective> level2ObjList = new List<Objective>();
    List<Objective> level3ObjList = new List<Objective>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                currLevelObjList = level1ObjList;
                break;
            case 2:
                currLevelObjList = level2ObjList;
                break;
            case 3:
                currLevelObjList = level3ObjList;
                break;
        }
        if (currLevelObjList.Count > 0)
        {
            updateObjectivesList();
        }
    }

    public bool isEmpty(int level)
    {
        switch (level)
        {
            case 1:
                if (level1ObjList.Count > 0)
                {
                    return false;
                } else
                {
                    return true;
                }
                break;
            case 2:
                if (level2ObjList.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                break;
            case 3:
                if (level3ObjList.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                break;
            default:
                return false;
                break;
        }
    }

    /// <summary>
    /// Creates a new objective
    /// </summary>
    /// <param name="objName">Name of the objective</param>
    /// <param name="objDesc">Description of the objective's requirements</param>
    public void createNewObjective(string objName, string objDesc, int levelIndex)
    {
        Objective obj = new Objective(currLevelObjList.Count, objName, objDesc, objPrefab.transform, parentTransform);
        switch (levelIndex)
        {
            case 1:
                obj.setIndex(level1ObjList.Count);
                level1ObjList.Add(obj);
                break;
            case 2:
                obj.setIndex(level2ObjList.Count);
                level2ObjList.Add(obj);
                break;
            case 3:
                obj.setIndex(level3ObjList.Count);
                level3ObjList.Add(obj);
                break;
        }
        addObjectiveToScreen(obj);
    }

    public void updateLevel1Objs(int lvl1Parts)
    {
        if (level1ObjList[0].getName() == "Where'd my ship go?")
        {
            level1ObjList[0].setDesc("Retrieve some of your ship parts " + lvl1Parts + "/6");
            level1ObjList[0].setCompleted(checkCompleted(level1ObjList[0], lvl1Parts));
        }
    }

    public void updateLevel2Objs(int lvl2Parts)
    {
        if (level2ObjList[0].getName() == "Picking up the pieces")
        {
            level2ObjList[0].setDesc("Retrieve some more of your ship parts " + lvl2Parts + "/6");
            level2ObjList[0].setCompleted(checkCompleted(level2ObjList[0], lvl2Parts));
        }
    }

    public void updateLevel3Objs(int lvl3Parts)
    {
        if (level3ObjList[0].getName() == "The final piece of the puzzle")
        {
            level3ObjList[0].setDesc("Find the last part of your ship " + lvl3Parts + "/6");
            level3ObjList[0].setCompleted(checkCompleted(level3ObjList[0], lvl3Parts));
        }
    }

    /// <summary>
    /// Updates the list of objectives on the screen
    /// </summary>
    void updateObjectivesList()
    {
        foreach (Objective obj in currLevelObjList) //loops through the player's current objectives
        {
            if (obj.getCompleted()) //if the objective is completed remove it from the list
            {
                removeObjective(obj);
                break; //breaks out of the foreach loop so the program doesn't loop through an element that isn't there (stops an enumeration error)
            } else //if the objective is not completed, make sure it's in the right place on the screeni
            {
                obj.setIndex(currLevelObjList.BinarySearch(obj)); //sets the objective's new index in the on screen list to its index in the list
                obj.updatePosition();
            }
        }
    }

    bool checkCompleted(Objective o, int r)
    {
        if (level1ObjList.Contains(o))
        {
            if (o.getName() == "Where'd my ship go?")
            {
                if (r == 6)
                {
                    return true;
                } else
                {
                    return false;
                }
            } else if (o.getName() == "Onwards and upwards")
            {
                //if the portal is open then completed TODO
                return false;
            } else
            {
                return false;
            }
        } else if (level2ObjList.Contains(o))
        {
            if (o.getName() == "Picking up the pieces")
            {
                if (r == 6)
                {
                    return true;
                } else
                {
                    return false;
                }
            } else if (o.getName() == "More onwards and more upwards")
            {
                //if the portal is open then completed TODO
                return false;
            } else
            {
                return false;
            }
        } else if (level3ObjList.Contains(o))
        {
            if (o.getName() == "The final piece of the puzzle")
            {
                if (r == 1)
                {
                    return true;
                } else
                {
                    return false;
                }
            } else if (o.getName() == "Something's not right...")
            {
                //when boss appears, complete this objective and add a new one to kill the boss
                return false;
            } else
            {
                return false;
            }
        } else
        {
            return false;
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
        currLevelObjList.Remove(obj);
    }

    public void testCompleteObj()
    {
        currLevelObjList[0].setCompleted(true);
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
        body.localPosition = new Vector3(0, -120 - (index * 180), 0);
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

    public string getName()
    {
        return objName;
    }

    public void setDesc(string d)
    {
        objDesc = d;
        displayDetails();
    }

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
