using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

public class ScoreSystemTests
{
    [Test]
    public void AddScore_ShouldIncreaseScore()
    {
        var scoreSystem = new ScoreSystem();
        scoreSystem.AddScore(5);

        Assert.AreEqual(5, scoreSystem.Score);
    }

}
