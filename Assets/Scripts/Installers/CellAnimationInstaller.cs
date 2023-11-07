using UnityEngine;
using Zenject;

public class CellAnimationInstaller : MonoInstaller
{
    [SerializeField] private CellAnimation cellAnimationPrefab;
    public override void InstallBindings()
    {
        Container
        .Bind<CellAnimation>()
        .FromInstance(cellAnimationPrefab)
        .AsSingle();
    }
}