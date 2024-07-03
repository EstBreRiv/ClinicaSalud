namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IEspecialidadRepository Especialidad { get; }
        IMedicoRepository Medico { get; }

        IMedicamentoRepository Medicamento { get; }

        ITratamientoRepository Tratamiento { get; }

        IPadecimientoRepository Padecimiento { get; }

        IPacienteRepository Paciente { get; }

        IMedicoEspecialidadRepository MedicoEspecialidad { get; }

        IPacienteMedicamentoRepository PacienteMedicamento { get; }

        IPacienteTratamientoRepository PacienteTratamiento { get; }

        IPacientePadecimientoRepository PacientePadecimiento { get; }
        IApplicationUserRepository ApplicationUser { get; }

        void save();
    }
}
