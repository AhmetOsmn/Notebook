namespace Sample.Contracts
{
    public static class RmqConstants
    {
        public const string QueueName = "submit-order";
        public const string UserName = "guest";
        public const string Password = "guest";
        private static int MessageCount = 1;

        public static void AddOneToCounter()
        {
            MessageCount++;
        }

        public static int GetMessageCount()
        {
            return MessageCount;
        }
    }
}
