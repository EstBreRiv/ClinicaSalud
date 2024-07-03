using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository
{
    public class PacientePadecimientoRepository : Repository<PacientePadecimiento>, IPacientePadecimientoRepository
    {

        private ApplicationDBContext _db;

        public PacientePadecimientoRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PacientePadecimiento pacientePadecimiento)
        {
            _db.PacientePadecimiento.Update(pacientePadecimiento);
        }
    }
}
