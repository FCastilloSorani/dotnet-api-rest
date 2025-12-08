namespace Api.Services;

public class MessageService : IMessageService
{
    public string GetWelcomeMessage(string username)
    {
        return $"Hello {username}! This is a .NET API!";
    }
}