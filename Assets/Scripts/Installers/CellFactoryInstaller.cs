using Zenject;

public class CellFactoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CellFactory>().AsSingle();
    }
}