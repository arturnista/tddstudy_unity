using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

public class EntityMovementTests
{
    [Test]
    public void EntityShouldStartAbleToMove()
    {
        var physicsSystem = Substitute.For<IPhysicsSystem>();
        EntityMovement movement = new EntityMovement(physicsSystem);
        Assert.IsTrue(movement.CanMove);
    }

    [Test]
    public void MoveDirectionRight_ShouldReturnRight()
    {
        var physicsSystem = Substitute.For<IPhysicsSystem>();
        physicsSystem.IsPositionWalkable(Vector3.right).Returns(true);

        EntityMovement movement = new EntityMovement(physicsSystem);

        var result = movement.MoveDirection(Vector3.zero, Vector3.right);

        AssertEqualVector(Vector3.right, result);
    }

    [Test]
    public void MoveDirectionRight_WithCollision_ShouldReturnZero()
    {
        var physicsSystem = Substitute.For<IPhysicsSystem>();
        physicsSystem.IsPositionWalkable(Vector3.right).Returns(false);

        EntityMovement movement = new EntityMovement(physicsSystem);

        var result = movement.MoveDirection(Vector3.zero, Vector3.right);

        AssertEqualVector(Vector3.zero, result);
    }

    private void AssertEqualVector(Vector3 expected, Vector3 actual, float delta = 0.01f)
    {
        Assert.AreEqual(expected.x, actual.x, delta, "Wrong X value");
        Assert.AreEqual(expected.y, actual.y, delta, "Wrong Y value");
        Assert.AreEqual(expected.z, actual.z, delta, "Wrong Z value");
    }

}
