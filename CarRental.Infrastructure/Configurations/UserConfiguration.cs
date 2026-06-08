using CarRental.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Infrastructure.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.ComplexProperty(x => x.FullName, fullName =>
            {
                fullName.Property(x => x.Value)
                    .HasColumnName("full_name")
                    .HasMaxLength(255)
                    .IsRequired();
            });

            builder.ComplexProperty(x => x.Email, email =>
            {
                email.Property(x => x.Value)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsRequired();
            });

            builder.ComplexProperty(x => x.PhoneNumber, phone =>
            {
                phone.Property(x => x.Value)
                    .HasColumnName("phone_number")
                    .HasMaxLength(20)
                    .IsRequired();
            });

            builder.ComplexProperty(x => x.PassportNumber, passport =>
            {
                passport.Property(x => x.Value)
                    .HasColumnName("passport_number")
                    .HasMaxLength(20)
                    .IsRequired();
            });
        }
    }
}
