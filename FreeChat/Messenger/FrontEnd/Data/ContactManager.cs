using FreeChat.Messenger.Client;
using System.Collections.Generic;

namespace FreeChat.Messenger.FrontEnd.Data
{
    class ContactManager
    {
        private static int maxContactCount = 100;
        private static Dictionary<string, Contact> contacts = new Dictionary<string, Contact>();

        public static int ContactCount
        {
            get { return contacts.Count; }
            private set { }
        }

        // 从服务器获取所有联系人基本信息，并将这些信息存放在 contacts
        public static void Init(MessageClient client)
        {
            contacts.Clear();
            int status = client.GetRequestSender().UserInfo(User.Username, "ALL");
            if (status == 220)
            {
                string info = client.GetRequestSender().ReadExtras();
                string[] lines = info.Trim().Split('\n');
                foreach (string line in lines)
                {
                    string[] item = NetUtility.Split(line);
                    if (item != null)
                    {
                        Contact contact = new Contact();
                        contact.Username = item[0];
                        contact.Nickname = item[1];
                        contact.Remark = item[2];
                        contact.Header = item[3];
                        contacts.Add(contact.Username, contact);
                    }
                }
            }
        }

        public static int MaxContactCount
        {
            get { return contacts.Count; }
            set { maxContactCount = value; }
        }

        public static Contact GetContact(string username)
        {
            Contact contact = null;
            contacts.TryGetValue(username, out contact);
            return contact;
        }

        public static bool SetContactProperty(string username, string property, string value)
        {
            Contact Contact = GetContact(username);
            if (Contact != null)
            {
                switch (property)
                {
                    case "Username":
                        Contact.Username = value;
                        return true;
                    case "Nickname":
                        Contact.Nickname = value;
                        return true;
                    case "Remark":
                        Contact.Remark = value;
                        return true;
                    case "Header":
                        Contact.Header = value;
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool Contains(string username)
        {
            if (GetContact(username) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
