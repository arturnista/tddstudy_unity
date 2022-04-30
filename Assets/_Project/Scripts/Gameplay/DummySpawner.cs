using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class DummySpawner : MonoBehaviour
{
    
    private DummyEntity.Factory _dummyFactory;
    
    [Inject]
    private void Construct(DummyEntity.Factory dummyFactory)
    {
        _dummyFactory = dummyFactory;
    }
    
    private void Start()
    {
        var dummy = _dummyFactory.Create();
        dummy.transform.position = new Vector3(0.5f, 0.5f);
    }
    
}
