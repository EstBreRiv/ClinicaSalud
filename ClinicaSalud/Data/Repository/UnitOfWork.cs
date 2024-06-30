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

            Medico = new MedicoRepository(_db);

            Padecimiento = new PadecimientoRepository(_db);

            Paciente = new PacienteRepository(_db);
        }

        public IEspecialidadRepository Especialidad { get; private set; }

        public IMedicamentoRepository Medicamento { get; private set; }

        public ITratamientoRepository Tratamiento { get; private set; }

        public IMedicoRepository Medico { get; private set; }

        public IPadecimientoRepository Padecimiento { get; private set; }

        public IPacienteRepository Paciente { get; private set; }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
