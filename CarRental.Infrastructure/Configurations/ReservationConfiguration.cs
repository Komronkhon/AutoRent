using CarRental.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Infrastructure.Configurations
{
    public sealed class ReservationConfiguration
    : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("reservations");

            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.CarId)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>();

            builder.ComplexProperty(
                x => x.RentalPeriod,
                rental =>
                {
                    rental.Property(x => x.StartDate)
                        .HasColumnName("start_date");

                    rental.Property(x => x.EndDate)
                        .HasColumnName("end_date");
                });
        }
    }
}
