using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        LevelManager.Instance.SwitchToNextLevel();
    }
}
