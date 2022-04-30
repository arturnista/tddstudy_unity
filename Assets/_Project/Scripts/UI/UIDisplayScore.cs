using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class UIDisplayScore : MonoBehaviour
{

    private TextMeshProUGUI _text;

    private IScoreSystem _scoreSystem;
    private ILocalizationSystem _localizationSystem;
    private int _lastScore;

    [Inject]
    private void Construct(IScoreSystem scoreSystem, ILocalizationSystem localizationSystem)
    {
        _scoreSystem = scoreSystem;
        _localizationSystem = localizationSystem;
        _text = GetComponent<TextMeshProUGUI>();
        UpdateScore();
    }

    private void Update()
    {
        if (_lastScore != _scoreSystem.Score)
        {
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        _text.text = _localizationSystem.GetTranslation("UI_SCORE") + ": " + _scoreSystem.Score;
        _lastScore = _scoreSystem.Score;
    }
}
