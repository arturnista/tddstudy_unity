using UnityEngine;
using Zenject;
using UnityEngine.Audio;

public class ProjectContextInstaller : MonoInstaller
{
    
    [SerializeField] private AudioMixer _audioMixer;
    [Space]
    [SerializeField] private AudioMixerGroup _sfxMixerGroup = default;
    [SerializeField] private AudioMixerGroup _musicMixerGroup = default;
    
    public override void InstallBindings()
    {
        Container.Bind<AudioMixer>().FromInstance(_audioMixer).AsSingle();
        Container.Bind<AudioMixerGroup>().WithId(SoundEffectsSystem.SFX_MIXER).FromInstance(_sfxMixerGroup);
        Container.Bind<AudioMixerGroup>().WithId(MusicSystem.MUSIC_MIXER).FromInstance(_musicMixerGroup);
        Container.Bind<ILocalizationSystem>().To<LocalizationSystem>().FromNew().AsSingle();
        
        Container.Bind<IInputManager>().To<InputManager>().FromNew().AsSingle().NonLazy();
        Container.Bind<ConfigurationManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        
        Container.Bind<MenuManager>().FromNew().AsSingle();
        Container.Bind<SoundEffectsSystem>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<MusicSystem>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }
}