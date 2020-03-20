using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moula.Stage2.Data.TechnicalAssessment;


namespace Moula.Stage2.Tests.TechnicalAssessment.Helper
{
    public static class SQLHelper
    {
        public static DbContextOptions<PaymentContext> GetInMemoryContext()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                var options = new DbContextOptionsBuilder<PaymentContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;


                // Create the schema in the database

                using (var context = new PaymentContext(options))
                {
                    context.Database.EnsureCreated();
                }

                return options;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
