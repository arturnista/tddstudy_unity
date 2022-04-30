using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreSystem
{
    
    int Score { get; }
    void AddScore(int amount = 1);

}
