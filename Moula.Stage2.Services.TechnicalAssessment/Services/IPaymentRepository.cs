using Moula.Stage2.Data.TechnicalAssessment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moula.Stage2.Services.TechnicalAssessment.Services
{
   public interface IPaymentRepository
    {
        public string AddPayment(double amount, DateTime date, bool sufficeBal);
        public string UpdatePayment(int id, string status, string reason);
        public IEnumerable<Payment> GetPayments();

    }
}
