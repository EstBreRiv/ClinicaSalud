using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IPadecimientoRepository : IRepository<Padecimiento>
    {
        void Update(Padecimiento padecimiento);
    }
}
