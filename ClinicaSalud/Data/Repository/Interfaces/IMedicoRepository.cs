using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IMedicoRepository : IRepository<Medico>
    {
        void Update(Medico medico);
    }
}
