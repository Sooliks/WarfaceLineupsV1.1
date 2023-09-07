using WarfaceLineupsV1._1.Database.Models;

namespace WarfaceLineupsV1._1.Database.Requests;

public class HandlerNotifications
{
    public static void SendNotify(Account adminAccount, Account recipientAccount, string title, string text)
    {
        using (Context db = new Context())
        {
            var notify = new Notification(adminAccount,recipientAccount, title, text);
            db.Notifications.Add(notify);
            db.SaveChangesAsync();
        }
    }
    public static void DeleteNotify(Account recipientAccount, int notifyId)
    {
        using (Context db = new Context())
        {
            var notification = db.Notifications.FirstOrDefault(n => n.Recipient == recipientAccount && n.Id == notifyId);
            if (notification != null)
            {
                db.Notifications.Remove(notification);
                db.SaveChangesAsync();
            }
        }
    }
    public static List<Notification> GetNotificationsByRecipientAccount(Account recipientAccount)
    {
        using Context db = new Context();
        return db.Notifications.OrderByDescending(n => n.Recipient == recipientAccount).ToList();
    }
    public static int GetCountNotificationsOfAccount(Account account)
    {
        using Context db = new Context();
        return db.Notifications.Count(n => n.Recipient == account);
    }
}