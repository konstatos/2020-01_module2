using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName="ZenjectInstaller")]
public class ZenjectInstaller : ScriptableObjectInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ILevelManager>().To<LevelManager>().AsSingle();
    }
}
