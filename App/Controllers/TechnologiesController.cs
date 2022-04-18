using App.Dto;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class TechnologiesController : ControllerBase
{
    private const string ApiUrl = "https://app.ifs.aero/EternalBlue/api/technologies";
    private static List<Technology> _technologies = new();

    public TechnologiesController()
    {
        if (_technologies.Any()) return;
        _technologies = Utils.Utils.GetDataFromApi<List<Technology>>(ApiUrl);
    }

    /// <summary>
    /// Returns all technologies
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public List<TechnologyDto> GetTechnologies()
    {
        return _technologies.Select(m => new TechnologyDto(m.Guid, m.Name)).ToList();
    }
}