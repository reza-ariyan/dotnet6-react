using System.Linq;
using App.Controllers;
using NUnit.Framework;

namespace APP.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Get_Technologies_Has_Data()
    {
        // Arrange
        const bool expected = true;
        var technologies = new TechnologiesController();

        // Act
        var data = technologies.GetTechnologies();

        // Assert
        var actual = data.Any();
        Assert.AreEqual(expected, actual, "Technologies doesn't return any data.");
    }
}