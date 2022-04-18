namespace App.Dto;

public class Criteria
{
    public int PageSize { get; set; } = 200;
    public int Page { get; set; } = 1;
    public Guid? Technology { get; set; }
    public int YearsOfExperience { get; set; }
}