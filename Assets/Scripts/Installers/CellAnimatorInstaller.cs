using UnityEngine;
using Zenject;

public class CellAnimatorInstaller : MonoInstaller
{
    [SerializeField] private CellAnimator cellAnimatorPrefab;
    public override void InstallBindings()
    {
        Container
            .Bind<CellAnimator>()
            .FromInstance(cellAnimatorPrefab)
            .AsSingle();
    }
}