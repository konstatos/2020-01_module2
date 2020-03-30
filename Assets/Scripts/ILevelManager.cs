
using UnityEngine;

public interface ILevelManager
{
    void NewGame();
    void ContinueGame();
    void LoadGame();
    void SaveGame();
    void SwitchToNextLevel();
}
