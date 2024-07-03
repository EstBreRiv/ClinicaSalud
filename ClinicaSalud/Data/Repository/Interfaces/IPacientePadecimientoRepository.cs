using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IPacientePadecimientoRepository : IRepository<PacientePadecimiento>
    {
        void Update(PacientePadecimiento pacienteMedicamento);
    }
}
