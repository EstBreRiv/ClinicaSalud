using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository
{
    public class MedicoEspecialidadRepository : Repository<MedicoEspecialidad>, IMedicoEspecialidadRepository
    {

        private ApplicationDBContext _db;

        public MedicoEspecialidadRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MedicoEspecialidad medicoEspecialidad)
        {
            _db.MedicoEspecialidad.Update(medicoEspecialidad);
        }
    }
}
