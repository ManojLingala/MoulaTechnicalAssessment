using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moula.Stage2.Data.TechnicalAssessment;
using Moula.Stage2.Data.TechnicalAssessment.Data.Entities;
using Moula.Stage2.Services.TechnicalAssessment.Services;
using System;
using Xunit;
using System.Threading.Tasks;
using Moula.Stage2.Tests.TechnicalAssessment.Helper;
using System.Linq;

namespace Moula.Stage2.Tests.TechnicalAssessment
{
    /// <summary>
    /// 
    /// AS I'm using Inmemory database not creating any mock objects
    /// 
    /// </summary>
    public class MoulaTests
    {
        [Fact]
        public async Task CreatePaymentAsync()
        {
           var options= SQLHelper.GetInMemoryContext();

            // Run the test against one instance of the context
            using (var context = new PaymentContext(options))
            {
                var service = new PaymentDataService(context);
                var payment = new Payment()
                {
                    Amount = 10,
                    Status = "Pending",
                    Date = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
                service.AddPayment(payment.Amount, payment.Date,true);
                context.SaveChanges();

                // we can also use a separate instance of the context to verify correct data was saved to database
                Assert.Equal(1, await context.Payments.CountAsync());
            }

        }

        [Theory]
        [InlineData(1, "Cancelled")]
        [InlineData(1, "Processed")]
        public void UpdatePayment(int Id, string Status)
        {
            var options = SQLHelper.GetInMemoryContext();

 
            using (var context = new PaymentContext(options))
            {
                var service = new PaymentDataService(context);
                var payment = new Payment()
                {
                    Amount = 10,
                    Status = "Pending",
                    Reason = string.Empty, //Can be optional
                    Date = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
                service.AddPayment(payment.Amount, payment.Date, true);
                service.UpdatePayment(Id, Status, payment.Reason);
            }

            using (var context = new PaymentContext(options))
            {
                var data = context.Payments.Where(w => w.ID == Id).Select(s => s.Status);
                Assert.Equal(Status, context.Payments.Where(w => w.ID == Id).Select(s => s.Status).FirstOrDefault());
            }
        }

        [Fact]
        public void  NotSufficientFund()
        {
            var options = SQLHelper.GetInMemoryContext();

            // Run the test against one instance of the context
            using (var context = new PaymentContext(options))
            {
                var service = new PaymentDataService(context);

                service.AddPayment(20, DateTime.UtcNow, false);
                context.SaveChanges();

                Assert.Equal("NotEnoughFunds", context.Payments.Where(w => w.ID == 1).Select(s => s.Reason).FirstOrDefault());
            }

     
        }
    }
}
