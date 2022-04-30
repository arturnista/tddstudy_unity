using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using NSubstitute;

[TestFixture]
public class DummyGemInteractionTests : ZenjectIntegrationTestFixture
{

    [UnityTest]
    public IEnumerator DummyMoveToGem_ShouldIncreaseScore()
    {
        var gem = new GameObject("Gem");
        gem.transform.position = Vector3.right;
        var boxCollider = gem.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        var gemView = gem.AddComponent<GemView>();

        var dummy = new GameObject("DummyTest");
        dummy.transform.position = Vector3.zero;
        var dummyCollider = dummy.AddComponent<BoxCollider2D>();
        var dummyEntity = dummy.AddComponent<DummyEntity>();
        var rigidbody2D = dummy.AddComponent<Rigidbody2D>();
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

        PreInstall();

        var scoreSystem = new ScoreSystem();
        var floatingText = Substitute.For<IFloatingTextSystem>();
        
        var inputManager = Substitute.For<IInputManager>();
        var axisMovement = Substitute.For<IAxisKey>();
        axisMovement.Value.Returns(Vector2.right);
        inputManager.Movement.Returns(axisMovement);

        Container.Bind<IFloatingTextSystem>().FromInstance(floatingText);
        Container.Bind<IInputManager>().FromInstance(inputManager);
        Container.Bind<IScoreSystem>().FromInstance(scoreSystem);
        Container.Bind<IPhysicsSystem>().FromInstance(new PhysicsSystem());

        PostInstall();

        yield return new WaitForEndOfFrame();

        var initialPosition = dummy.transform.position;
        var finalPosition = initialPosition + Vector3.right;

        yield return new WaitForSeconds(DummyEntity.MOVE_TIME * 2f);

        yield return new WaitForEndOfFrame();
        AssertEqualVector(finalPosition, dummyEntity.transform.position);
        
        Assert.AreEqual(1, scoreSystem.Score);
        Assert.IsFalse(gemView.IsActive);

        yield return new WaitForSeconds(GemView.RESPAWN_TIME + 1f);
        yield return new WaitForEndOfFrame();

        Assert.IsTrue(gemView.IsActive);

        GameObject.Destroy(dummy);
    }

    private void AssertEqualVector(Vector3 expected, Vector3 actual, float delta = 0.01f)
    {
        Assert.AreEqual(expected.x, actual.x, delta, "Wrong X value");
        Assert.AreEqual(expected.y, actual.y, delta, "Wrong Y value");
        Assert.AreEqual(expected.z, actual.z, delta, "Wrong Z value");
    }

}
