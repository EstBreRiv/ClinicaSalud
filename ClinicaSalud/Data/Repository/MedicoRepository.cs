using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository
{
    public class MedicoRepository : Repository<Medico>, IMedicoRepository
    {
        private ApplicationDBContext _db;

        public MedicoRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Medico medico)
        {
            _db.Medico.Update(medico);
        }
    }
}
