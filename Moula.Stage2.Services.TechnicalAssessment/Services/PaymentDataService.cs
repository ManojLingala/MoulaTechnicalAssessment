using System;
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using Moula.Stage2.Data.TechnicalAssessment;
using Moula.Stage2.Data.TechnicalAssessment.Data.Entities;
using Moula.Stage2.Data.TechnicalAssessment.Helper;
using Z.EntityFramework.Extensions;

namespace Moula.Stage2.Services.TechnicalAssessment.Services
{
    public class PaymentDataService : IPaymentRepository
    {
        private readonly PaymentContext _paymentContext;

        public PaymentDataService(PaymentContext paymentContext )
        {
            _paymentContext = paymentContext;
          
        }

        public string AddPayment(double amount, DateTime date, bool suffBal)
        {
            if (!suffBal)
            {
                return CreatePayment(nameof(Helper.Closed), nameof(Helper.NotEnoughFunds), amount, date);
            }
            else
            {
                return CreatePayment(nameof(Helper.Pending), string.Empty, amount, date);
            }
        }


        public string UpdatePayment(int id, string status)
        {

            List<Payment> payment = _paymentContext.Payments.Where(c => c.ID == id).ToList();
            foreach (Payment paymt in payment)
            {
                paymt.Status = status;
            }

            _paymentContext.SaveChanges();

            return "Success";

        }

        public IEnumerable<Payment> GetPayments()
        {
            return _paymentContext.Payments.Select(s => new Payment { ID = s.ID, Date = s.Date, Status = s.Status, Amount = s.Amount, Reason = s.Reason }).OrderByDescending(o => o.Date);
        }
        protected string CreatePayment(string status, string reason, double amount, DateTime date)
        {
            var payment = new Payment()
            {
                Amount = amount,
                Status = status,
                Reason = reason,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Date = date
            };
            _paymentContext.Payments.Add(payment);
            _paymentContext.SaveChanges();
            return "Success";
        }
    }
}
