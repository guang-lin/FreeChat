using FreeChat.Messenger.Server;
using System.Text.RegularExpressions;
using System.Windows;

namespace FreeChat.Messenger
{
    // 该类用于解析消息转发服务器发送的请求
    class RequestParse
    {
        private RequestHandler handler;
        private string request;
        private string[] parameter;
        private static DialogueWindow dialogueWindow;
        private delegate void DelegateUIMethod(string message, string sender, bool isLeft);

        public RequestParse(RequestHandler handler)
        {
            this.handler = handler;
        }

        private void UIMethod(string message, string sender, bool isLeft)
        {
            if (dialogueWindow != null)
            {
                dialogueWindow.AddBubble(message, sender, isLeft);
            }
        }

        // 建立方法入口表
        private ParseEntry[] entrys = {
            new ParseEntry("TYPE", "ReceiveMessage")
            };

        // 解析服务器请求并决定调用指定的方法
        public void Parse(string request)
        {
            if (IsMatch(request)) // 格式是否匹配
            {
                this.request = request;
                parameter = NetUtility.Split(request);
            }
            else
            {
                return;
            }
            // 在此根据入口命令决定调用哪个方法
            int i = 0;
            for (; i < entrys.Length; i++)
            {
                if (entrys[i].Command.Equals(parameter[0]))
                {
                    // 调用
                    GetType().GetMethod(entrys[i].MethodName).Invoke(this, null);
                    break;
                }
            }
            if (i == entrys.Length)
            {
                // Console.WriteLine("没有找到合适的入口");
            }
        }

        // 确定请求内容是否符合正确格式
        private bool IsMatch(string request)
        {
            string pattern1 = "[A-Za-z]+";
            string pattern2 = "[A-Za-z]+ +.*";
            bool match1 = Regex.IsMatch(request, pattern1);
            bool match2 = Regex.IsMatch(request, pattern2);
            if (match1 || match2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // 设置要操作的消息窗体
        public static void SetDialogueWindow(DialogueWindow dialogueWindow)
        {
            RequestParse.dialogueWindow = dialogueWindow;
        }

        // 接收服务器转发的消息（被动）
        public void ReceiveMessage()
        {
            // Console.WriteLine(parameter[0]);
            if (parameter[0].Equals("TYPE"))
            {
                if (parameter[1].Equals("0")) // 消息类型
                {
                    // 一对一发送
                    string sender = parameter[2];
                    handler.ReturnResponse("331");
                    request = handler.GetRequest();
                    parameter = NetUtility.Split(request);
                    if (parameter[0].Equals("MSG") && parameter.Length == 2)
                    {
                        // 将消息转发到指定主机
                        handler.ReturnResponse("220");
                        // 添加消息气泡
                        Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new DelegateUIMethod(UIMethod), parameter[1], sender, true);
                    }
                    else
                    {
                        handler.ReturnResponse("420");
                    }
                }
                else if (parameter[1].Equals("11"))
                {
                    string sender = parameter[2];
                    handler.ReturnResponse("331");
                    request = handler.GetRequest();
                    parameter = NetUtility.Split(request);
                    if (parameter[0].Equals("MSG") && parameter.Length == 2)
                    {
                        // 将消息转发到指定主机
                        handler.ReturnResponse("220");
                        // 添加消息气泡
                        Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new DelegateUIMethod(UIMethod), parameter[1], sender, true);
                    }
                    else
                    {
                        handler.ReturnResponse("420");
                    }
                }
            }
        }
    }
}
