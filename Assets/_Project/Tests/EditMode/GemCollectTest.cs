using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

public class GemCollectTest
{
    [Test]
    public void OnCollect_ShouldIncreaseScore()
    {
        var scoreSystem = Substitute.For<IScoreSystem>();

        int scoreAmount = 1;
        var gemCollect = new GemCollect(scoreAmount, scoreSystem);

        gemCollect.OnCollect();

        scoreSystem.Received().AddScore(1);
    }

    [Test]
    public void OnCollect_ShouldIncreaseScore_By5()
    {
        var scoreSystem = Substitute.For<IScoreSystem>();

        int scoreAmount = 5;
        var gemCollect = new GemCollect(scoreAmount, scoreSystem);

        gemCollect.OnCollect();

        scoreSystem.Received().AddScore(5);
    }

    [Test]
    public void OnCollect_ShouldNotCollectTwice()
    {
        var scoreSystem = Substitute.For<IScoreSystem>();

        int scoreAmount = 1;
        var gemCollect = new GemCollect(scoreAmount, scoreSystem);

        bool result = gemCollect.OnCollect();
        Assert.IsTrue(result);

        bool secondResult = gemCollect.OnCollect();
        Assert.IsFalse(secondResult);

        scoreSystem.Received().AddScore(1);
    }
}
