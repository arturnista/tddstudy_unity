using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameBoot : MonoBehaviour
{
    
    private MenuManager _menuManager;
    private IInputManager _inputManager;
    private PauseSystem _pauseSystem;
    private MusicSystem _musicSystem;

    [Inject]
    private void Construct(MenuManager menuManager, IInputManager inputManager, PauseSystem pauseSystem, MusicSystem musicSystem)
    {
        _menuManager = menuManager;
        _inputManager = inputManager;
        _pauseSystem = pauseSystem;
        _musicSystem = musicSystem;
    }
    
    private void Start()
    {
        _menuManager.OpenMenu(MenuManager.UI_GAME);
        _musicSystem.PlayMusic("music_game");
    }

    private void OnEnable()
    {
        _inputManager.Pause.OnDown += HandlePauseDown;
    }

    private void OnDisable()
    {
        _inputManager.Pause.OnDown -= HandlePauseDown;
    }

    private void HandlePauseDown()
    {
        if (_pauseSystem.IsPaused)
        {
            _pauseSystem.Resume(PauseSystem.PauseReason.Player);
        }
        else
        {
            _pauseSystem.Pause(PauseSystem.PauseReason.Player);
        }
    }
}
