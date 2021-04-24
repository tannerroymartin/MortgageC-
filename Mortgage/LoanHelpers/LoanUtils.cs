using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mortgage.LoanHelpers
{
    public class LoanUtils
    {
        /// <summary>
        /// Calc an interest payment
        /// </summary>
        /// <param name="Amount"></param>
        /// <param name="Rate"></param>
        /// <param name="Term"></param>
        /// <returns></returns>
        public decimal CalcPayment(decimal Amount, decimal Rate, int Term)
        {
            //var Numerator = Amount * Convert.ToDecimal(Rate);
            //var Demominator = Convert.ToDecimal((1 - Math.Pow(1 + Rate, -Term)));
            //var Payment = Numerator / Demominator;
            //return Payment;
            var RateDouble = Convert.ToDouble(Rate/1200);
            var AmountDouble = Convert.ToDouble(Amount);
            var PaymentDouble = (AmountDouble * RateDouble) / (1 - Math.Pow(1 + RateDouble, -Term));
            
            return Convert.ToDecimal(PaymentDouble);
        }

        public decimal CalcMonthlyInterest(decimal Balance, decimal Rate)
        {
            return Balance * (Rate / 1200);
        }

       
        

        

     





    }
}
