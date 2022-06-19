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
        DontDestroyOnLoad(transform.parent.gameObject); // make it persist between scenes
    }

    #endregion

    #region public methods

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion

}
