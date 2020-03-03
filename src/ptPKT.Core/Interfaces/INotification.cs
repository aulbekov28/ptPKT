namespace ptPKT.Core.Interfaces
{
    public interface INotification
    {
        void Send(string email, string content);
    }
}