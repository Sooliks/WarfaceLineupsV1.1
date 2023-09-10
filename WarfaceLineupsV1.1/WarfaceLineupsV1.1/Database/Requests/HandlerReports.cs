using WarfaceLineupsV1._1.Database.Models;

namespace WarfaceLineupsV1._1.Database.Requests;

public class HandlerReports
{
    public static List<Report> GetExpectedReportsList()
    {
        using Context db = new Context();
        return db.Reports.Where(r => r.Status == "expected").ToList();
    }
    public static bool AddNewReport(Account sender,Lineup lineup, string typeReport)
    {
        using Context db = new Context();
        var r = db.Reports.FirstOrDefault(r => r.Lineup == lineup && r.Sender == sender);
        if (r != null) return false;
        
        var report = new Report(sender, lineup, typeReport);
        db.Reports.Add(report);
        db.SaveChangesAsync();
        return true;
    }
    public static void SetReportComplete(int reportId)
    {
        using Context db = new Context();
        var report = db.Reports.FirstOrDefault(r => r.Id == reportId);
        if (report != null)
        {
            report.Status = "completed";
            db.Reports.Update(report);
            db.SaveChangesAsync();
        }
    }
}