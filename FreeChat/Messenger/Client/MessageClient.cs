namespace FreeChat.Messenger.Client
{
    class MessageClient
    {
        private string host = "127.0.0.1";
        private int port = 8888;
        private RequestSender requestSender;

        public MessageClient(string host, int port)
        {
            this.host = host;
            this.port = port;
            requestSender = new RequestSender(host, port);
        }

        public string Host
        {
            get { return host; }
            private set { }
        }

        public int Port
        {
            get { return port; }
            private set { }
        }

        public RequestSender GetRequestSender()
        {
            return requestSender;
        }
    }
}
