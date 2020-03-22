using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moula.Stage2.Data.TechnicalAssessment.Data.Entities;
using Moula.Stage2.Services.TechnicalAssessment.Services;
using Moula.Stage2.TechnicalAssessment.Requests;
using System;
using System.Collections.Generic;

namespace Moula.Stage2.TechnicalAssessment.Controllers
{
    /// <summary>
    /// The below versioning is commented to accomodate the future API Changes.
    /// </summary>
    //[Route("api/v{version:apiVersion}/paymw=ent")]
    //[ApiVersion("2.0")]
    [Route("api/[Controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }


        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public string Post([FromBody] PaymentCreateRequest payReq)
        {
            try
            {
                if (ModelState.IsValid)
                    return _paymentService.CreatePayment(payReq.Amount, payReq.Date, payReq.SufficientBalance);
                else
                    return "";
            }
          
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new order: {ex}");
                return ex.Message;
            }
        }

        [HttpGet]
        public IEnumerable<Payment> GetPayments()
        {
            return _paymentService.GetPayments();
        }
        // PUT: api/Payment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PaymentUpdateRequest payReq)
        {
            try
            {
                _paymentService.UpdatePayment(payReq.Id, payReq.Status, payReq.Reason);
            }
            catch(Exception ex)
            {
               _logger.LogError($"Failed to update a payment: {ex}");
            }
            
        }
    }
}
