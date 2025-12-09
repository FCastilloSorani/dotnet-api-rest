namespace Api.Exceptions;

public class UserAlreadyExistsException(string message) : Exception(message)
{
}