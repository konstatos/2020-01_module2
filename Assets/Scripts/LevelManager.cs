
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : ILevelManager
{
    const int LevelCount = 2;
    string SaveFile => $"{Application.persistentDataPath}/SaveGame.txt";
    int CurrentLevel;

    void Awake()
    {
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
    }

    void SaveCurrentLevel()
    {
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.Save();
    }

    public void NewGame()
    {
        CurrentLevel = 0;
        SaveCurrentLevel();
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
        SaveCurrentLevel();
        PlayCurrentLevel();
    }

    public void SaveGame()
    {
        GameState state = new GameState();
        state.CurrentLevel = CurrentLevel;
        foreach (ISaveable obj in Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
            obj.Save(state);

        using (FileStream file = File.Create(SaveFile)) {
            new BinaryFormatter().Serialize(file, state);
        }

        /*
        var json = JsonUtility.ToJson(state, prettyPrint: true);
        File.WriteAllText(SaveFile, json);
        */
    }

    public void LoadGame()
    {
        if (File.Exists(SaveFile)) {
            GameState state;

            /*
            var json = File.ReadAllText(SaveFile);
            state = JsonUtility.FromJson<GameState>(json);
            */

            using (FileStream file = File.Open(SaveFile, FileMode.Open)) {
                state = new BinaryFormatter().Deserialize(file) as GameState;
            }

            //StartCoroutine(LoadGame(state));
        }
    }

    IEnumerator LoadGame(GameState state)
    {
        CurrentLevel = state.CurrentLevel;
        SaveCurrentLevel();

        AsyncOperation loading = SceneManager.LoadSceneAsync($"Level{CurrentLevel+1}");
        while (!loading.isDone)
            yield return null;

        foreach (ISaveable obj in Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
            obj.Restore(state);
    }
}
