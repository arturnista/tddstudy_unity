using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement
{
    private IPhysicsSystem _physicsSystem;

    public bool CanMove { get; private set; }

    public EntityMovement(IPhysicsSystem physicsSystem)
    {
        _physicsSystem = physicsSystem;
        CanMove = true;
    }

    public Vector3 MoveDirection(Vector3 position, Vector3 direction)
    {
        var initialPosition = position;
        var finalPosition = initialPosition + direction;

        if (!_physicsSystem.IsPositionWalkable(finalPosition))
        {
            return initialPosition;
        }

        return finalPosition;
    }

    public void StartMoving()
    {
        CanMove = false;
    }

    public void StopMoving()
    {
        CanMove = true;
    }

}
