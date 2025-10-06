using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Dal.Models
{
    /// <summary>
    /// Фабрика создания контекста базы данных
    /// </summary>
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        /// <summary>
        /// Создание контекста БД
        /// </summary>
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=tf_identity;Username=postgres;Password=8961", b => 
                b.MigrationsAssembly("Dal"));

            return new IdentityDbContext(optionsBuilder.Options);
        }
    }
}