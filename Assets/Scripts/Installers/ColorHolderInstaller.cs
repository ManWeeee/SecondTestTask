using UnityEngine;
using Zenject;

public class ColorHolderInstaller : MonoInstaller
{
    [SerializeField] private ColorHolder colorHolder;
    public override void InstallBindings()
    {
        Container
            .Bind<ColorHolder>()
            .FromInstance(colorHolder)
            .AsSingle();
    }
}