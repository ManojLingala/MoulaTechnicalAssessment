using Moula.Stage2.Data.TechnicalAssessment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moula.Stage2.Services.TechnicalAssessment.Services
{
    public interface IPaymentService
    {
        string CreatePayment(double amount, DateTime date, bool sufficientBal);
        string UpdatePayment(int id, string status, string reason);
        IEnumerable<Payment> GetPayments();
    }
}
