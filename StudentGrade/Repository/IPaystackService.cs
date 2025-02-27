using PayStack.Net;
using StudentGradeApp.DataContext;
using StudentGradeApp.Models;

namespace StudentGradeApp.Repository
{
    public interface IPaystackService   
    {

        public Task<TransactionVerifyResponse?> VerifyPayment(string reference);
        public Task<TransactionInitializeResponse?> InitializePayment(PaystackPaymentModel payment);
        public Task<List<TransactionResponseVM>> GetTransactions();

    }
}
