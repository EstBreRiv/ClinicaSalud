using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IPacienteRepository : IRepository<Paciente>
    {

        void Update(Paciente paciente);
    }
}
