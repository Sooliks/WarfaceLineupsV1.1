namespace WarfaceLineupsV1._1.Models;

public record AddLineupData(string Title, string Description, byte TypeGameMap, byte TypeSide, byte TypeFeature, byte TypePlant, string UrlOnLineup);