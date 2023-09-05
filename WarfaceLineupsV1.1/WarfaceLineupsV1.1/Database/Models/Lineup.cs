namespace WarfaceLineupsV1._1.Database.Models;

public class Lineup
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsVerified { get; set; }
    public string UrlOnVideo { get; set; }
    public byte TypeGameMap { get; set; } 
    public byte TypeSide { get; set; }
    public byte TypeFeature { get; set; } 
    public byte TypePlant { get; set; }
    public string UrlOnPreview { get; set; }
    public Account Owner { get; set; }
    public List<Comment> Comments { get; set; }

    public Lineup()
    {
        
    }
    public Lineup(string title, string description, string urlOnVideo, byte typeGameMap, byte typeSide, byte typeFeature, byte typePlant, string urlOnPreview, Account owner)
    {
        this.Title = title;
        this.Description = description;
        this.IsVerified = false;
        this.UrlOnVideo = urlOnVideo;
        this.TypeGameMap = typeGameMap;
        this.TypeSide = typeSide;
        this.TypeFeature = typeFeature;
        this.TypePlant = typePlant;
        this.UrlOnPreview = urlOnPreview;
        this.Owner = owner;
        this.Comments = new List<Comment>();
    }
}