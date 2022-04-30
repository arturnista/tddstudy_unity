using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

[System.Serializable]
public class MusicItem
{

    [SerializeField] private AudioClip _clip = default;
    public AudioClip Clip => _clip;

    [SerializeField, Range(0f, 1f)] private float _volume = .1f;
    public float Volume => _volume;

}

public class MusicSystem : MonoBehaviour
{
    public const string MUSIC_MIXER = "MusicMixer";

    [Inject(Id = MUSIC_MIXER)] private AudioMixerGroup _musicMixerGroup = default;
    
    private Dictionary<string, MusicItem> _musicLibrary = default;

    private const int MusicAmount = 2;

    private List<AudioSource> _musicSources;

    private int _sourceIndex;

    private void Awake()
    {
        _musicLibrary = new Dictionary<string, MusicItem>();

        _sourceIndex = 0;
        _musicSources = new List<AudioSource>();
        for (int i = 0; i < MusicAmount; i++)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = _musicMixerGroup;
            source.playOnAwake = false;
            source.loop = true;
            source.volume = 0f;
            _musicSources.Add(source);
        }
    }

    public void RegisterLibrary(GenericDictionary<string, MusicItem> library)
    {
        foreach (var item in library)
        {
            _musicLibrary.Add(item.Key, item.Value);
        }
    }

    public void PlayMusic(string type)
    {
        if (!_musicLibrary.ContainsKey(type))
        {
            Debug.LogWarning($"Music type {type} not found!");
            return;
        }
        
        Debug.Log($"Changing music to {type}");

        var lastSource = _musicSources[_sourceIndex];
        StartCoroutine(FadeSound(lastSource, 0f, 1f, () => {
            lastSource.Stop();
        }));

        _sourceIndex = (_sourceIndex + 1) % _musicSources.Count;
        
        var music = _musicLibrary[type];
        _musicSources[_sourceIndex].clip = music.Clip;

        var currentSource = _musicSources[_sourceIndex];
        currentSource.Play();
        StartCoroutine(FadeSound(_musicSources[_sourceIndex], music.Volume, 1f));
    }

    private IEnumerator FadeSound(AudioSource source, float to, float timeSeconds, Action onFinish = null)
    {
        float rate = 1f / timeSeconds;
        float volume = source.volume;
        
        while (!Mathf.Approximately(volume, to))
        {
            volume = Mathf.MoveTowards(volume, to, rate * Time.unscaledDeltaTime);
            source.volume = volume;
            yield return null;
        }
        onFinish?.Invoke();
    }

}
