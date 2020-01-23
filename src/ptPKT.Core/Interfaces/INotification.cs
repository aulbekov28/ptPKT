namespace ptPKT.Core.Interfaces
{
    public interface INotification
    {
        void SendNotification(string email, string content);
    }
}