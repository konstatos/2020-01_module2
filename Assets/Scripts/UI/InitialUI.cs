
using UnityEngine;
using Zenject;

public class InitialUI : MonoBehaviour
{
    [Inject] ILevelManager levelManager;

    public void NewGame()
    {
        levelManager.NewGame();
    }

    public void ContinueGame()
    {
        levelManager.ContinueGame();
    }
}
