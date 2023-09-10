using EmailServiceExample.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmailServiceExample.API.EntityConfigurations
{
    public class EmailHistoryConfigurationcs : IEntityTypeConfiguration<EmailHistory>
    {
        public void Configure(EntityTypeBuilder<EmailHistory> builder)
        {
            builder.Property(x=>x.Subject).HasMaxLength(100);
            builder.Property(x=>x.SenderEmail).HasMaxLength(320);
            builder.Property(x => x.SendDate).HasDefaultValue(DateTime.Now);
        }
    }
}
