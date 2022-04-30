using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MusicLibrary : MonoBehaviour
{
    
    [SerializeField] private GenericDictionary<string, MusicItem> _library;
    
    [Inject]
    private void Construct(MusicSystem musicSystem)
    {
        musicSystem.RegisterLibrary(_library);
    }

}
