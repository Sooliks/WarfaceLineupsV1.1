using Microsoft.EntityFrameworkCore;
using WarfaceLineupsV1._1.Database.Models;
using WarfaceLineupsV1._1.Models;

namespace WarfaceLineupsV1._1.Database.Requests;

public static class HandlerLineups
{
    public static Lineup GetVerifiedLineupByLineupId(int lineupId)
    {
        using Context db = new Context();
        return db.Lineups.FirstOrDefault(v => v.Id == lineupId && v.IsVerified == true);
    }
    public static Lineup GetLineupByLineupId(int lineupId)
    {
        using Context db = new Context();
        return db.Lineups.FirstOrDefault(v => v.Id == lineupId);
    }

    public static void AddNewLineup(string title, string description, string urlOnVideo, Map map, byte typeSide, byte typeFeature, byte typePlant, Account owner)
    {
        using Context db = new Context();
        var lineup = new Lineup(title, description, urlOnVideo, map, typeSide, typeFeature, typePlant, null, owner);
        db.Lineups.Add(lineup);
        db.SaveChangesAsync();
    }

    public static void AddNewLineup(string title, string description, Map map, byte typeSide, byte typeFeature, byte typePlant, IFormFileCollection screenshots, Account owner)
    {
        using Context db = new Context();
        var lineup = new Lineup(title, description, null, map, typeSide, typeFeature, typePlant, null, owner);
        db.Entry(lineup).Collection(c=>c.Screenshots).Load();
        foreach (var screenshot in screenshots)
        {
            using (var ms = new MemoryStream())
            {
                screenshot.CopyTo(ms);
                var fileBytes = ms.ToArray();
                lineup.Screenshots.Add(new Screenshot(fileBytes));
            }
        }
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
    public static int GetCountVerifiedLineups(FilterForLineupsData filterForLineupsData)
    {
        using Context db = new Context();
        return db.Lineups.Count(v=> (v.TypeSide == filterForLineupsData.TypeSide || filterForLineupsData.TypeSide == 10) && (v.TypeFeature == filterForLineupsData.TypeFeature || filterForLineupsData.TypeFeature == 10) && (v.TypeMap.Id == filterForLineupsData.TypeGameMap || filterForLineupsData.TypeGameMap == 10) && (v.TypePlant == filterForLineupsData.TypePlant || filterForLineupsData.TypePlant == 10) && (v.Title.ToLower().StartsWith(filterForLineupsData.Search.ToLower()) || v.Title == "") && v.IsVerified == true);
    }
    public static int GetCountAllLineups(FilterForLineupsData filterForLineupsData)
    {
        using Context db = new Context();
        return db.Lineups.Count(v=> (v.TypeSide == filterForLineupsData.TypeSide || filterForLineupsData.TypeSide == 10) && (v.TypeFeature == filterForLineupsData.TypeFeature || filterForLineupsData.TypeFeature == 10) && (v.TypeMap.Id == filterForLineupsData.TypeGameMap || filterForLineupsData.TypeGameMap == 10) && (v.TypePlant == filterForLineupsData.TypePlant || filterForLineupsData.TypePlant == 10) && (v.Title.ToLower().StartsWith(filterForLineupsData.Search.ToLower()) || v.Title == ""));
    }
    public static int GetCountAllLineupsByOwnerId(FilterForLineupsData filterForLineupsData, int ownerId)
    {
        using Context db = new Context();
        return db.Lineups.Count(v=> (v.TypeSide == filterForLineupsData.TypeSide || filterForLineupsData.TypeSide == 10) && (v.TypeFeature == filterForLineupsData.TypeFeature || filterForLineupsData.TypeFeature == 10) && (v.TypeMap.Id == filterForLineupsData.TypeGameMap || filterForLineupsData.TypeGameMap == 10) && (v.TypePlant == filterForLineupsData.TypePlant || filterForLineupsData.TypePlant == 10) && (v.Title.ToLower().StartsWith(filterForLineupsData.Search.ToLower()) || v.Title == "") && v.Owner.Id == ownerId);
    }
    public static int GetCountVerifiedLineupsByOwnerId(FilterForLineupsData filterForLineupsData, int ownerId)
    {
        using Context db = new Context();
        return db.Lineups.Count(v=> (v.TypeSide == filterForLineupsData.TypeSide || filterForLineupsData.TypeSide == 10) && (v.TypeFeature == filterForLineupsData.TypeFeature || filterForLineupsData.TypeFeature == 10) && (v.TypeMap.Id == filterForLineupsData.TypeGameMap || filterForLineupsData.TypeGameMap == 10) && (v.TypePlant == filterForLineupsData.TypePlant || filterForLineupsData.TypePlant == 10) && (v.Title.ToLower().StartsWith(filterForLineupsData.Search.ToLower()) || v.Title == "") && v.Owner.Id == ownerId && v.IsVerified == true);
    }
    public static List<Lineup> GetVerifiedLineups(FilterForLineupsData filterForLineupsData, int minId, int count)
    {
        using Context db = new Context();
        
        return db.Lineups.OrderByDescending(v=>v.Id).Where(v=> (v.TypeSide == filterForLineupsData.TypeSide || filterForLineupsData.TypeSide == 10) && (v.TypeFeature == filterForLineupsData.TypeFeature || filterForLineupsData.TypeFeature == 10) && (v.TypeMap.Id == filterForLineupsData.TypeGameMap || filterForLineupsData.TypeGameMap == 10) && (v.TypePlant == filterForLineupsData.TypePlant || filterForLineupsData.TypePlant == 10) && (v.Title.ToLower().StartsWith(filterForLineupsData.Search.ToLower()) || v.Title == "") && v.IsVerified == true).Skip(minId).Take(count).Include(v=>v.TypeMap).ToList();
    }
    public static List<Lineup> GetUnVerifiedLineups(int minId, int count)
    {
        using Context db = new Context();
        return db.Lineups.OrderByDescending(v=>v.Id).Where(v=> v.IsVerified == false).Skip(minId).Take(count).Include(v=>v.TypeMap).ToList();
    }
    public static List<Lineup> GetVerifiedLineupsByOwnerId(FilterForLineupsData filterForLineupsData, int minId, int count, int ownerId)
    {
        using Context db = new Context();
        return db.Lineups.OrderByDescending(v=>v.Id).Where(v=> (v.TypeSide == filterForLineupsData.TypeSide || filterForLineupsData.TypeSide == 10) && (v.TypeFeature == filterForLineupsData.TypeFeature || filterForLineupsData.TypeFeature == 10) && (v.TypeMap.Id == filterForLineupsData.TypeGameMap || filterForLineupsData.TypeGameMap == 10) && (v.TypePlant == filterForLineupsData.TypePlant || filterForLineupsData.TypePlant == 10) && (v.Title.ToLower().StartsWith(filterForLineupsData.Search.ToLower()) || v.Title == "") && v.IsVerified == true && v.Owner.Id == ownerId).Skip(minId).Take(count).Include(v=>v.TypeMap).ToList();
    }
    public static List<Lineup> GetAllLineupsByOwnerId(FilterForLineupsData filterForLineupsData, int minId, int count, int ownerId)
    {
        using Context db = new Context();
        return db.Lineups.OrderByDescending(v=>v.Id).Where(v=> (v.TypeSide == filterForLineupsData.TypeSide || filterForLineupsData.TypeSide == 10) && (v.TypeFeature == filterForLineupsData.TypeFeature || filterForLineupsData.TypeFeature == 10) && (v.TypeMap.Id == filterForLineupsData.TypeGameMap || filterForLineupsData.TypeGameMap == 10) && (v.TypePlant == filterForLineupsData.TypePlant || filterForLineupsData.TypePlant == 10) && (v.Title.ToLower().StartsWith(filterForLineupsData.Search.ToLower()) || v.Title == "") && v.Owner.Id == ownerId).Skip(minId).Take(count).Include(v=>v.TypeMap).ToList();
    }
}