using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IEspecialidadRepository : IRepository<Especialidad>
    {

        void Update(Especialidad especialidad);
    }
}
