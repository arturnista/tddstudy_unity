using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class DummyEntity : MonoBehaviour
{

    public const float MOVE_TIME = 0.2f;

    private IFloatingTextSystem _floatingTextSystem;
    private ILocalizationSystem _localizationSystem;
    private IInputManager _inputManager;
    private IPhysicsSystem _physicsSystem;
    private EntityMovement _entityMovement;

    private Vector2 _lastMotion;

    private bool _isMoving;

    [Inject]
    public void Construct(IFloatingTextSystem floatingTextSystem, IInputManager inputManager, ILocalizationSystem localizationSystem, IPhysicsSystem physicsSystem)
    {
        _floatingTextSystem = floatingTextSystem;
        _localizationSystem = localizationSystem;
        _inputManager = inputManager;
        _physicsSystem = physicsSystem;

        _entityMovement = new EntityMovement(_physicsSystem);
    }
    
    private void Update()
    {
        if (_inputManager == null || _inputManager.Movement == null) return;

        var motion = _inputManager.Movement.Value;
        if (!_entityMovement.CanMove) return;
        if (_lastMotion != motion)
        {
            if (motion.y > 0f) motion.y = 1f;
            else if (motion.y < 0f) motion.y = -1f;
            if (motion.x > 0f) motion.x = 1f;
            else if (motion.x < 0f) motion.x = -1f;

            if (_lastMotion.x != motion.x)
            {
                if (motion.x > 0f) MoveRight();
                else if (motion.x < 0f) MoveLeft();
            }
            else if (_lastMotion.y != motion.y)
            {
                if (motion.y > 0f) MoveUp();
                else if (motion.y < 0f) MoveDown();
            }
        }

        _lastMotion = motion;
    }

    public void MoveRight()
    {
        if (!_entityMovement.CanMove)
        {
            return;
        }

        _floatingTextSystem.Create(_localizationSystem.GetTranslation("UI_RIGHT"), transform.position, Color.magenta);
        var finalPosition = _entityMovement.MoveDirection(transform.position, Vector3.right);
        StartCoroutine(MoveCoroutine(transform.position, finalPosition, MOVE_TIME));
    }

    public void MoveLeft()
    {
        if (!_entityMovement.CanMove)
        {
            return;
        }

        _floatingTextSystem.Create(_localizationSystem.GetTranslation("UI_LEFT"), transform.position, Color.blue);
        var finalPosition = _entityMovement.MoveDirection(transform.position, Vector3.left);
        StartCoroutine(MoveCoroutine(transform.position, finalPosition, MOVE_TIME));
    }

    public void MoveUp()
    {
        if (!_entityMovement.CanMove)
        {
            return;
        }

        _floatingTextSystem.Create(_localizationSystem.GetTranslation("UI_UP"), transform.position, Color.green);
        var finalPosition = _entityMovement.MoveDirection(transform.position, Vector3.up);
        StartCoroutine(MoveCoroutine(transform.position, finalPosition, MOVE_TIME));
    }

    public void MoveDown()
    {
        if (!_entityMovement.CanMove)
        {
            return;
        }

        _floatingTextSystem.Create(_localizationSystem.GetTranslation("UI_DOWN"), transform.position, Color.yellow);
        var finalPosition = _entityMovement.MoveDirection(transform.position, Vector3.down);
        StartCoroutine(MoveCoroutine(transform.position, finalPosition, MOVE_TIME));
    }

    private IEnumerator MoveCoroutine(Vector3 from, Vector3 to, float time)
    {
        _entityMovement.StartMoving();
        transform.position = from;

        float rate = 1f / time;
        float value = 0f;
        while (value <= 1f)
        {
            value += rate * Time.deltaTime;
            float easeValue = LeanTween.easeOutBack(0f, 1f, value);
            transform.position = Vector3.LerpUnclamped(from, to, easeValue);
            yield return null;
        }
        transform.position = to;

        _entityMovement.StopMoving();
    }

    public class Factory : PlaceholderFactory<DummyEntity>
    {
        
    }
}