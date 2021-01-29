namespace FreeChat.Messenger.FrontEnd.Data
{
    class User
    {
        private static string header = "";
        private static string serverHost = "";
        private static int serverPort = 0;

        public static void Init()
        {
            ServerHost = Properties.Settings.Default.ServerHost;
            ServerPort = Properties.Settings.Default.ServerPort;
        }

        public static void UpdateSocketAddress()
        {
            ServerHost = Properties.Settings.Default.ServerHost;
            ServerPort = Properties.Settings.Default.ServerPort;
        }

        public static string Username { get; set; } = "";
        public static string Password { get; set; } = "";
        public static string Nickname { get; set; } = "";
        public static string Remark { get; set; } = "";

        public static string Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
                Properties.Settings.Default.Header = value;
                Properties.Settings.Default.Save();
            }
        }

        public static string Host { get; set; } = "";
        public static int Port { get; set; } = 0;

        public static string ServerHost
        {
            get
            {
                return serverHost;
            }
            set
            {
                serverHost = value;
                Properties.Settings.Default.ServerHost = value;
                Properties.Settings.Default.Save();
            }
        }

        public static int ServerPort
        {
            get
            {
                return serverPort;
            }
            set
            {
                serverPort = value;
                Properties.Settings.Default.ServerPort = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
