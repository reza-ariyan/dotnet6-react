namespace App.Dto;

public class Candidate
{
    public Guid CandidateId { get; set; }
    public string FullName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Gender { get; set; }
    public string ProfilePicture { get; set; }
    public string Email { get; set; }
    public string FavoriteMusicGenre { get; set; }
    public string Dad { get; set; }
    public string Mom { get; set; }
    public bool CanSwim { get; set; }
    public string Barcode { get; set; }
    public List<Experience> Experience { get; set; }
}