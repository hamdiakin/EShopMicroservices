namespace BuildingBlocks.Exceptions;

public class InternalServerException : Exception
{
    public InternalServerException(string message) : base(message)
    {
    }

    public InternalServerException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public InternalServerException(string name, object key)
        : base($"Entity \"{name}\" ({key}) encountered an internal server error.")
    {
    }

    public static InternalServerException Create<T>(Guid id)
    {
        return new InternalServerException($"The {typeof(T).Name} with ID {id} encountered an internal server error.");
    }
    
    // with string message and string details
    public  InternalServerException(string message, string details) : base(message)
    {
        Details = details;
    }

    public string? Details { get; }
}