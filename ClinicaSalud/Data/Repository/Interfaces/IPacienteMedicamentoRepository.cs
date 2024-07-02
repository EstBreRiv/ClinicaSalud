using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IPacienteMedicamentoRepository : IRepository<PacienteMedicamento>
    {
        void Update(PacienteMedicamento pacienteMedicamento);
    }
}
