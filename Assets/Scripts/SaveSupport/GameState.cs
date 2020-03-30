using System;
using UnityEngine;

[Serializable]
public class GameState
{
    public int CurrentLevel;
    public PlayerState playerState = new PlayerState();
    public MovingPlatformState platformState = new MovingPlatformState();
}
