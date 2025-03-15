using SampleCabDataETLProject.DAL.Repositories.Contracts;
using System.Threading.Tasks;

namespace SampleCabDataETLProject.DAL.Infrastructure.Interfaces;

public interface IUnitOfWork
{
    ISampleCabDataRepository SampleCabDataRepository { get; }
    Task SaveChangesAsync();
}