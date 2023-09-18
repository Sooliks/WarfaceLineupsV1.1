namespace WarfaceLineupsV1._1.Database.Models;

public class Map
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Map()
    {
        
    }
    public Map(string name)
    {
        this.Name = name;
    }
}