using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;
using NSubstitute;

[TestFixture]
public class DummyEntityTests : ZenjectIntegrationTestFixture
{

    private (
        IFloatingTextSystem,
        IAxisKey,
        IInputManager,
        ILocalizationSystem
    ) SetupDeps()
    {
        var floatingText = Substitute.For<IFloatingTextSystem>();
        Container.Bind<IFloatingTextSystem>().FromInstance(floatingText);

        var inputManager = Substitute.For<IInputManager>();
        var axisMovement = Substitute.For<IAxisKey>();
        inputManager.Movement.Returns(axisMovement);
        Container.Bind<IInputManager>().FromInstance(inputManager);

        var localizationSystem = Substitute.For<ILocalizationSystem>();
        Container.Bind<ILocalizationSystem>().FromInstance(localizationSystem);

        Container.Bind<IPhysicsSystem>().FromInstance(new PhysicsSystem());

        return (
            floatingText,
            axisMovement,
            inputManager,
            localizationSystem
        );
    }

    [UnityTest]
    public IEnumerator MoveRight()
    {
        var dummy = new GameObject("DummyTest");
        dummy.transform.position = Vector3.zero;
        var dummyEntity = dummy.AddComponent<DummyEntity>();

        PreInstall();

        (
            IFloatingTextSystem floatingText,
            IAxisKey axisMovement,
            IInputManager inputManager,
            ILocalizationSystem localizationSystem
        ) = SetupDeps();

        localizationSystem.GetTranslation("UI_RIGHT").Returns("RIGHT TEXT");
        
        axisMovement.Value.Returns(Vector2.zero);
        
        PostInstall();

        yield return new WaitForEndOfFrame();

        var initialPosition = dummy.transform.position;
        dummyEntity.MoveRight();
        
        yield return new WaitForSeconds(DummyEntity.MOVE_TIME);

        yield return new WaitForEndOfFrame();
        AssertEqualVector(initialPosition + Vector3.right, dummyEntity.transform.position);
        floatingText.Received().Create("RIGHT TEXT", initialPosition, Color.magenta);

        GameObject.Destroy(dummy);
    }

    [UnityTest]
    public IEnumerator MoveRight_WithInput()
    {
        var dummy = new GameObject("DummyTest");
        dummy.transform.position = Vector3.zero;
        var dummyEntity = dummy.AddComponent<DummyEntity>();

        PreInstall();

        (
            IFloatingTextSystem floatingText,
            IAxisKey axisMovement,
            IInputManager inputManager,
            ILocalizationSystem localizationSystem
        ) = SetupDeps();
        
        axisMovement.Value.Returns(Vector2.right);
        localizationSystem.GetTranslation("UI_RIGHT").Returns("RIGHT TEXT");

        PostInstall();

        yield return new WaitForEndOfFrame();

        var initialPosition = dummy.transform.position;

        yield return new WaitForSeconds(DummyEntity.MOVE_TIME);

        yield return new WaitForEndOfFrame();
        AssertEqualVector(initialPosition + Vector3.right, dummyEntity.transform.position);
        floatingText.Received().Create("RIGHT TEXT", initialPosition, Color.magenta);

        GameObject.Destroy(dummy);
    }

    [UnityTest]
    public IEnumerator MoveLeft()
    {
        var dummy = new GameObject("DummyTest");
        dummy.transform.position = Vector3.zero;
        var dummyEntity = dummy.AddComponent<DummyEntity>();

        PreInstall();

        (
            IFloatingTextSystem floatingText,
            IAxisKey axisMovement,
            IInputManager inputManager,
            ILocalizationSystem localizationSystem
        ) = SetupDeps();

        axisMovement.Value.Returns(Vector2.zero);

        localizationSystem.GetTranslation("UI_LEFT").Returns("LEFT TEXT");
        
        PostInstall();

        yield return new WaitForEndOfFrame();

        var initialPosition = dummy.transform.position;
        dummyEntity.MoveLeft();
        
        yield return new WaitForSeconds(DummyEntity.MOVE_TIME);

        yield return new WaitForEndOfFrame();
        AssertEqualVector(initialPosition + Vector3.left, dummyEntity.transform.position);
        floatingText.Received().Create("LEFT TEXT", initialPosition, Color.blue);

        GameObject.Destroy(dummy);
    }

    [UnityTest]
    public IEnumerator MoveLeft_WithInput()
    {
        var dummy = new GameObject("DummyTest");
        dummy.transform.position = Vector3.zero;
        var dummyEntity = dummy.AddComponent<DummyEntity>();

        PreInstall();

        (
            IFloatingTextSystem floatingText,
            IAxisKey axisMovement,
            IInputManager inputManager,
            ILocalizationSystem localizationSystem
        ) = SetupDeps();

        axisMovement.Value.Returns(Vector2.left);

        localizationSystem.GetTranslation("UI_LEFT").Returns("LEFT TEXT");
        
        PostInstall();

        yield return new WaitForEndOfFrame();

        var initialPosition = dummy.transform.position;

        yield return new WaitForSeconds(DummyEntity.MOVE_TIME);

        yield return new WaitForEndOfFrame();
        AssertEqualVector(initialPosition + Vector3.left, dummyEntity.transform.position);
        floatingText.Received().Create("LEFT TEXT", initialPosition, Color.blue);

        GameObject.Destroy(dummy);
    }

    [UnityTest]
    public IEnumerator MoveUp()
    {
        var dummy = new GameObject("DummyTest");
        dummy.transform.position = Vector3.zero;
        var dummyEntity = dummy.AddComponent<DummyEntity>();

        PreInstall();

        (
            IFloatingTextSystem floatingText,
            IAxisKey axisMovement,
            IInputManager inputManager,
            ILocalizationSystem localizationSystem
        ) = SetupDeps();

        axisMovement.Value.Returns(Vector2.zero);
        localizationSystem.GetTranslation("UI_UP").Returns("UP TEXT");
        
        PostInstall();

        yield return new WaitForEndOfFrame();

        var initialPosition = dummy.transform.position;
        dummyEntity.MoveUp();
        
        yield return new WaitForSeconds(DummyEntity.MOVE_TIME);

        yield return new WaitForEndOfFrame();
        AssertEqualVector(initialPosition + Vector3.up, dummyEntity.transform.position);
        floatingText.Received().Create("UP TEXT", initialPosition, Color.green);

        GameObject.Destroy(dummy);
    }

    [UnityTest]
    public IEnumerator MoveUp_WithInput()
    {
        var dummy = new GameObject("DummyTest");
        dummy.transform.position = Vector3.zero;
        var dummyEntity = dummy.AddComponent<DummyEntity>();

        PreInstall();

        (
            IFloatingTextSystem floatingText,
            IAxisKey axisMovement,
            IInputManager inputManager,
            ILocalizationSystem localizationSystem
        ) = SetupDeps();

        axisMovement.Value.Returns(Vector2.up);
        localizationSystem.GetTranslation("UI_UP").Returns("UP TEXT");
        
        PostInstall();

        yield return new WaitForEndOfFrame();

        var initialPosition = dummy.transform.position;

        yield return new WaitForSeconds(DummyEntity.MOVE_TIME);

        yield return new WaitForEndOfFrame();
        AssertEqualVector(initialPosition + Vector3.up, dummyEntity.transform.position);
        floatingText.Received().Create("UP TEXT", initialPosition, Color.green);

        GameObject.Destroy(dummy);
    }

    [UnityTest]
    public IEnumerator MoveDown()
    {
        var dummy = new GameObject("DummyTest");
        dummy.transform.position = Vector3.zero;
        var dummyEntity = dummy.AddComponent<DummyEntity>();

        PreInstall();

        (
            IFloatingTextSystem floatingText,
            IAxisKey axisMovement,
            IInputManager inputManager,
            ILocalizationSystem localizationSystem
        ) = SetupDeps();

        axisMovement.Value.Returns(Vector2.zero);

        localizationSystem.GetTranslation("UI_DOWN").Returns("DOWN TEXT");
        
        PostInstall();

        yield return new WaitForEndOfFrame();

        var initialPosition = dummy.transform.position;
        dummyEntity.MoveDown();
        
        yield return new WaitForSeconds(DummyEntity.MOVE_TIME);

        yield return new WaitForEndOfFrame();
        AssertEqualVector(initialPosition + Vector3.down, dummyEntity.transform.position);
        floatingText.Received().Create("DOWN TEXT", initialPosition, Color.yellow);

        GameObject.Destroy(dummy);
    }

    [UnityTest]
    public IEnumerator MoveDown_WithInput()
    {
        var dummy = new GameObject("DummyTest");
        dummy.transform.position = Vector3.zero;
        var dummyEntity = dummy.AddComponent<DummyEntity>();

        PreInstall();

        (
            IFloatingTextSystem floatingText,
            IAxisKey axisMovement,
            IInputManager inputManager,
            ILocalizationSystem localizationSystem
        ) = SetupDeps();
        
        axisMovement.Value.Returns(Vector2.down);
        localizationSystem.GetTranslation("UI_DOWN").Returns("DOWN TEXT");
        
        PostInstall();

        yield return new WaitForEndOfFrame();

        var initialPosition = dummy.transform.position;

        yield return new WaitForSeconds(DummyEntity.MOVE_TIME);

        yield return new WaitForEndOfFrame();
        AssertEqualVector(initialPosition + Vector3.down, dummyEntity.transform.position);
        floatingText.Received().Create("DOWN TEXT", initialPosition, Color.yellow);

        GameObject.Destroy(dummy);
    }

    [UnityTest]
    public IEnumerator MoveDown_AfterUp_WithInput()
    {
        var dummy = new GameObject("DummyTest");
        dummy.transform.position = Vector3.zero;
        var dummyEntity = dummy.AddComponent<DummyEntity>();

        PreInstall();

        (
            IFloatingTextSystem floatingText,
            IAxisKey axisMovement,
            IInputManager inputManager,
            ILocalizationSystem localizationSystem
        ) = SetupDeps();

        localizationSystem.GetTranslation("UI_DOWN").Returns("DOWN TEXT");
        localizationSystem.GetTranslation("UI_UP").Returns("UP TEXT");
        
        PostInstall();

        // Move Down
        axisMovement.Value.Returns(Vector2.down);
        yield return new WaitForEndOfFrame();

        var initialPosition = dummy.transform.position;

        yield return new WaitForSeconds(DummyEntity.MOVE_TIME);

        yield return new WaitForEndOfFrame();
        var downPosition = initialPosition + Vector3.down;
        AssertEqualVector(downPosition, dummyEntity.transform.position);
        floatingText.Received().Create("DOWN TEXT", initialPosition, Color.yellow);

        yield return new WaitForEndOfFrame();

        // Move Up
        var beforeUpPosition = dummy.transform.position;
        axisMovement.Value.Returns(Vector2.up);
        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(DummyEntity.MOVE_TIME / 2f);
        axisMovement.Value.Returns(Vector2.zero);
        
        yield return new WaitForSeconds(DummyEntity.MOVE_TIME / 2f);

        yield return new WaitForEndOfFrame();
        var finalPos = beforeUpPosition + Vector3.up;
        AssertEqualVector(finalPos, dummyEntity.transform.position);
        AssertEqualVector(initialPosition, dummyEntity.transform.position);
        floatingText.Received().Create("UP TEXT", beforeUpPosition, Color.green);
        

        GameObject.Destroy(dummy);
    }

    [UnityTest]
    public IEnumerator NotMoveRight_BecauseOfCollision()
    {
        var wall = new GameObject("Wall");
        wall.transform.position = Vector3.right;
        wall.AddComponent<BoxCollider2D>();
        wall.gameObject.layer = LayerMask.NameToLayer("Wall");

        var dummy = new GameObject("DummyTest");
        dummy.transform.position = Vector3.zero;
        var dummyEntity = dummy.AddComponent<DummyEntity>();

        PreInstall();

        (
            IFloatingTextSystem floatingText,
            IAxisKey axisMovement,
            IInputManager inputManager,
            ILocalizationSystem localizationSystem
        ) = SetupDeps();

        axisMovement.Value.Returns(Vector2.right);
        localizationSystem.GetTranslation("UI_RIGHT").Returns("RIGHT TEXT");
        
        PostInstall();

        yield return new WaitForEndOfFrame();

        var initialPosition = dummy.transform.position;

        yield return new WaitForSeconds(DummyEntity.MOVE_TIME);

        yield return new WaitForEndOfFrame();
        AssertEqualVector(initialPosition, dummyEntity.transform.position);
        floatingText.Received().Create("RIGHT TEXT", initialPosition, Color.magenta);

        GameObject.Destroy(dummy);
    }

    [UnityTest]
    public IEnumerator NotMoveRight_BecauseOfCollision_MoveLeftAfter()
    {
        var wall = new GameObject("Wall");
        wall.transform.position = Vector3.right;
        wall.AddComponent<BoxCollider2D>();
        wall.gameObject.layer = LayerMask.NameToLayer("Wall");

        var dummy = new GameObject("DummyTest");
        dummy.transform.position = Vector3.zero;
        var dummyEntity = dummy.AddComponent<DummyEntity>();

        PreInstall();

        (
            IFloatingTextSystem floatingText,
            IAxisKey axisMovement,
            IInputManager inputManager,
            ILocalizationSystem localizationSystem
        ) = SetupDeps();

        inputManager.Movement.Returns(axisMovement);
        localizationSystem.GetTranslation("UI_RIGHT").Returns("RIGHT TEXT");
        
        PostInstall();

        axisMovement.Value.Returns(Vector2.right);
        yield return new WaitForEndOfFrame();

        var initialPosition = dummy.transform.position;

        yield return new WaitForSeconds(DummyEntity.MOVE_TIME);

        yield return new WaitForEndOfFrame();
        AssertEqualVector(initialPosition, dummyEntity.transform.position);
        floatingText.Received().Create("RIGHT TEXT", initialPosition, Color.magenta);

        yield return new WaitForEndOfFrame();
        axisMovement.Value.Returns(Vector2.left);
        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(DummyEntity.MOVE_TIME);

        yield return new WaitForEndOfFrame();
        AssertEqualVector(initialPosition + Vector3.left, dummyEntity.transform.position);
        floatingText.Received().Create("RIGHT TEXT", initialPosition, Color.magenta);

        GameObject.Destroy(dummy);
    }

    private void AssertEqualVector(Vector3 expected, Vector3 actual, float delta = 0.01f)
    {
        Assert.AreEqual(expected.x, actual.x, delta, "Wrong X value");
        Assert.AreEqual(expected.y, actual.y, delta, "Wrong Y value");
        Assert.AreEqual(expected.z, actual.z, delta, "Wrong Z value");
    }

}
