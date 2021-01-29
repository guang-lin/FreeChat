using FreeChat.Messenger;
using FreeChat.Messenger.Client;
using FreeChat.Messenger.FrontEnd.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FreeChat
{
    /// <summary>
    /// DialogueWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DialogueWindow : Window
    {
        private MessageClient client;
        private string dialogist = string.Empty; // 对话者用户名

        public DialogueWindow()
        {
            InitializeComponent();
        }

        public DialogueWindow(string dialogist)
        {
            InitializeComponent();
            this.dialogist = dialogist;
            RequestParse.SetDialogueWindow(this);
            Prepare();
            return;
        }

        public void Init()
        {
            try
            {
                // 创建 MessageClient 对象用来与服务器通信
                client = new MessageClient(User.ServerHost, User.ServerPort);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        // 向窗体添加消息气泡
        public void AddBubble(string message, ImageBrush headerBrush, bool isLeft)
        {
            ListBoxItem listBoxItem = new ListBoxItem();
            listBoxItem.MinHeight = 50;
            listBoxItem.Height = double.NaN;
            if (isLeft)
            {
                listBoxItem.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                listBoxItem.HorizontalAlignment = HorizontalAlignment.Right;
            }
            listBoxItem.SetValue(StyleProperty, Application.Current.Resources["ListBoxItemStyle1"]);

            DockPanel dockPanel = new DockPanel();

            Border header_border = new Border();
            if (isLeft)
            {
                DockPanel.SetDock(header_border, Dock.Left);
            }
            else
            {
                DockPanel.SetDock(header_border, Dock.Right);
            }
            header_border.Width = 50;
            header_border.Height = 50;
            if (isLeft)
            {
                header_border.Margin = new Thickness(10, 4, 4, 4);
            }
            else
            {
                header_border.Margin = new Thickness(4, 4, 10, 4);
            }
            header_border.Background = headerBrush;
            header_border.SetValue(StyleProperty, Application.Current.Resources["BorderStyle1"]);

            Border bubble_border = new Border();
            if (isLeft)
            {
                DockPanel.SetDock(bubble_border, Dock.Left);
            }
            else
            {
                DockPanel.SetDock(bubble_border, Dock.Right);
            }
            bubble_border.Width = double.NaN;
            bubble_border.Height = double.NaN;
            bubble_border.MaxWidth = 250;
            bubble_border.Margin = new Thickness(4, 12, 4, 4);
            SolidColorBrush brush = new SolidColorBrush();
            if (isLeft)
            {
                brush.Color = Color.FromArgb(0xFF, 0x9E, 0xD4, 0xB8);
            }
            else
            {
                brush.Color = Color.FromArgb(0xFF, 0x4B, 0xA1, 0xAB);
            }
            bubble_border.Background = brush;
            bubble_border.SetValue(StyleProperty, Application.Current.Resources["BorderStyle2"]);

            TextBlock textBlock = new TextBlock();
            textBlock.HorizontalAlignment = HorizontalAlignment.Stretch;
            textBlock.VerticalAlignment = VerticalAlignment.Stretch;
            textBlock.Width = double.NaN;
            textBlock.Margin = new Thickness(10, 10, 10, 10);
            textBlock.FontSize = 14;
            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.Text = message;

            bubble_border.Child = textBlock;
            dockPanel.Children.Add(header_border);
            dockPanel.Children.Add(bubble_border);
            listBoxItem.Content = dockPanel;

            dialogueList.Items.Add(listBoxItem);

            dialogueList.ScrollIntoView(listBoxItem);
        }

        public void AddBubble(string message, string username, bool isLeft)
        {
            AddBubble(message, GetImageBrush(username), isLeft);
        }

        // 发送消息
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string message = messageTextBox.Text;
            if (message.Length != 0)
            {
                messageTextBox.Text = "";
                if (client == null)
                {
                    Init();
                }

                string[] parameter = new string[3];
                if (dialogist.StartsWith("10")) // 判断对话是否为群聊
                {
                    parameter[0] = "11"; // 一对多发送的消息
                }
                else
                {
                    parameter[0] = "0"; // 单独发送的消息
                }
                parameter[1] = User.Username; // 发送者
                parameter[2] = dialogist; // 接收对象
                // 发送
                client.GetRequestSender().SendMessage(message, parameter);
                // 向窗体添加气泡
                ImageBrush brush = new ImageBrush();
                string header = AppDomain.CurrentDomain.BaseDirectory + Properties.Settings.Default.HeaderDirectory + User.Header;
                Uri uri = new Uri(header, UriKind.Absolute);
                brush.ImageSource = new BitmapImage(uri);
                AddBubble(message, brush, false);
            }
        }

        // 当用户是范祺琪和李广林时为聊天窗口做特殊准备
        private void Prepare()
        {
            string lgl = "980728";
            string fqq = "980510";
            if (User.Username.Equals(lgl))
            {
                if (!dialogist.Equals(fqq))
                {
                    // 移除所有测试对话
                    while (dialogueList.Items.Count > 0)
                    {
                        dialogueList.Items.RemoveAt(0);
                    }
                }
            }
            else if (User.Username.Equals(fqq))
            {
                // 移除所有测试对话
                while (dialogueList.Items.Count > 0)
                {
                    dialogueList.Items.RemoveAt(0);
                }
                if (dialogist.Equals(lgl))
                {
                    ImageBrush lgl_header = GetImageBrush(lgl);
                    ImageBrush fqq_header = GetImageBrush(fqq);
                    AddBubble("你好啊，我是范祺琪，饭饭饭饭饭饭饭饭饭饭饭饭饭饭饭", fqq_header, false);
                    AddBubble("我懒，我很懒", fqq_header, false);
                    AddBubble("我啥也不会", fqq_header, false);
                    AddBubble("懒吧", lgl_header, true);
                }
            }
            else
            {
                // 移除所有测试对话
                while (dialogueList.Items.Count > 0)
                {
                    dialogueList.Items.RemoveAt(0);
                }
            }
        }

        public ImageBrush GetImageBrush(string username)
        {
            ImageBrush brush = new ImageBrush();
            string headerName = "";
            if (username.Equals(User.Username))
            {
                headerName = User.Header;
            }
            else
            {
                headerName = ContactManager.GetContact(username).Header;
            }
            Uri uri = new Uri(Properties.Settings.Default.HeaderDirectory + headerName, UriKind.Relative);
            brush.ImageSource = new BitmapImage(uri);
            return brush;
        }
    }
}
