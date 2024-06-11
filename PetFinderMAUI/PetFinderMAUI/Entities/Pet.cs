namespace PetFinderMAUI.Entities;

public class Pet
{
    public string? PetId { get; set; }
    public string? PetName { get; set; }
    public string? PetDescription { get; set; }
    public string? PetImage { get; set; }
    public bool PetFavourite { get; set; }
    public string? PublisherId { get; set; }
    public string PetAge { get; set; } = "not mentioned";
    public string Gender { get; set; } = "not mentioned";
    public bool Vaccinated { get; set; }
    public string PetBreed { get; set; } = "not mentioned";
    public string PetCategory { get; set; } = "not mentioned";
    public string PetStatus { get; set; } = "waiting";
    public DateTime Timestamp { get; set; } = DateTime.Now;
}