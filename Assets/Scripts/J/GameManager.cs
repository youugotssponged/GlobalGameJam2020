using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton Declaration to ensure 
    // Single Instance of GameManager is available with no copies
    private static GameManager _instance;
    public static GameManager Instance {
        get{
            if(_instance == null){
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    private void Awake(){
        DontDestroyOnLoad(gameObject);
    }
    ///////////////////////////////////////



    // Data variables that need to be carried across levels
    [SerializeField]  [Range(0, 100)] private float playerHealth = 100;
    private int LevelIndex = 0;

    // Game States
    public GameStates currentState;
    public enum GameStates{
        GAME_START,
        GAME_END,
        SHIP_FIXED,
        SHIP_NOT_FIXED,
    }

}
