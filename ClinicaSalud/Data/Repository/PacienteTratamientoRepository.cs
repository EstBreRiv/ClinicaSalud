using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository
{
    public class PacienteTratamientoRepository : Repository<PacienteTratamiento>, IPacienteTratamientoRepository
    {

        private ApplicationDBContext _db;

        public PacienteTratamientoRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PacienteTratamiento pacienteTratamiento)
        {
            _db.PacienteTratamiento.Update(pacienteTratamiento);
        }
    }
}
