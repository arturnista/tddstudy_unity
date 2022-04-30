using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class ConfigurationManager : MonoBehaviour
{

    private AudioMixer _audioMixer;

    public IntConfig MasterVolume;
    public IntConfig SfxVolume;
    public IntConfig MusicVolume;

    [Inject]
    private void Construct(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
    }
    
    private void Awake()
    {
        MasterVolume = new IntConfig("MasterVolume", 5);
        MasterVolume.AddParser(NormalizeVolumeParser);

        SfxVolume = new IntConfig("SfxVolume", 10);
        SfxVolume.AddParser(NormalizeVolumeParser);

        MusicVolume = new IntConfig("MusicVolume", 10);
        MusicVolume.AddParser(NormalizeVolumeParser);
    }
    
    private IEnumerator Start()
    {
        MasterVolume.OnUpdate += (value => _audioMixer.SetFloat("MasterVolume", MasterVolume.Parse()));
        SfxVolume.OnUpdate += (value => _audioMixer.SetFloat("SfxVolume", SfxVolume.Parse()));
        MusicVolume.OnUpdate += (value => _audioMixer.SetFloat("MusicVolume", MusicVolume.Parse()));
        
        yield return new WaitForEndOfFrame();
        
        _audioMixer.SetFloat("MasterVolume", MasterVolume.Parse());
        _audioMixer.SetFloat("SfxVolume", SfxVolume.Parse());
        _audioMixer.SetFloat("MusicVolume", MusicVolume.Parse());
    }

    private float NormalizeVolumeParser(int volume)
    {
        float normalized = (volume / 10f);
        var valueClamp = Mathf.Clamp(normalized, 0.0001f, 1f);
        return Mathf.Log10(valueClamp) * 20f;
    }

}