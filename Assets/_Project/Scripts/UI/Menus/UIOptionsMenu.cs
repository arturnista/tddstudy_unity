using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UI.Elements;
using Zenject;

namespace UI.Menus
{
    
    public class UIOptionsMenu : MonoBehaviour
    {

        [SerializeField] private UIOptions _masterVolumeOptions;
        [SerializeField] private UIOptions _sfxVolumeOptions;
        [SerializeField] private UIOptions _musicVolumeOptions;
        [Space]
        [SerializeField] private Button _backButton;
        
        private MenuManager _menuManager;
        private ConfigurationManager _configurationManager;
        
        [Inject]
        private void Construct(MenuManager menuManager, ConfigurationManager configurationManager)
        {
            _menuManager = menuManager;
            _configurationManager = configurationManager;
            ConstructOptions();
        }

        private void Start()
        {
            EventSystem.current.SetSelectedGameObject(_masterVolumeOptions.gameObject);
        }

        private void ConstructOptions()
        {
            _masterVolumeOptions.Construct(
                "Master Volume",
                _configurationManager.MasterVolume.Value,
                new List<string>() { "MUTE", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }
            );
            _sfxVolumeOptions.Construct(
                "Sfx Volume",
                _configurationManager.SfxVolume.Value,
                new List<string>() { "MUTE", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }
            );
            _musicVolumeOptions.Construct(
                "Music Volume",
                _configurationManager.MusicVolume.Value,
                new List<string>() { "MUTE", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }
            );
        }

        private void OnEnable()
        {
            _masterVolumeOptions.OnChangeValue += HandleChangeMasterVolume;
            _sfxVolumeOptions.OnChangeValue += HandleChangeSfxVolume;
            _musicVolumeOptions.OnChangeValue += HandleChangeMusicVolume;

            _backButton.onClick.AddListener(HandleBackButtonClick);
        }

        private void OnDisable()
        {
            _masterVolumeOptions.OnChangeValue -= HandleChangeMasterVolume;
            _sfxVolumeOptions.OnChangeValue -= HandleChangeSfxVolume;
            _musicVolumeOptions.OnChangeValue -= HandleChangeMusicVolume;

            _backButton.onClick.RemoveListener(HandleBackButtonClick);
        }

        private void HandleChangeMasterVolume(int index, string value)
        {
            _configurationManager.MasterVolume.Value = index;
        }

        private void HandleChangeSfxVolume(int index, string value)
        {
            _configurationManager.SfxVolume.Value = index;
        }

        private void HandleChangeMusicVolume(int index, string value)
        {
            _configurationManager.MusicVolume.Value = index;
        }

        private void HandleBackButtonClick()
        {
            _menuManager.BackMenu();
        }

        private void OnValidate()
        {
            // ConstructOptions();
        }
        
    }
    
}
