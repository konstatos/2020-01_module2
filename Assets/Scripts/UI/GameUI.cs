
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameUI : MonoBehaviour
{
    [Inject] ILevelManager levelManager;

    public void Save()
    {
        levelManager.SaveGame();
    }

    public void Load()
    {
        levelManager.LoadGame();
    }

    public void Exit()
    {
        SceneManager.LoadScene("Initial");
    }
}
