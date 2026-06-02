using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken); 
    }
}
