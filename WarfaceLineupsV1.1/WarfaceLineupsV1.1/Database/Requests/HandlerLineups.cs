using WarfaceLineupsV1._1.Database.Models;

namespace WarfaceLineupsV1._1.Database.Requests;

public static class HandlerLineups
{
    public static Lineup GetLineupByLineupId(int lineupId)
    {
        using Context db = new Context();
        return db.Lineups.SingleOrDefault(v => v.Id == lineupId);
    }

    public static void AddNewLineup(string title, string description, string urlOnVideo, byte typeGameMap, byte typeSide, byte typeFeature, byte typePlant, string urlOnPreview, Account owner)
    {
        using Context db = new Context();
        var lineup = new Lineup(title, description, urlOnVideo, typeGameMap, typeSide, typeFeature, typePlant,
            urlOnPreview, owner);
        db.Lineups.Add(lineup);
        db.SaveChangesAsync();
    }
    public static void DeleteLineup(int idLineup)
    {
        using Context db = new Context();
        var lineup = db.Lineups.FirstOrDefault(v=>v.Id==idLineup);
        if (lineup != null)
        {
            db.Lineups.Remove(lineup);
            db.SaveChangesAsync();
        }
    }
    public static void ToPublishLineup(int idLineup)
    {
        using Context db = new Context();
        var lineup = db.Lineups.SingleOrDefault(v => v.Id == idLineup);
        lineup.IsVerified = true;
        db.Lineups.Update(lineup);
        db.SaveChangesAsync();
    }
    public static bool VideoIsDuplicate(string urlOnVideo)
    {
        using Context db = new Context();
        var lineup = db.Lineups.FirstOrDefault(v => v.UrlOnVideo == urlOnVideo);
        if (lineup != null)
        {
            return true;
        }
        return false;
    }
}