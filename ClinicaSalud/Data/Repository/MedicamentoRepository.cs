using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository
{
    public class MedicamentoRepository : Repository<Medicamento>, IMedicamentoRepository
    {

        private ApplicationDBContext _db;

        public MedicamentoRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Medicamento medicamento)
        {
            _db.Medicamento.Update(medicamento);
        }
    }
}
