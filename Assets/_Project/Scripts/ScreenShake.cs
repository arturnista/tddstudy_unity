using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour, IScreenShake
{

    private bool _enabled = true;
    private Transform _target;

    private void Awake()
    {
        _target = Camera.main.transform.parent;
    }

    public void Shake(float strength, float time)
    {
        if (!_enabled) return;
        StartCoroutine(ScreenShakeCoroutine(strength, time));
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Disable()
    {
        _enabled = false;
        Stop();
    }
    
    private IEnumerator ScreenShakeCoroutine(float strength, float time)
    {
        float currentTime = 0f;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            _target.localPosition = Random.insideUnitSphere * strength;
            yield return null;
        }

        _target.localPosition = Vector3.zero;
    }

}
