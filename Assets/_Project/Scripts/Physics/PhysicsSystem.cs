using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSystem : IPhysicsSystem
{
    public bool IsPositionWalkable(Vector3 position)
    {
        var collider = Physics2D.OverlapCircle(position, 0.3f, LayerMask.GetMask("Wall"));
        return collider == null;
    }
}
