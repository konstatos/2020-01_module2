
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    const int LevelCount = 2;
    int CurrentLevel;

    public void NewGame()
    {
        CurrentLevel = 0;
        PlayCurrentLevel();
    }

    public void ContinueGame()
    {
        if (CurrentLevel >= LevelCount)
            NewGame();
        else
            PlayCurrentLevel();
    }

    public void PlayCurrentLevel()
    {
        if (CurrentLevel < LevelCount)
            SceneManager.LoadScene($"Level{CurrentLevel+1}");
        else
            SceneManager.LoadScene($"Initial");
    }

    public void SwitchToNextLevel()
    {
        ++CurrentLevel;
        PlayCurrentLevel();
    }
}
