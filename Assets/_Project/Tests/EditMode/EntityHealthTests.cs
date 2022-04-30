using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

public interface IDamageCalculator
{
    int CalculateDamage(int dmg);
}

public class EntityHealth
{

    private int _health;
    public int Health => _health;

    public EntityHealth(int health)
    {
        _health = health;
    }

    public void DealDamage(int dmg, IDamageCalculator calculator)
    {
        _health -= calculator.CalculateDamage(dmg);
    }
}

public class EntityHealthTests
{
    [Test]
    public void ShouldTake10Damage()
    {
        
        var damageCalculator = Substitute.For<IDamageCalculator>();
        damageCalculator.CalculateDamage(10).Returns(10);

        EntityHealth health = new EntityHealth(100);
        health.DealDamage(10, damageCalculator);

        Assert.AreEqual(90, health.Health);

    }

}
