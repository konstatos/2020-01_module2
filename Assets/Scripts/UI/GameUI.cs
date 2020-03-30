
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public void Save()
    {
    }

    public void Load()
    {
    }

    public void Exit()
    {
        SceneManager.LoadScene("Initial");
    }
}
