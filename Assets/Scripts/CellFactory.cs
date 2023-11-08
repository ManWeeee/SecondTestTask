using UnityEngine;
using Zenject;

public class CellFactory : IFactory<Cell>
{
    private DiContainer _diContainer;

    CellFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }
    public Cell Create()
    {
        var instance = _diContainer.Resolve<Cell>();
        _diContainer.Inject(instance);
        return Object.Instantiate(instance);
    }
}
