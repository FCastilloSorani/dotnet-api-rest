namespace Api.Services;

public class MessageService : IMessageService
{
    public string GetWelcomeMessage()
    {
        return "Hello world from a .NET API!";
    }
}