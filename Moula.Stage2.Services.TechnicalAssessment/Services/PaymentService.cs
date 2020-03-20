using System;
using System.Collections.Generic;
using System.Text;
using Moula.Stage2.Data.TechnicalAssessment.Data.Entities;

namespace Moula.Stage2.Services.TechnicalAssessment.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentDataService;

        public PaymentService(IPaymentRepository paymentDataService)
        {
            _paymentDataService = paymentDataService;
        }
     

        public string CreatePayment(double amount, DateTime date, bool sufficientBal)
        {
            return _paymentDataService.AddPayment(amount, date, sufficientBal);
        }

        public IEnumerable<Payment> GetPayments()
        {
            return _paymentDataService.GetPayments();
        }

        public string UpdatePayment(int id, string status)
        {
            return _paymentDataService.UpdatePayment(id, status);
        }
       
    }
}
