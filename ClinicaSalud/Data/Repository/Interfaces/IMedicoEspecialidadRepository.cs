using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IMedicoEspecialidadRepository : IRepository<MedicoEspecialidad>
    {
        void Update(MedicoEspecialidad medicoEspecialidad);

    }
}
