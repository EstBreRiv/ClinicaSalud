using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository
{
    public class PacienteMedicamentoRepository : Repository<PacienteMedicamento>, IPacienteMedicamentoRepository
    {

        private ApplicationDBContext _db;

        public PacienteMedicamentoRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PacienteMedicamento pacienteMedicamento)
        {
            _db.PacienteMedicamento.Update(pacienteMedicamento);
        }
    }   
    
}
