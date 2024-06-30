using ClinicaSalud.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicaSalud.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Especialidad> Especialidad { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Tratamiento> Tratamiento{ get; set; }
        public DbSet<Medicamento> Medicamento{ get; set; }
        public DbSet<Padecimiento> Padecimiento{ get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
    }
}
