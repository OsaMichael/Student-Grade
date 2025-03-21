using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGradeApp.DataContext;
using StudentGradeApp.Models;
using StudentGradeApp.Repository;
using System.Text.Json;

namespace StudentGradeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IStudentGradeRepository _repository;
        private readonly IPaystackService _paystackService;
        private readonly ILogger<CourseController> _logger;
        private readonly string _paystackSecretKey;
        public PaymentController(IStudentGradeRepository repository, ILogger<CourseController> logger, IPaystackService paystackService, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _paystackService = paystackService;
            _paystackSecretKey = configuration["Paystack:SecretKey"];
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

        [HttpGet("getTransactions")]
        public async Task<IActionResult> GetTransactions()
        {
            var response = await _paystackService.GetTransactions();
            if (response != null)
            {
                return Ok(response );
            }

            return BadRequest(" status failed.");
        }

        [HttpPost]
        public async Task<IActionResult> HandleWebhook1()
        {
            try
            {
                using var reader = new StreamReader(Request.Body);
                var requestBody = await reader.ReadToEndAsync();
                _logger.LogInformation("Received Paystack Webhook: {RequestBody}", requestBody);

                // Verify signature
                var paystackSignature = Request.Headers["x-paystack-signature"].FirstOrDefault();
                var secretKey = _paystackSecretKey; // Store securely in appsettings.json

                using (var hmac = new System.Security.Cryptography.HMACSHA512(System.Text.Encoding.UTF8.GetBytes(secretKey)))
                {
                    var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(requestBody));
                    var hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();

                    if (hashString != paystackSignature)
                    {
                        _logger.LogWarning("Invalid Paystack Signature");
                        return Unauthorized("Invalid signature");
                    }
                }

                // Deserialize JSON
                var eventData = JsonSerializer.Deserialize<PaystackWebhookEvent>(requestBody);
                if (eventData == null)
                {
                    return BadRequest("Invalid webhook data");
                }

                // Handle different event types
                switch (eventData.@event)
                {
                    case "charge.success":
                        _logger.LogInformation("Payment Successful: {TransactionReference}", eventData.data.reference);
                        break;
                    case "transfer.success":
                        _logger.LogInformation("Transfer Successful: {TransferReference}", eventData.data.reference);
                        break;
                    default:
                        _logger.LogWarning("Unhandled Paystack event: {EventType}", eventData.@event);
                        break;
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling Paystack Webhook");
                return StatusCode(500, "Internal server error");
            }
        }


            [HttpPost("webhook/paystack")]
            public async Task<IActionResult> HandleWebhook()
            {
                try
                {
                    using var reader = new StreamReader(Request.Body);
                    var requestBody = await reader.ReadToEndAsync();
                    _logger.LogInformation("Received Paystack Webhook: {RequestBody}", requestBody);

                    var eventData = JsonSerializer.Deserialize<PaystackWebhookEvent>(requestBody);
                    if (eventData == null) return BadRequest("Invalid webhook data");

                    if (eventData.@event == "charge.success")
                    {
                        _logger.LogInformation("Payment Successful: {TransactionReference}", eventData.data.reference);

                        // TODO: Update database payment status

                        return Ok();
                    }

                    return BadRequest("Unhandled Paystack event");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error handling Paystack Webhook");
                    return StatusCode(500, "Internal server error");
                }
            }

    }
}
