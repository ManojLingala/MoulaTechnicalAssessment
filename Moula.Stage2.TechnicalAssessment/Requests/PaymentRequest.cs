using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.Stage2.TechnicalAssessment.Requests
{

    public class PayementCreateRequest
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public Boolean SufficientBalance { get; set; }
    }
    public class PaymentUpdateRequest
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
   
}
