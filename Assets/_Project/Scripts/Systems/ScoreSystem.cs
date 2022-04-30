using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : IScoreSystem
{

    public int Score { get; protected set; }

    public void AddScore(int amount = 1)
    {
        Score += amount;
    }

}
