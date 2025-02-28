namespace GETFlightApp.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityType, int id) 
        : base($"Entity of type {entityType} with an id of {id} doesn't exist.")
    {
        
    }
}
