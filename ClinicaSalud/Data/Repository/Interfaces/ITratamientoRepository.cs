using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface ITratamientoRepository : IRepository<Tratamiento>
    {

        void Update(Tratamiento tratamiento);
    }
}