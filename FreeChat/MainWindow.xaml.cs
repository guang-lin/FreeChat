using FreeChat.Messenger;
using FreeChat.Messenger.Client;
using FreeChat.Messenger.FrontEnd.Data;
using FreeChat.Messenger.Server;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FreeChat
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MessageClient client;
        private MessageServer server;
        Thread listenerThread;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            string lastUser = Properties.Settings.Default.User;
            string lastPassword = Properties.Settings.Default.Password;
            usernameTextBox.Text = lastUser;
            passwordBox.Password = lastPassword;
            User.Init();

            // 设置登录窗体头像
            string lastHeader = Properties.Settings.Default.Header;
            string header = Properties.Settings.Default.HeaderDirectory + lastHeader;
            // AppDomain.CurrentDomain.BaseDirectory
            if (lastHeader.Length != 0)
            {
                ImageBrush brush = new ImageBrush();
                Uri uri = new Uri(header, UriKind.Relative);
                try
                {
                    brush.ImageSource = new BitmapImage(uri);
                }
                catch(FileNotFoundException)
                {
                    brush.ImageSource = null;
                }
                loginHeader.Background = brush;
            }
        }

        // 登录
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordBox.Password;

            try
            {
                // 创建 MessageClient 对象，用来与服务器通信
                client = new MessageClient(User.ServerHost, User.ServerPort);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (username.Length != 0 && password.Length != 0)
            {
                int code = client.GetRequestSender().Login(username, password);
                if(code == 531)
                {
                    MessageBox.Show("用户名不存在");
                }
                else
                {
                    if (code == 220) // 登录成功，然后提交我的主机地址和端口，用于被动接收消息
                    {
                        // 设置和保存本次登录的用户名和密码
                        User.Username = username;
                        User.Password = password;
                        Properties.Settings.Default.User = username;
                        Properties.Settings.Default.Password = password;
                        Properties.Settings.Default.Save();

                        // 创建消息监听对象,用来监听服务器主动向我发送的请求
                        server = new MessageServer();

                        // 获取创建的监听套接字的地址和端口
                        User.Host = server.GetHost();
                        User.Port = server.GetPort();

                        // 在独立的线程运行消息监听服务
                        listenerThread = new Thread(delegate () { server.Listen(); /* 开始监听 */ });
                        listenerThread.IsBackground = true;
                        listenerThread.Start(); // 启动监听线程

                        // 提交我的主机地址和端口，使服务器可以主动地向我发送消息
                        code = client.GetRequestSender().SendPort(User.Username, User.Host, User.Port);

                        if (code == 220)
                        {
                            // MessageBox.Show("提交成功");
                        }
                        else
                        {
                            MessageBox.Show("监听端口提交失败");
                            return;
                        }

                        // 从服务器获取自己的基本信息
                        string[] para = null; // 命令参数数组
                        // 发送请求
                        code = client.GetRequestSender().UserInfo(username, "SELF");
                        if (code == 220)
                        {
                            string info = client.GetRequestSender().ReadExtras();
                            para = NetUtility.Split(info); // 拆分返回内容
                        }
                        else
                        {
                            MessageBox.Show("登录出错");
                            return;
                        }
                        if(para != null)
                        {
                            User.Username = para[0];
                            User.Nickname = para[1];
                            User.Remark = "";
                            User.Header = para[2];
                        }

                        // 获取其它用户基本信息
                        ContactManager.Init(client);
                        MessageHomeWindow messageHome = new MessageHomeWindow(this);
                        messageHome.Owner = this;
                        Hide();
                        messageHome.Show(); // 进入消息主窗体
                    }
                    else if(code != 420)
                    {
                        MessageBox.Show("密码错误");
                    }
                    else
                    {
                        MessageBox.Show("登录出错");
                    }
                }
            }
        }

        // 设置服务器主机地址和端口
        private void Label_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HostSettingsWindow window = new HostSettingsWindow();
            window.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //
        }
    }
}
