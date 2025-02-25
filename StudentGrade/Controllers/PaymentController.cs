using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGradeApp.DataContext;
using StudentGradeApp.Models;
using StudentGradeApp.Repository;

namespace StudentGradeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IStudentGradeRepository _repository;
        private readonly IPaystackService _paystackService;
        private readonly ILogger<CourseController> _logger;
        public PaymentController(IStudentGradeRepository repository, ILogger<CourseController> logger, IPaystackService paystackService)
        {
            _repository = repository;
            _logger = logger;
            _paystackService = paystackService;
        }

        // POST: api/Payment/process
        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentModel paymentDto)
        {
            if (paymentDto == null || string.IsNullOrEmpty(paymentDto.StudentNumber))
            {
                return BadRequest("Invalid payment data.");
            }
            var result = await _repository.Payment(paymentDto);
            if (result.message == "Successful")
            {
                return Ok(result);
            }
            else
            {
                return UnprocessableEntity(new { result.message });
            }
        }

        [HttpGet("history/{studentNumber}")]
        public async Task<IActionResult> GetPaymentHistory(string studentNumber)
        {
            var history = await _repository.GetPaymentHistoryByStudentNumber(studentNumber);
            return Ok(history);
        }

        [HttpGet("AllPayments")]
        public async Task<IActionResult> GetAllPayments()
        {
            var result = await _repository.GetAllPayments();

            return Ok(result);

        }
        /// <summary>
        ///  paystack
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPost("initialize")]
        public async Task<IActionResult> InitializePayment([FromBody] PaystackPaymentModel payment)
        {
            if (payment == null || payment.Amount <= 0)
                return BadRequest("Invalid payment details.");

            var response = await _paystackService.InitializePayment(payment);
            if (response != null && response.Status)
            {
                return Ok(new { message = "Payment Initialized", url = response.Data.AuthorizationUrl, reference = response.Data.Reference });
            }

            return BadRequest("Payment initialization failed.");
        }

        [HttpGet("verify/{reference}")]
        public async Task<IActionResult> VerifyPayment(string reference)
        {
            var response = await _paystackService.VerifyPayment(reference);
            if (response != null && response.Status)
            {
                return Ok(new { message = "Payment Verified", response });
            }

            return BadRequest("Payment verification failed.");
        }
    }
}
