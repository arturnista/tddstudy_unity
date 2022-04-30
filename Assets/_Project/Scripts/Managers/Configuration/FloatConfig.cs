using UnityEngine;

public class FloatConfig : BaseConfig<float>
{

    public FloatConfig(string name, float defaultValue): base(name, defaultValue)
    {
        
    }

    protected override float GetValue(float defautValue)
    {
        return PlayerPrefs.GetFloat(_name, defautValue);
    }

    protected override float SaveValue(float value)
    {
        PlayerPrefs.SetFloat(_name, value);
        return value;
    }

}