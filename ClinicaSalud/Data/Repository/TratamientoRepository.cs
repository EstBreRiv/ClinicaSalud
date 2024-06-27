using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;

namespace ClinicaSalud.Data.Repository
{
    public class TratamientoRepository : Repository<Tratamiento>, ITratamientoRepository
    {

        private ApplicationDBContext _db;

        public TratamientoRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Tratamiento tratamiento)
        {
            _db.Tratamiento.Update(tratamiento);
        }
    }
}
