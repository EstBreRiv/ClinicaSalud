using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        private ApplicationDBContext _db;

        public PacienteRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Paciente paciente)
        {
            _db.Paciente.Update(paciente);
        }
    }
}
