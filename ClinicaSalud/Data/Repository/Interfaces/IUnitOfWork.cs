namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IEspecialidadRepository Especialidad { get; }
        IMedicoRepository Medico { get; }

        IMedicamentoRepository Medicamento { get; }

        ITratamientoRepository Tratamiento { get; }

        void save();
    }
}
