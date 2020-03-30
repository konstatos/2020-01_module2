using System;
using UnityEngine;

[Serializable]
public class PlayerState
{
    public RigidBodyState body = new RigidBodyState();
    public float visualDirection;
}
