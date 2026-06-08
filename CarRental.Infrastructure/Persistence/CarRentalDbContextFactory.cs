using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarRental.Infrastructure.Persistence;

public sealed class CarRentalDbContextFactory : IDesignTimeDbContextFactory<CarRentalDbContext>
{
    public CarRentalDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CarRentalDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=CarRentalDb;User Id=sa;Password=Azami_0711;TrustServerCertificate=True;");

        return new CarRentalDbContext(optionsBuilder.Options);
    }
}