namespace GETFlightApp.Domain.Entities;

public class RoleUseCase 
{
    public int RoleId { get; set; }
    public int UseCaseId { get; set; }
    public virtual Role Role { get; set; }
}
