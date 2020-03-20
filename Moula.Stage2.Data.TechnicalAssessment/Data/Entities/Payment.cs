using System;
using System.Collections.Generic;
using System.Text;

namespace Moula.Stage2.Data.TechnicalAssessment.Data.Entities
{
    public class Payment
    {
        public int ID { get; set; }
        public double Amount { get; set; }

        //Processed Date 
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
