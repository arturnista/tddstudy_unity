using UnityEngine;

public class IntConfig : BaseConfig<int>
{

    public IntConfig(string name, int defaultValue): base(name, defaultValue)
    {
        
    }

    protected override int GetValue(int defautValue)
    {
        return PlayerPrefs.GetInt(_name, defautValue);
    }

    protected override int SaveValue(int value)
    {
        PlayerPrefs.SetInt(_name, value);
        return value;
    }

}