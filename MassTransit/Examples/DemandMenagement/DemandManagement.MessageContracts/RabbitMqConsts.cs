namespace DemandManagement.MessageContracts
{
    public static class RabbitMqConsts
    {
        public const string RabbitMqUri = "rabbitmq://localhost/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string RegisterDemandServiceQueue = "registerdemand.service";
        public const string NotificationServiceQueue = "notification.service";
        public const string ThirdPartyServiceQueue = "thirdparty.service";
    }
}
