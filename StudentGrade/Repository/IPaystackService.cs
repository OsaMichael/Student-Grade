using PayStack.Net;
using StudentGradeApp.DataContext;
using StudentGradeApp.Models;

namespace StudentGradeApp.Repository
{
    public interface IPaystackService
    {
        //public Task<PaystackResponse?> InitializePayment(PaystackPayment payment);
       // public Task<PaystackResponse?> VerifyPayment(string reference);
        public Task<TransactionVerifyResponse?> VerifyPayment(string reference);
        public Task<TransactionInitializeResponse?> InitializePayment(PaystackPaymentModel payment);
       // public Task<TransactionInitializeResponse?> InitializePayment(PaystackPayment payment);

    }
}
