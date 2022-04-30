using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

[System.Serializable]
public class AudioItem
{

    [SerializeField] private List<AudioClip> _clips = default;
    public bool HasClips => _clips.Count > 0;
    public AudioClip Clip => _clips[Random.Range(0, _clips.Count)];

    [SerializeField, Range(0f, 1f)] private float _volume = .3f;
    public float Volume => _volume;

}

public class SoundEffectsSystem : MonoBehaviour
{

    public const string SFX_MIXER = "SfxMixer";

    [Inject(Id = SFX_MIXER)] private AudioMixerGroup _sfxMixerGroup;

    private Dictionary<string, AudioItem> _sfxLibrary;

    private const int SfxAmount = 10;
    private const int WorldSfxAmount = 0;

    private List<AudioSource> _sfxSources;
    private List<AudioSource> _worldSfxSources;

    private int _sfxIndex;
    private int _worldSfxIndex;

    private void Awake()
    {        
        _sfxLibrary = new Dictionary<string, AudioItem>();
        
        _sfxIndex = 0;
        _sfxSources = new List<AudioSource>();
        for (int i = 0; i < SfxAmount; i++)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = _sfxMixerGroup;
            _sfxSources.Add(source);
        }

        _worldSfxIndex = 0;
        _worldSfxSources = new List<AudioSource>();
        for (int i = 0; i < WorldSfxAmount; i++)
        {
            var sourceGameObject = new GameObject("AudioSourcePositional");
            sourceGameObject.transform.parent = transform;

            var source = sourceGameObject.AddComponent<AudioSource>();
            source.spatialBlend = .9f;
            source.outputAudioMixerGroup = _sfxMixerGroup;
            _worldSfxSources.Add(source);
        }
    }

    public void RegisterLibrary(GenericDictionary<string, AudioItem> library)
    {
        foreach (var item in library)
        {
            _sfxLibrary.Add(item.Key, item.Value);
        }
    }

    public void PlaySFX(string type, float delay = 0f)
    {
        if (!_sfxLibrary.ContainsKey(type))
        {
            Debug.LogWarning($"SFX type <b>{type}</b> not found!");
            return;
        }

        var sourceObject = _sfxSources[_sfxIndex];
        var item = _sfxLibrary[type];
        if (!item.HasClips) return;

        if (delay > 0f)
        {
            StartCoroutine(PlaySFXDelayed(sourceObject, item, delay));
        }
        else
        {
            sourceObject.PlayOneShot(item.Clip, item.Volume);
        }

        _sfxIndex += 1;
        if (_sfxSources.Count > _sfxIndex) _sfxIndex = 0;
    }

    public void PlaySFX(string type, Vector3 position)
    {
        if (!_sfxLibrary.ContainsKey(type))
        {
            Debug.LogWarning($"SFX type <b>{type}</b> not found!");
            return;
        }
        
        var item = _sfxLibrary[type];
        if (!item.HasClips) return;

        var sourceObject = _worldSfxSources[_worldSfxIndex];
        if (sourceObject.isPlaying) sourceObject.Stop();
        sourceObject.transform.position = position;

        sourceObject.PlayOneShot(item.Clip, item.Volume);

        _worldSfxIndex += 1;
        if (_worldSfxSources.Count > _worldSfxIndex) _worldSfxIndex = 0;
    }

    private IEnumerator PlaySFXDelayed(AudioSource source, AudioItem item, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.PlayOneShot(item.Clip, item.Volume);
    }

}
