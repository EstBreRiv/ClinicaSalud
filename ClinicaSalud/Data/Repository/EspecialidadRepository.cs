using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository
{
    public class EspecialidadRepository : Repository<Especialidad>, IEspecialidadRepository
    {

        private ApplicationDBContext _db;

        public EspecialidadRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Especialidad especialidad)
        {
            _db.Especialidad.Update(especialidad);
        }
    }
}
