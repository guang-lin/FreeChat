namespace FreeChat.Messenger.Server
{
    class ParseEntry
    {
        // 命令解析入口类
        public string Command { get; set; } = "";
        public string MethodName { get; set; } = "";

        public ParseEntry(string command, string methodName)
        {
            this.Command = command;
            this.MethodName = methodName;
        }
    }
}
