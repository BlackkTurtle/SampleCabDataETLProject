using SampleCabDataETLProject.DAL.Infrastructure.Interfaces;
using SampleCabDataETLProject.DAL.Persistence;
using SampleCabDataETLProject.DAL.Repositories.Contracts;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.DAL.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public ISampleCabDataRepository SampleCabDataRepository { get; }

    public UnitOfWork
        (AppDbContext context,
        ISampleCabDataRepository sampleCabDataRepository)
    {
        _appDbContext = context;
        SampleCabDataRepository = sampleCabDataRepository;
    }

    public async Task SaveChangesAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }
}