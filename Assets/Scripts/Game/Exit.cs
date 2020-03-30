using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Exit : MonoBehaviour
{
    [Inject] ILevelManager levelManager;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        levelManager.SwitchToNextLevel();
    }
}
