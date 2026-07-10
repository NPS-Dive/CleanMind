using CleanMind.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanMind.Persistence.Configurations;

public class ClinicConfig : IEntityTypeConfiguration<Clinic>
    {
        void IEntityTypeConfiguration<Clinic>.Configure ( EntityTypeBuilder<Clinic> builder )
        {
            builder.Property(prop=>prop.Name)
                .HasMaxLength(150)
                .IsRequired();
        }
    }