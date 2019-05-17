using Microsoft.EntityFrameworkCore;
using StarterApi.Data.Entities;
using StarterApi.Data.Map;
using StarterApi.Data.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
