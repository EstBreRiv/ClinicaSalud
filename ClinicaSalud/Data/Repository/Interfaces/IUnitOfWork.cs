namespace ClinicaSalud.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IEspecialidadRepository Especialidad { get; }

        void save();
    }
}
