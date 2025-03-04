using Microsoft.EntityFrameworkCore;
using SMS.Domain.Common;

namespace SMS.Helper
{
    public static class DataBaseHelper
    {
        public static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string dbProvider, string connectionString)
        {
            return dbProvider.ToLowerInvariant() switch
            {
                Constants.DbProviderKey.Npgsql => builder.UseNpgsql(connectionString, e =>
                e.MigrationsAssembly("SMS.Migrators.PostgresSQL")),
                Constants.DbProviderKey.SqlServer => builder.UseSqlServer(connectionString, e =>
                    e.MigrationsAssembly("SMS.Migrators.SQLServer")),
                _ => throw new InvalidOperationException($"DB Provider {dbProvider} is not supported."),
            };
        }
    }
}
