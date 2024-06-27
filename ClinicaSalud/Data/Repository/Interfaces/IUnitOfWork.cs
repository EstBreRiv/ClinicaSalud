namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IEspecialidadRepository Especialidad { get; }
        IMedicoRepository Medico { get; }

        void save();
    }
}
