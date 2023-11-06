using System;
using UnityEngine;
using Zenject;

public class BoardInstaller : MonoInstaller
{
    [SerializeField] private Board boardPrefab;
    public override void InstallBindings()
    {
        Container
            .Bind<Board>()
            .FromInstance(boardPrefab)
            .AsSingle();
    }
}