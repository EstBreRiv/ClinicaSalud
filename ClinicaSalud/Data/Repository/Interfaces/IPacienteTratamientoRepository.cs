using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IPacienteTratamientoRepository : IRepository<PacienteTratamiento>
    {
        void Update(PacienteTratamiento pacienteTratamiento);
    }
}
