using ClinicaSalud.Data.Repository.Interfaces;
using ClinicaSalud.Models;

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

            MedicoEspecialidad = new MedicoEspecialidadRepository(_db);

            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public IEspecialidadRepository Especialidad { get; private set; }

        public IMedicamentoRepository Medicamento { get; private set; }

        public ITratamientoRepository Tratamiento { get; private set; }

        public IMedicoRepository Medico { get; private set; }

        public IPadecimientoRepository Padecimiento { get; private set; }

        public IPacienteRepository Paciente { get; private set; }

        public IMedicoEspecialidadRepository MedicoEspecialidad { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
