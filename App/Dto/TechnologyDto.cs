namespace App.Dto;

public class TechnologyDto
{
    public TechnologyDto(Guid value, string label)
    {
        Value = value;
        Label = label;
    }

    public Guid Value { get; set; }
    public string Label { get; set; }
}