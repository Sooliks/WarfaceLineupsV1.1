namespace WarfaceLineupsV1._1.Database.Models;

public class Report
{
    public int Id { get; set; }
    public string Status { get; set; }
    public Account Sender { get; set; }
    public Lineup Lineup { get; set; }
    public string TypeReport { get; set; }
    
    public Report()
    {
        
    }

    public Report(Account sender, Lineup lineup, string typeReport)
    {
        Status = "expected";
        Sender = sender;
        Lineup = lineup;
        TypeReport = typeReport;
    }
}