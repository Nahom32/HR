namespace Human_Resources.Hubs
{
    public interface IMessageHub
    {
        Task ReceiveMessage(string message);

    }
}
