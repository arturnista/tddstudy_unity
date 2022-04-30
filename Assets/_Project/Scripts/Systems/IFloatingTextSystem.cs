using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFloatingTextSystem
{

    void Create(string message, Vector3 position, float time = 1f);
    void Create(string message, Vector3 position, Color color, float time = 1f, float size = 7f);

}
