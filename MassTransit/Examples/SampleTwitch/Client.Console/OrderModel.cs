﻿namespace Client.Console
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public string CustomerNumber { get; set; }
        public string PaymentCardNumber { get; set; }
        public string Notes { get; set; }
    }
}