using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class InputKey
{

    public delegate void DownHandler();
    public event DownHandler OnDown;

    public delegate void UpHandler();
    public event UpHandler OnUp;

    private InputAction _action;

    public void SetInput(InputAction action)
    {
        _action = action;

        _action.performed += (ctx) => OnDown?.Invoke();
        _action.canceled += (ctx) => OnUp?.Invoke();
    }

}

public interface IAxisKey
{
    Vector2 Value { get; }
    void SetInput(InputAction action);
}

public class AxisKey : IAxisKey
{
    public delegate void UpdateHandler(Vector2 value);
    public event UpdateHandler OnUpdate;

    private InputAction _action;

    public Vector2 Value => _action.ReadValue<Vector2>();

    public void SetInput(InputAction action)
    {
        _action = action;
        
        _action.performed += (ctx) => {
            OnUpdate?.Invoke(Value);
        };
        _action.canceled += (ctx) => {
            OnUpdate?.Invoke(Vector2.zero);
        };
    }

}

public class InputManager : IInputManager
{

    public IAxisKey Movement { get; private set; } = new AxisKey();
    public InputKey Pause { get; private set; } = new InputKey();

#if UNITY_EDITOR

    public InputKey TakeScreenshot { get; private set; } = new InputKey();
    public InputKey ToogleScreenshot { get; private set; } = new InputKey();

#endif
    
    private GameControls _gameControls;

    public InputManager()
    {
        _gameControls = new GameControls();

        _gameControls.Global.Enable();
        Pause.SetInput(_gameControls.Global.Pause);
        
        _gameControls.Gameplay.Enable();
        Movement.SetInput(_gameControls.Gameplay.Movement);

#if UNITY_EDITOR

        _gameControls.Cheats.Enable();
        TakeScreenshot.SetInput(_gameControls.Cheats.TakeScreenshot);
        ToogleScreenshot.SetInput(_gameControls.Cheats.ToogleScreenshot);

#endif
        // _gameControls.Gameplay.Attack.performed
    }

    public void EnableUI()
    {
        _gameControls.Gameplay.Disable();
        _gameControls.UI.Enable();
    }

    public void EnableGameplay()
    {
        _gameControls.Gameplay.Enable();
        _gameControls.UI.Disable();
    }

}
