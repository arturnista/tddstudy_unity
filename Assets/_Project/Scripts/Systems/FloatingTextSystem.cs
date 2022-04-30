using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class FloatingTextSystem : MonoBehaviour, IFloatingTextSystem
{

    public const string FLOATING_TEXT_PREFAB_NAME = "FloatingTextPrefab";
    [Inject(Id = FLOATING_TEXT_PREFAB_NAME)] private GameObject _prefab;

    public void Create(string message, Vector3 position, float time = 1f)
    {
        Create(message, position, Color.white, time);
    }

    public void Create(string message, Vector3 position, Color color, float time = 1f, float size = 7f)
    {
        StartCoroutine(DisplayTextCoroutine(message, position, color, time, size));
    }

    private IEnumerator DisplayTextCoroutine(string message, Vector3 position, Color color, float time = 1f, float size = 7f)
    {
        var created = Instantiate(_prefab, position, Quaternion.identity);
        var textMesh = created.GetComponentInChildren<TextMeshPro>();
        textMesh.text = message;
        textMesh.color = color;
        textMesh.fontSize = size;
        LeanTween.value(created, 0f, 1f, time)
        .setOnUpdate(value => {
            created.transform.position = Vector3.Lerp(position, position + Vector3.up, value);
        })
        .setEaseOutQuad();
        yield return new WaitForSeconds(time);
        Destroy(created.gameObject);
    }

}
