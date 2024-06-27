using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IMedicamentoRepository : IRepository<Medicamento>
    {

        void Update(Medicamento medicamento);
    }
}