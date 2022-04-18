using System.Linq;
using App.Controllers;
using App.Dto;
using NUnit.Framework;

namespace APP.Tests;

public class Candidates
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Get_Candidates_Has_Data()
    {
        // Arrange
        const bool expected = true;
        var candidates = new CandidatesController();

        // Act
        var data = candidates.GetCandidates(new Criteria());

        // Assert
        var actual = data.Any();
        Assert.AreEqual(expected, actual, "Candidates doesn't show any data.");
    }


    [Test]
    public void Never_Show_Candidate_After_Being_Accepted()
    {
        // Arrange
        const bool expected = false;
        var candidates = new CandidatesController();

        // Act
        var firstCandidate = candidates.GetCandidates(new Criteria()).First();
        candidates.Accept(firstCandidate.CandidateId);
        var data = candidates.GetCandidates(new Criteria());

        // Assert
        var exist = data.Any(c => c.CandidateId.Equals(firstCandidate.CandidateId));
        Assert.AreEqual(expected, exist, "Accepted Candidate is being shown !");
    }


    [Test]
    public void Never_Show_Candidate_After_Being_Rejected()
    {
        // Arrange
        const bool expected = false;
        var candidates = new CandidatesController();

        // Act
        var firstCandidate = candidates.GetCandidates(new Criteria()).First();
        candidates.Accept(firstCandidate.CandidateId);
        var data = candidates.GetCandidates(new Criteria());

        // Assert
        var exist = data.Any(c => c.CandidateId.Equals(firstCandidate.CandidateId));
        Assert.AreEqual(expected, exist, "Rejected Candidate is being shown !");
    }
}