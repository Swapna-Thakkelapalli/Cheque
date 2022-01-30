using System;

namespace ChequeAPI.Models
{
    public class ChequeDTO
    {
        public string PayeeName { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string Date { get; set; }
    }
}
