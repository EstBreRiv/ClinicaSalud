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
            Medico = new MedicoRepository(_db);
        }

        public IEspecialidadRepository Especialidad { get; private set; }

        public IMedicoRepository Medico { get; private set; }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
