namespace motorcycle.shared.CreationalBase
{
    public class BaseError : Exception
    {
        public MessageType Type { get; private set; }
        public string MessageDescription { get; private set; } = "";

        public BaseError AddMessage(string message)
        {
            MessageDescription = message;
            return this;
        }

        public BaseError AddErrorType(MessageType messageType)
        {
            Type = messageType;
            Source = Type.ToString();
            return this;
        }


        public static BaseError Create(MessageType messageType, string message) => 
            new BaseError()
                    .AddErrorType(messageType)
                    .AddMessage(message);
    }
}
