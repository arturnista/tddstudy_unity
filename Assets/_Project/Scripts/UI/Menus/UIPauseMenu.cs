using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace UI.Menus
{
    
    public class UIPauseMenu : MonoBehaviour
    {
        
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _menuButton;

        private PauseSystem _pauseSystem;
        private MenuManager _menuManager;
        
        [Inject]
        private void Construct(PauseSystem pauseSystem, MenuManager menuManager)
        {
            _pauseSystem = pauseSystem;
            _menuManager = menuManager;
        }

        private void OnEnable()
        {
            _resumeButton.onClick.AddListener(HandleResumeClick);
            _optionsButton.onClick.AddListener(HandleOptionsClick);
            _menuButton.onClick.AddListener(HandleMenuClick);
        }

        private void Start()
        {
            EventSystem.current.SetSelectedGameObject(_resumeButton.gameObject);
        }

        private void OnDisable()
        {
            _resumeButton.onClick.RemoveListener(HandleResumeClick);
            _optionsButton.onClick.RemoveListener(HandleOptionsClick);
            _menuButton.onClick.RemoveListener(HandleMenuClick);
        }

        private void HandleResumeClick()
        {
            _pauseSystem.Resume(PauseSystem.PauseReason.Player);
        }

        private void HandleOptionsClick()
        {
            _menuManager.OpenMenu(MenuManager.UI_OPTIONS);
        }

        private void HandleMenuClick()
        {
            _menuManager.LoadScene(MenuManager.MENU);
            _pauseSystem.Resume(PauseSystem.PauseReason.Player);
        }

    }
    
}
