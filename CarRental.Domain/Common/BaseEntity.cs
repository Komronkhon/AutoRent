using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; }
    }
}
