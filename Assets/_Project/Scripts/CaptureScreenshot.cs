using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CaptureScreenshot : MonoBehaviour
{
    
#if UNITY_EDITOR

    private bool _screenshootActive = false;
    private const string DIRECTORY_NAME = "Images/Screenshots/";

    private InputManager _inputManager;
    
    [Inject]
    private void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    private void Awake()
    {
        if (!Directory.Exists(DIRECTORY_NAME))
        {
            Directory.CreateDirectory(DIRECTORY_NAME);
        }
    }

    private void OnEnable()
    {
        _inputManager.TakeScreenshot.OnDown += TakeScreenshot;
        _inputManager.ToogleScreenshot.OnDown += ToogleScreenshot;
    }

    private void OnDisable()
    {
        _inputManager.TakeScreenshot.OnDown -= TakeScreenshot;
        _inputManager.ToogleScreenshot.OnDown -= ToogleScreenshot;
    }

    private void ToogleScreenshot()
    {
        if (!_screenshootActive)
        {
            Debug.Log("Screenshot coroutine ACTIVED");
            StartCoroutine(TakeScreenshotCoroutine());
        }
        else
        {
            Debug.Log("Screenshot coroutine STOP");
            StopAllCoroutines();
        }
        _screenshootActive = !_screenshootActive;
    }

    private IEnumerator TakeScreenshotCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            TakeScreenshot();
        }
    }

    private void TakeScreenshot()
    {
        string filename = "SS_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        ScreenCapture.CaptureScreenshot(DIRECTORY_NAME + filename);
        Debug.Log("SS " + filename + " saved");
    }

#endif

}
