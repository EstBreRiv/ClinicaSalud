using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository
{
    public class PadecimientoRepository : Repository<Padecimiento>, IPadecimientoRepository
    {
        private ApplicationDBContext _db;

        public PadecimientoRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Padecimiento padecimiento)
        {
            _db.Padecimiento.Update(padecimiento);
        }
    }
}
