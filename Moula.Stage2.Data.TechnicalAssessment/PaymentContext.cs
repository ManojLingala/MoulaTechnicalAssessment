using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moula.Stage2.Data.TechnicalAssessment.Data.Entities;
using System;

namespace Moula.Stage2.Data.TechnicalAssessment
{
    public class PaymentContext : DbContext
    {

        public PaymentContext(DbContextOptions<PaymentContext> options)
    : base(options)
        {
            
        }

        public DbSet<Payment> Payments { get; set; }

    }
}
