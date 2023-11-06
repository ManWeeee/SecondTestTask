using UnityEngine;
using Zenject;

public class CellInstaller : MonoInstaller
{
    [SerializeField] private Cell cellPrefab;
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<Cell>()
            .FromInstance(cellPrefab)
            .AsSingle();
    }
}