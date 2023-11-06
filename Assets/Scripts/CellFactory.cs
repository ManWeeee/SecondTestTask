using Zenject;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

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
        return Object.Instantiate(instance);
    }
}
