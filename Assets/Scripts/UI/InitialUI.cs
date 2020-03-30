
using UnityEngine;

public class InitialUI : MonoBehaviour
{
    public void NewGame()
    {
        LevelManager.Instance.NewGame();
    }

    public void ContinueGame()
    {
        LevelManager.Instance.ContinueGame();
    }
}
