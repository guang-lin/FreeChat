using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FreeChat.Messenger.Server
{
    class RequestListener
    {
        private TcpListener serverSocket;
        private int workerThreads = Environment.ProcessorCount; // 线程池中辅助线程的最大数目
        private int completionPortThreads = Environment.ProcessorCount; // 线程池中异步 I/O 线程的最大数目

        public RequestListener(string host, int port)
        {
            serverSocket = new TcpListener(IPAddress.Parse(host), port);
            ThreadPool.SetMaxThreads(workerThreads, completionPortThreads);
        }

        public void Listen()
        {
            // MessageBox.Show("用户开始监听："+host + ":" + port);
            // 开始监听服务器请求
            serverSocket.Start();
            // 进入监听闭环
            while (true)
            {
                TcpClient client = serverSocket.AcceptTcpClient();
                // 将方法排入队列以执行。该方法在线程池中有可用线程时执行。
                ThreadPool.QueueUserWorkItem(arg => Executor(client), null);
            }
        }

        private void Executor(TcpClient clientSocket)
        {
            new RequestHandler(clientSocket);
        }

        // 停止监听连接请求
        public void Close()
        {
            serverSocket.Stop();
        }
    }
}
