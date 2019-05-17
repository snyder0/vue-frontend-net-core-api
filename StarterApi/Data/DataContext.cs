using Microsoft.EntityFrameworkCore;
using StarterApi.Common.Constants;
using StarterApi.Data.Entities;
using StarterApi.Security;
using System;
using System.Linq;
using System.Reflection;

namespace StarterApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ApplyConfigurations(builder);
            SeedData(builder);
        }

        private static void SeedData(ModelBuilder builder)
        {
            var passwordHasher = new PasswordHash("admin");
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Role = RoleConstants.Admin,
                    Email = "admin@admin.com",
                    FirstName = "Seeded-Admin-FirstName",
                    LastName = "Seeded-Admin-LastName",
                    PasswordHash = passwordHasher.Hash,
                    PasswordSalt = passwordHasher.Salt
                });
        }

        private void ApplyConfigurations(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var applyGenericMethod = typeof(ModelBuilder)
                .GetMethods()
                .Where(m =>
                    m.Name == "ApplyConfiguration"
                    && m.GetParameters()
                        .First()
                        .ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)
                        .GetGenericTypeDefinition())
                .FirstOrDefault();

            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters);

            foreach (var type in types)
            {
                foreach (var @interface in type.GetInterfaces())
                {
                    if (@interface.IsConstructedGenericType && @interface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(@interface.GenericTypeArguments[0]);
                        applyConcreteMethod.Invoke(builder, new object[] { Activator.CreateInstance(type) });
                        break;
                    }
                }
            }
        }
    }
}
