using ClinicaSalud.Data.Repository.Interfaces;

namespace ClinicaSalud.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDBContext _db;

        public UnitOfWork(ApplicationDBContext db)
        {

            _db = db;

            Especialidad = new EspecialidadRepository(_db);

            Medicamento = new MedicamentoRepository(_db);
            
            Tratamiento = new TratamientoRepository(_db);
        }

        public IEspecialidadRepository Especialidad { get; private set; }

        public IMedicamentoRepository Medicamento { get; private set; }

        public ITratamientoRepository Tratamiento { get; private set; }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
