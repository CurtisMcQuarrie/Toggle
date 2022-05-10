using UnityEngine;
/* GameManager
 * Purpose:
 *      Handles UI interactions.
 *      Handles Scene Changes.
 */
public class GameManager : MonoBehaviour
{
    #region fields

    public Difficulty difficulty = Difficulty.Easy;

    #endregion

    #region monobehaviour

    void Awake()
    {
        DontDestroyOnLoad(this); // make it persist between scenes
    }
    
    #endregion
}
