using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScreenShake
{

    void Shake(float strength, float time);
    void Stop();
    void Enable();
    void Disable();
    
}
