using GETFlightApp.Domain.Primitives;

namespace GETFlightApp.Domain.Entities;

public class Role : Entity
{
    public string Name { get; set; }
    public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    public virtual ICollection<RoleUseCase> UseCases { get; set; } = new HashSet<RoleUseCase>();
}
