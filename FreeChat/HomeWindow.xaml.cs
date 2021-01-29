using FreeChat.Messenger.Client;
using FreeChat.Messenger.FrontEnd.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FreeChat
{
    /// <summary>
    /// MessageHomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageHomeWindow : Window
    {
        MainWindow mainWindow;

        public MessageHomeWindow()
        {
            InitializeComponent();
        }

        public MessageHomeWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.mainWindow = mainWindow;

            // 设置头像
            ImageBrush brush = new ImageBrush();
            Uri uri = new Uri(Properties.Settings.Default.HeaderDirectory + "/" + User.Header, UriKind.Relative);
            brush.ImageSource = new BitmapImage(uri);
            topHeaderBorder.Background = brush;
            // 设置昵称
            nickNameLabel.Content = User.Nickname;
            
            homeListBox.Items.Remove(homeListBox.FindName("_"+User.Username));
            homeListBox.FindName("_" + User.Username);
            // 为消息列表添加事件
            AddEventForAll();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
            MessageClient client;
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
            client.GetRequestSender().Logout(User.Username);
            mainWindow.Show();
        }

        // 消息列表点击事件
        private void HomeItemsMouseLButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label label = sender as Label;
            string dialogist = ((ListBoxItem)label.Parent).Name.Substring(1);
            DialogueWindow dialogueWindow = new DialogueWindow(dialogist);
            dialogueWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            dialogueWindow.ShowActivated = false;
            if (dialogist.StartsWith("10"))
            {
                dialogueWindow.Title = "群聊";
            }
            else
            {
                Contact contact = ContactManager.GetContact(dialogist);
                if (contact != null)
                {
                    dialogueWindow.Title = contact.Remark;
                }
            }
            dialogueWindow.Show();
        }

        // 为消息列表中的每一项添加事件处理器
        public void AddEventForAll()
        {
            foreach(ListBoxItem item in homeListBox.Items)
            {
                Label label = (Label)item.Content;
                label.AddHandler(Label.MouseDownEvent,new MouseButtonEventHandler(HomeItemsMouseLButtonDown), true);
            }
        }
    }
}
