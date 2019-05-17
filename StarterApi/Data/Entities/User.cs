using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarterApi.Data.Map;

namespace StarterApi.Data.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName)
                .HasMaxLength(25);

            builder.Property(x => x.LastName)
                .HasMaxLength(25);
        }
    }
}
