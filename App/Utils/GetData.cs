using System.Net;
using App.Dto;
using Newtonsoft.Json;

namespace App.Utils;

public static class Utils
{
    /// <summary>
    /// Get data from an api
    /// </summary>
    /// <param name="uri">Api url to get data from</param>
    /// <typeparam name="T">Return data type (the Api result will be casted to this type)</typeparam>
    /// <returns></returns>
    public static T GetDataFromApi<T>(string uri) where T : new()
    {
        var webRequest = WebRequest.Create(uri);
        using var response = webRequest.GetResponse();
        using var content = response.GetResponseStream();
        using var reader = new StreamReader(content);
        var strContent = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<T>(strContent) ?? new T();
    }

    /// <summary>
    /// Applies filters on the list of candidates
    /// </summary>
    /// <param name="candidates">Candidates list to be filtered</param>
    /// <param name="criteria">Filter data</param>
    /// <returns></returns>
    public static IEnumerable<Candidate> Filter(this List<Candidate> candidates, Criteria criteria)
    {
        if (criteria.Technology is not null && !criteria.Technology.Equals(Guid.Empty))
            candidates = candidates.Where(c => c.Experience.Any(e => e.TechnologyId.Equals(criteria.Technology)))
                .ToList();

        if (!criteria.YearsOfExperience.Equals(0))
            candidates = candidates.Where(c => c.Experience.Any(e => e.YearsOfExperience >= criteria.YearsOfExperience))
                .ToList();
        return candidates;
    }
}