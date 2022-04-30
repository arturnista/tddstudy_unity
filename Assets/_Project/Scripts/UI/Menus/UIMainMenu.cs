using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace UI.Menus
{
        
    public class UIMainMenu : MonoBehaviour
    {
        
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _optionsGameButton;
        [SerializeField] private Button _creditsGameButton;
        [SerializeField] private Button _quitGameButton;
        
        private MenuManager _menuManager;
        
        [Inject]
        private void Construct(MenuManager menuManager)
        {
            _menuManager = menuManager;
        }

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(HandleStartButtonClick);
            _optionsGameButton.onClick.AddListener(HandleOptionsButtonClick);
            _creditsGameButton.onClick.AddListener(HandleCreditsButtonClick);
    #if UNITY_WEBGL
            Destroy(_quitGameButton.gameObject);
    #else
            _quitGameButton.onClick.AddListener(HandleQuitButtonClick);
    #endif
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(HandleStartButtonClick);
            _creditsGameButton.onClick.RemoveListener(HandleCreditsButtonClick);
            _optionsGameButton.onClick.RemoveListener(HandleOptionsButtonClick);
    #if !UNITY_WEBGL
            _quitGameButton.onClick.RemoveListener(HandleQuitButtonClick);
    #endif
        }

        private void Start()
        {
            EventSystem.current.SetSelectedGameObject(_startGameButton.gameObject);
        }

        private void HandleStartButtonClick()
        {
            _menuManager.LoadScene(MenuManager.GAME);
        }

        private void HandleOptionsButtonClick()
        {
            _menuManager.OpenMenu(MenuManager.UI_OPTIONS);
        }

        private void HandleCreditsButtonClick()
        {
            _menuManager.OpenMenu(MenuManager.UI_CREDITS);
        }

        private void HandleQuitButtonClick()
        {
            Application.Quit();
        }

    }

}