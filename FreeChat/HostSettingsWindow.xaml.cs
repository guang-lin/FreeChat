using FreeChat.Messenger;
using FreeChat.Messenger.FrontEnd.Data;
using System;
using System.Windows;

namespace FreeChat
{
    /// <summary>
    /// HostSettingsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HostSettingsWindow : Window
    {
        public HostSettingsWindow()
        {
            InitializeComponent();
            // 读取已有设置
            string address = Properties.Settings.Default.ServerHost;
            string port = Convert.ToString(Properties.Settings.Default.ServerPort);
            ipTextBox.Text = address;
            portTextBox.Text = port;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string address = ipTextBox.Text.Trim();
            string port = portTextBox.Text.Trim();

            if(address.Length != 0 || port.Length != 0)
            {
                if (NetUtility.IsAddress(address))
                {
                    Properties.Settings.Default.ServerHost = address;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("地址格式错误");
                    return;
                }

                if (port.Length != 0)
                {
                    if (NetUtility.IsPort(port))
                    {
                        Properties.Settings.Default.ServerPort = Convert.ToInt32(port);
                        Properties.Settings.Default.Save();
                        User.UpdateSocketAddress();
                    }
                    else
                    {
                        MessageBox.Show("端口格式或范围错误");
                        return;
                    }
                }
                Close();
            }
            else
            {
                return;
            }
        }
    }
}
