using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* GameManager
 * Purpose:
 *      Handles UI interactions.
 *      Handles Scene Changes.
 */ 
public class GameManager : MonoBehaviour
{
    #region fields

    public Difficulty difficulty;

    #endregion

    #region monobehaviour
    void Awake()
    {
        DontDestroyOnLoad(this); // make it persist between scenes
        difficulty = Difficulty.Easy;
    }
    
    void Update()
    {
        
    }
    #endregion
}
