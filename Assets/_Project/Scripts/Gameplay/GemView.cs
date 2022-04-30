using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GemView : MonoBehaviour
{

    public const float RESPAWN_TIME = 5f;
    
    [SerializeField] private int _scoreAmount = 1;
    [SerializeField] private GameObject _visual;

    public bool IsActive => _gemCollect.IsActive;

    private BoxCollider2D _collider;

    private GemCollect _gemCollect;
    private IScoreSystem _scoreSystem;
    private IFloatingTextSystem _floatingTextSystem;

    [Inject]
    private void Construct(IScoreSystem scoreSystem, IFloatingTextSystem floatingTextSystem)
    {
        _scoreSystem = scoreSystem;
        _floatingTextSystem = floatingTextSystem;
        _gemCollect = new GemCollect(_scoreAmount, _scoreSystem);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        bool wasCollected = _gemCollect.OnCollect();
        if (!wasCollected) return;
        
        _floatingTextSystem.Create($"+{_gemCollect.ScoreAmount}", transform.position, Color.yellow);

        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        _visual?.SetActive(false);

        yield return new WaitForSeconds(RESPAWN_TIME);

        _visual?.SetActive(true);
        _gemCollect.OnRespawn();
    }
    
    public class Factory : PlaceholderFactory<GemView>
    {
        
    }

}
