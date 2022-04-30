using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MenuManager
{

    public const string SYSTEMS_DATA = "SystemsData";
    public const string MENU = "Menu";
    public const string UI_MAIN = "UI_Main";
    public const string UI_OPTIONS = "UI_Options";
    public const string UI_CREDITS = "UI_Credits";

    public const string GAME = "Game";
    public const string UI_GAME = "UI_Game";
    public const string UI_PAUSE = "UI_Pause";

    private readonly Stack<string> _menuStack = new Stack<string>();
    public string CurrentMenu => _menuStack.Peek();

    private readonly List<string> _scenesOpen = new List<string>();

    public AsyncOperation LoadScene(string name, LoadSceneMode mode = LoadSceneMode.Single)
    {
        if (mode == LoadSceneMode.Single)
        {
            _menuStack.Clear();
            _scenesOpen.Clear();
        }

        _scenesOpen.Add(name);
        return SceneManager.LoadSceneAsync(name, mode);
    }

    public AsyncOperation UnloadScene(string name)
    {
        if (!_scenesOpen.Contains(name))
        {
            Debug.Log($"Scene {name} is not loaded.");
            return null;
        }

        _scenesOpen.Remove(name);
        return SceneManager.UnloadSceneAsync(name);
    }

    public AsyncOperation OpenMenu(string name, bool stackScenes = false)
    {
        var openOperation = LoadScene(name, LoadSceneMode.Additive);

        if (!stackScenes && _menuStack.Count > 0)
        {
            UnloadScene(CurrentMenu);
        }
        
        _menuStack.Push(name);
        return openOperation;
    }

    public AsyncOperation BackMenu()
    {
        var menu = _menuStack.Pop();

        var task = UnloadScene(menu);
        // Only load the scene if not loaded yet
        if (!_scenesOpen.Contains(CurrentMenu))
        {
            return LoadScene(CurrentMenu, LoadSceneMode.Additive);
        }
        
        return task;
    }

    public void ClearMenuUntil(string name, bool inclusive = false)
    {
        if (!_menuStack.Contains(name)) return;

        void PopCurrentScene()
        {
            var menu = _menuStack.Pop();
            if (_scenesOpen.Contains(menu))
            {
                UnloadScene(menu);
            }
        }

        for (int i = _menuStack.Count - 1; i >= 0 ; i--)
        {
            var menu = _menuStack.Peek();
            if (menu != name)
            {
                PopCurrentScene();
            }
            else
            {
                if (inclusive) PopCurrentScene();
                break;
            }
        }
    }

    public void SetActiveScene(string name)
    {
        var scene = SceneManager.GetSceneByName(name);
        SceneManager.SetActiveScene(scene);
    }

}
