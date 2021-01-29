using System;
using System.Net.Sockets;
using System.Text;

namespace FreeChat.Messenger.Server
{
    class RequestHandler
    {
        private TcpClient socket; // Client Socket
        private RequestParse parse;

        public RequestHandler(TcpClient socket)
        {
            this.socket = socket;
            Run();

        }

        public void Run()
        {
            string request;
            request = GetRequest();
            parse = new RequestParse(this);
            parse.Parse(request);

        }

        public string GetRequest()
        {
            if (socket != null)
            {
                StringBuilder request = new StringBuilder();
                byte[] bytes = new byte[256];
                NetworkStream stream = socket.GetStream();
                // 读入所有数据
                int i;
                if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // 将数据转换为UFT-8格式字符串
                    request = request.Append(Encoding.UTF8.GetString(bytes, 0, i));
                }
                return request.ToString();
            }
            else
            {
                Console.WriteLine("没有收到用户命令");
                return null;
            }
        }

        // 返回响应消息
        public void ReturnResponse(string response)
        {
            NetworkStream stream = socket.GetStream();
            byte[] msg = Encoding.UTF8.GetBytes(response);
            stream.Write(msg, 0, msg.Length);
            Console.WriteLine("响应: {0}", response);
        }
    }
}
