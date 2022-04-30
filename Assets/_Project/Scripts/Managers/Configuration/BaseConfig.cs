using System;
using UnityEngine;

public abstract class BaseConfig<T>
{

    public delegate void UpdateHandler(T value);
    public event UpdateHandler OnUpdate;
    
    protected const string PREFS_PREFIX = "CONFIG_";

    protected string _name;
    protected Func<T, float> _parseAction;

    protected T _value;
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            SaveValue(_value);
            OnUpdate?.Invoke(_value);
        }
    }

    public BaseConfig(string name, T defautValue)
    {
        _name = PREFS_PREFIX + name;
        _value = GetValue(defautValue);
    }

    protected abstract T GetValue(T defautValue);
    protected abstract T SaveValue(T value);

    public void AddParser(Func<T, float> action)
    {
        _parseAction = action;
    }

    public float Parse()
    {
        return _parseAction.Invoke(_value);
    }

}