using App.Dto;
using App.Utils;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class CandidatesController : ControllerBase
{
    private const string ApiUrl = "https://app.ifs.aero/EternalBlue/api/candidates";
    private static List<Candidate> _candidates = new();
    private static readonly List<Candidate> AcceptedCandidates = new();

    public CandidatesController()
    {
        if (_candidates.Any()) return;
        _candidates = Utils.Utils.GetDataFromApi<List<Candidate>>(ApiUrl);
    }

    /// <summary>
    /// Returns all candidates list after applying filters
    /// </summary>
    /// <param name="criteria">filters</param>
    /// <returns></returns>
    [HttpGet]
    public List<Candidate> GetCandidates([FromQuery] Criteria criteria)
    {
        var data = _candidates.Filter(criteria);
        var result = data.Skip(criteria.Page - 1 * criteria.PageSize).Take(criteria.PageSize).ToList();
        return result;
    }

    /// <summary>
    /// Returns all accepted candidates
    /// </summary>
    /// <returns></returns>
    [HttpGet("accepted")]
    public List<Candidate> GetAcceptedCandidates()
    {
        return AcceptedCandidates.ToList();
    }

    /// <summary>
    /// Accepts a candidate
    /// </summary>
    /// <param name="id"></param>
    [HttpPost("accept/{id:guid}")]
    public bool Accept(Guid id)
    {
        var candidate = _candidates.FirstOrDefault(c => c.CandidateId.Equals(id));
        if (candidate == null) return false;
        AcceptedCandidates.Add(candidate);
        return _candidates.Remove(candidate);
    }

    /// <summary>
    /// Rejects a candidate
    /// </summary>
    /// <param name="id"></param>
    [HttpPost("reject/{id:guid}")]
    public bool Reject(Guid id)
    {
        var candidate = _candidates.FirstOrDefault(c => c.CandidateId.Equals(id));
        if (candidate == null) return false;
        return _candidates.Remove(candidate);
    }
}