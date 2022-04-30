using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputManager
{

    IAxisKey Movement { get; }
    InputKey Pause { get; }

    void EnableUI();
    void EnableGameplay();

}
