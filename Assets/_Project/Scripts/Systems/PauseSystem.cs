using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PauseSystem : IDisposable
{
    
    public enum PauseReason
    {
        Player,
        Menu,
        EndGame,
        Force
    }

    private PauseReason _pauseReason;

    private IInputManager _inputManager;
    private MenuManager _menuManager;
    private IScreenShake _screenShake;

    public bool IsPaused { get; private set; }
    
    [Inject]
    private void Construct(IInputManager inputManager, MenuManager menuManager, IScreenShake screenShake)
    {
        _inputManager = inputManager;
        _menuManager = menuManager;
        _screenShake = screenShake;
    }

    public PauseSystem()
    {
    }

    public void Dispose()
    {
        Resume(PauseReason.Force);
    }

    public void Pause(PauseReason reason)
    {
        Debug.Log($"Game <b>PAUSED</b> for {reason}");
        Application.targetFrameRate = 60;
        _pauseReason = reason;
        IsPaused = true;
        Time.timeScale = 0f;

        _screenShake.Stop();
        _inputManager.EnableUI();

        EventManager.Instance.DispatchEvent(EventManager.EventType.GamePause);
        if (reason == PauseReason.Player)
        {
            _menuManager.OpenMenu(MenuManager.UI_PAUSE, true);
        }
    }

    public void Resume(PauseReason reason)
    {
        if (_pauseReason != reason && reason != PauseReason.Force) return;
        Application.targetFrameRate = -1;
        Debug.Log($"Game <b>RESUMED</b> for {reason}");

        IsPaused = false;
        Time.timeScale = 1f;
        _inputManager.EnableGameplay();

        EventManager.Instance.DispatchEvent(EventManager.EventType.GameResume);
        if (reason == PauseReason.Player)
        {
            _menuManager.ClearMenuUntil(MenuManager.UI_PAUSE, true);
        }
    }
}
