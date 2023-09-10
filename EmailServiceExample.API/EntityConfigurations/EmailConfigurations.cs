using EmailServiceExample.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmailServiceExample.API.EntityConfigurations
{
    public class EmailConfigurations : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.Property(x=>x.EmailAddress).HasMaxLength(320);
            builder.Property(x=>x.IsActive).HasDefaultValue(true);
            builder.Property(x=>x.CreatedDate).HasDefaultValue(DateTime.Now);
            
        }
    }
}
