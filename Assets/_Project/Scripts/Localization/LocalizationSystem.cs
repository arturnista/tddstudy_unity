using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem : ILocalizationSystem
{

    private Dictionary<string, string> _keys;

    public LocalizationSystem()
    {
        _keys = new Dictionary<string, string>()
        {
            { "UI_RIGHT", "Right" },
            { "UI_LEFT", "Left" },
            { "UI_UP", "Up" },
            { "UI_DOWN", "Down" },
            { "UI_SCORE", "Score" },
        };
    }
    
    public string GetTranslation(string key)
    {
        if (!_keys.ContainsKey(key))
        {
            Debug.LogError($"Key {key} not localized!");
            return "";
        }
        return _keys[key];
    }
    
}
