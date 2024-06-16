using ClinicaSalud.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicaSalud.Data
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Especialidad> Especialidad { get; set; }
    }
}
