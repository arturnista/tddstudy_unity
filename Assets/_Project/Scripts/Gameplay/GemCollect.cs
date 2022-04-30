using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollect
{
    private int _scoreAmount;
    public int ScoreAmount => _scoreAmount;

    private IScoreSystem _scoreSystem;

    private bool _isActive;
    public bool IsActive => _isActive;

    public GemCollect(int scoreAmount, IScoreSystem scoreSystem)
    {
        _scoreAmount = scoreAmount;
        _scoreSystem = scoreSystem;

        _isActive = true;
    }

    public bool OnCollect()
    {
        if (!_isActive) return false;

        _isActive = false;
        _scoreSystem.AddScore(_scoreAmount);
        return true;
    }

    public void OnRespawn()
    {
        _isActive = true;
    }

}
