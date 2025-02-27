using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PayStack.Net;
using StudentGradeApp.DataContext;
using StudentGradeApp.Models;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.WebRequestMethods;


namespace StudentGradeApp.Repository
{
    public class PaystackService : IPaystackService
    {
        private readonly HttpClient _httpClient;
        private readonly string _paystackSecretKey;
        private readonly StudentContext _context;
        private PayStackApi paystackApi {  get; set; }
        public PaystackService(IConfiguration configuration, StudentContext context)
        {
            _httpClient = new HttpClient();
            _paystackSecretKey = configuration["Paystack:SecretKey"] ?? throw new Exception("Paystack Secret Key is missing");
            paystackApi = new PayStackApi(_paystackSecretKey);

           // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _paystackSecretKey);
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _context = context;
        }

        public async Task<TransactionInitializeResponse?> InitializePayment(PaystackPaymentModel payment)
        {
            //var url = "https://api.paystack.co/transaction/initialize";

            TransactionInitializeRequest req = new()
            {
                AmountInKobo = payment.Amount * 100,
                Email = payment.Email,
                Reference = Generate().ToString(),
                Currency = "NGN",
                CallbackUrl = "http://localhost:28540/payment/verify",
                 
            };

            TransactionInitializeResponse response = paystackApi.Transactions.Initialize(req);
            if(response.Status)
            {
                // save to db
                var save = new PaystackPayment
                {
                     Amount = payment.Amount,                     
                     Email = payment.Email,
                     TrxRef = req.Reference,
                     Name = payment.Name,
                     StudentNumber = payment.StudentNumber,
                     Remark = payment.Remark,
                };

                _context.PaystackPayments.Add(save);
                await _context.SaveChangesAsync();
            }

            return response;

        }

            
        public async Task<TransactionVerifyResponse?> VerifyPayment(string reference)
        {

            TransactionVerifyResponse response = paystackApi.Transactions.Verify(reference);
            if(response.Data.Status == "success")
            {
                var transaction = _context.PaystackPayments.Where(x=>x.TrxRef == reference).FirstOrDefault();
                if(transaction != null)
                {
                    transaction.Status = true;
                    _context.PaystackPayments.Update(transaction);
                  await  _context.SaveChangesAsync();

                    return response;
                }
            }
            else
            {
                response.Message = "an error has occur";
            }
         
            return response;
        }

        public async Task<List<TransactionResponseVM>> GetTransactions()
        {
            var result = new List<TransactionResponseVM>();
                var transaction = await _context.PaystackPayments.Where(x => x.Status == true).ToListAsync();
            if (transaction != null)
            {
                // map it into model 
                foreach(var itm in transaction)
                {
                    TransactionResponseVM res = new()
                    {
                        Amount = itm.Amount,
                        Currency = itm.Currency,
                        Email = itm.Email,
                        Name = itm.Name,
                        TransDate = itm.CreatedAt,
                        TransReference = itm.TrxRef,
                        StudentNumber = itm.StudentNumber,
                        Remark = itm.Remark,

                    };
                    result.Add(res);
                }
                
            }
            else { }

            return result;
        }

        public static long Generate()
        {
            return Random.Shared.NextInt64(1000000000, 9999999999); // 10-digit number
        }
    }
}
