namespace WarfaceLineupsV1._1.Database.Models;

public class Screenshot
{
    public int Id { get; set; }
    public byte[] Data { get; set; }
    public Lineup Lineup { get; set; }

    public Screenshot()
    {
        
    }

    public Screenshot(byte[] data, Lineup lineup)
    {
        this.Data = data;
        this.Lineup = lineup;
    }
}