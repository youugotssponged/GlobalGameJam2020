using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void updateObjectivesList()
    {
        foreach (Objective obj in currentObjList)
        {
            if (obj.getCompleted()) //if the objective is completed remove it from the list
            {
                removeObjective(obj);
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

}

class Objective : MonoBehaviour
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
        Debug.Log("New objective, " + name + "!");
    }

    public void instantiateSelf()
    {
        //body = Instantiate(prefab, new Vector3(0, -70 - (index * 130), 0), Quaternion.identity, parentTransform);
        body = Instantiate(prefab, parentTransform);
        body.localPosition = new Vector3(0, -70 - (index * 130), 0);
        //if (index == 0)
        //{
        //    body.localPosition = new Vector3(0, parentTransform.position.z - 70, 0);
        //} else
        //{
        //}
        Debug.Log(body.position);
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

    public bool getOnScreen()
    {
        return onScreen;
    }

    public void setOnScreen(bool s)
    {
        body.gameObject.SetActive(s);
        onScreen = s;
    }

}
