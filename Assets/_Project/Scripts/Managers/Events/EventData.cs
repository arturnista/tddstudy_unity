using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData
{
    
    private GameObject _gameObject;
    public GameObject GameObject => _gameObject;

    public EventData(GameObject gameObject)
    {
        _gameObject = gameObject;
    }

}
