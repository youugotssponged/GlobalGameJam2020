// Unity Libs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


// .NET Libs
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Utility Class Made by Jordan McCann 11th January 2020
/// </summary>
public class JUtillity
{
    #region Editor Related - Internal
    #endregion 

    #region Runtime Related

    /// <summary>
    /// RunAfter Function
    /// </summary>
    /// <param name="seconds">seconds of time to wait for before running the callback</param>
    /// <param name="callback">passable function</param>
    /// <returns>Coroutine</returns>
    public static IEnumerator RunAfter(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback();
    }

    #endregion


    #region Scene Related

    // Closes application
    public static void ExitGame()
    {
        Application.Quit();
    }


    /// <summary>
    /// LoadScene function
    /// </summary>
    /// <param name="scene_index">index number from build scene index to point to what level to load</param>
    public static void LoadScene(int scene_index)
    {
        SceneManager.LoadScene(scene_index);
    }

    #endregion


    #region Math Related

    //  Checks to see if a value is between the specified min and max
    public static bool IsBetween(float value, float min, float max)
    {
        bool check = (value > max && value < min);
        return check;
    }

    // Constraints a value to be between two other values
    public static float Constraint(float value, float min, float max)
    {
        float constraint = Mathf.Max(Mathf.Min(value, max), min);
        return constraint;
    }

    #endregion

}

#region Editor Related - External - GLOBAL ACCESS

    // Custom Decorator for slapping onto public variables that need to be attached through the editor
    // This decorator 
    [CustomPropertyDrawer(typeof(WarnIfObjectEmpty))]
    class RequiredObjectField : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
            WarnIfObjectEmpty warn = attribute as WarnIfObjectEmpty;

            if(property.objectReferenceValue == null){
                GUI.color = warn.color;
                EditorGUI.PropertyField(position, property, label);
                GUI.color = Color.white;
            }
            else {
                GUI.color = Color.green;
                EditorGUI.PropertyField(position, property, label);
                GUI.color = Color.white;
            }

        }
    }
    
    [CustomPropertyDrawer(typeof(WarnIfBoolEmpty))]
    class RequiredBoolField : PropertyDrawer {  
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            WarnIfBoolEmpty warnBool = attribute as WarnIfBoolEmpty; 
            if (property.boolValue == false) {
                    GUI.color = warnBool.color;
                    EditorGUI.PropertyField(position, property, label);
                    GUI.color = Color.white;
            }
            else {
                    GUI.color = Color.green;
                    EditorGUI.PropertyField(position, property, label);
                    GUI.color = Color.white;
            }
        }
    }


    // Wrapper for the Decorator
    class WarnIfObjectEmpty : PropertyAttribute {
        public Color color = Color.red;
    }

    class WarnIfBoolEmpty : PropertyAttribute {
        public Color color = Color.red;
    }


#endregion