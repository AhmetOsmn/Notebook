namespace LightMessagingCore.Common
{
    public static class MqConstants
    {
        public const string RmqUri = "rabbitmq://localhost/";
        public const string RmqUserName = "guest";
        public const string RmqPassword = "guest";
        public const string OrderQueueName = "lightmessagingcore.order";
    }
}