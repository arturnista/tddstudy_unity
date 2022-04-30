using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

public class AudioLibrary : MonoBehaviour
{
    
    [SerializeField] private GenericDictionary<string, AudioItem> _library;
    
    [Inject]
    private void Construct(SoundEffectsSystem sfxSystem)
    {
        sfxSystem.RegisterLibrary(_library);
    }

}
