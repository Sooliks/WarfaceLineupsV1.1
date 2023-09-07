namespace WarfaceLineupsV1._1.Database.Models;

public class Maps
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Maps()
    {
        
    }
    public Maps(string name)
    {
        this.Name = name;
    }
}