using WarfaceLineupsV1._1.Database.Models;

namespace WarfaceLineupsV1._1.Database.Requests;

public class HandlerMaps
{
    public static void AddMap(string name)
    {
        using Context db = new Context();
        var map = new Map(name);
        db.Add(map);
        db.SaveChangesAsync();
    }
    public static void DeleteMap(int id)
    {
        using Context db = new Context();
        var map = db.Maps.FirstOrDefault(m => m.Id == id);
        if (map != null)
        {
            db.Maps.Remove(map);
            db.SaveChangesAsync();
        }
    }

    public static void UpdateMap(int id, string newName)
    {
        using Context db = new Context();
        var map = db.Maps.FirstOrDefault(m => m.Id == id);
        if (map != null)
        {
            map.Name = newName;
            db.Maps.Update(map);
            db.SaveChangesAsync();
        }
    }
}