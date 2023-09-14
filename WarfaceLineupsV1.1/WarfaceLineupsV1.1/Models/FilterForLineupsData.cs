namespace WarfaceLineupsV1._1.Models;

public class FilterForLineupsData
{
    public byte TypeSide { get; set; }
    public byte TypeGameMap { get; set; }
    public byte TypeFeature { get; set; }
    public byte TypePlant { get; set; }
    public string Search { get; set; }

    public FilterForLineupsData()
    {
        
    }
};
