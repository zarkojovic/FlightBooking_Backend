using GETFlightApp.DataAccess;

namespace GETFlightApp.Implementation.UseCases;

public abstract class EfUseCase
{
    private readonly AspContext _context;
    protected EfUseCase(AspContext context)
    {
        _context = context;
    }
    protected AspContext Context => _context;
}