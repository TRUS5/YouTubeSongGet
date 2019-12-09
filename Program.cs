using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Automation;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace MilashkaChannel
{
    class Program
    {
        static void Main(string[] args)
        {


            DialogResult dialogResult2 = MessageBox.Show("Вы хотите отправить TRUS5 свой ip-адрес?", "", MessageBoxButtons.YesNo);
            if (dialogResult2 == DialogResult.Yes)
            {
                string to = "ipstealervor@yandex.ru";
                string from = "ipstealervor@yandex.ru";
                var temptext = "";
                String strHostName = string.Empty;
                strHostName = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                IPAddress[] addr = ipEntry.AddressList;
                for (int i = 0; i < addr.Length; i++)
                {
                    temptext += addr[i].ToString() + "\n";
                }
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("ipstealervor@yandex.ru"); // Адрес отправителя
                mail.To.Add(new MailAddress("trus888@gmail.com")); // Адрес получателя
                mail.Subject = "KEK";
                mail.Body = temptext;

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.yandex.ru";
                client.Port = 587; // Обратите внимание что порт 587
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("ipstealervor@yandex.ru", "QQqq1122"); // Ваши логин и пароль
                client.Send(mail);
            }
            else if (dialogResult2 == DialogResult.No)
            {
              
            }


            if (!File.Exists("Song.txt"))
            {
                using (FileStream fs = File.Create("Song.txt"))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("Тут будет песня Коляна.");
                    fs.Write(info, 0, info.Length);
                }
            }

            DialogResult dialogResult = MessageBox.Show("У ВАС СУПЕР БРАУЗЕР ДЛЯ ГЕЙМЕРОВ?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                OperaGXCheck();
            }
            else if (dialogResult == DialogResult.No)
            {
                ChromeCheck();
            }


    

        }

        public static void OperaGXCheck()
        {
            var cursongname = "Песни не играет";
            Process[] procsChrome = Process.GetProcessesByName("opera");
            if (procsChrome.Length <= 0)
            {
                cursongname = "Браузер не открыт";
            }


            foreach (Process proc in procsChrome)
            {
                // the chrome process must have a window 
                if (proc.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }
                // to find the tabs we first need to locate something reliable - the 'New Tab' button 
                AutomationElement root = AutomationElement.FromHandle(proc.MainWindowHandle);
                Condition condNewTab = new PropertyCondition(AutomationElement.NameProperty, "Новая вкладка");
                AutomationElement elmNewTab = root.FindFirst(TreeScope.Descendants, condNewTab);
                // get the tabstrip by getting the parent of the 'new tab' button 
                TreeWalker treewalker = TreeWalker.ControlViewWalker;
                AutomationElement elmTabStrip = treewalker.GetParent(elmNewTab);
                // loop through all the tabs and get the names which is the page title 
                Condition condTabItem = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
                foreach (AutomationElement tabitem in elmTabStrip.FindAll(TreeScope.Children, condTabItem))
                {
                    if (tabitem.Current.Name.Contains("- YouTube"))
                    {
                        cursongname = tabitem.Current.Name.Replace("- YouTube", "");
                    }

                }
            }
            Console.WriteLine(cursongname);
            FileStream fileStream = File.Open("Song.txt", FileMode.Open);

            fileStream.SetLength(0);
            fileStream.Close(); // 
            using (StreamWriter w = File.AppendText("Song.txt"))
            {
                w.Write(cursongname);
                w.Close();
            }
            Thread.Sleep(5000);
            OperaGXCheck();
        }

        public static void ChromeCheck()
        {
            var cursongname = "Песни не играет";
            Process[] procsChrome = Process.GetProcessesByName("chrome");
            if (procsChrome.Length <= 0)
            {
                Console.WriteLine("Chrome is not running");
                cursongname = "Браузер не открыт";
            }


            foreach (Process proc in procsChrome)
            {
                // the chrome process must have a window 
                if (proc.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }
                // to find the tabs we first need to locate something reliable - the 'New Tab' button 
                AutomationElement root = AutomationElement.FromHandle(proc.MainWindowHandle);
                Condition condNewTab = new PropertyCondition(AutomationElement.NameProperty, "Новая вкладка");
                AutomationElement elmNewTab = root.FindFirst(TreeScope.Descendants, condNewTab);
                // get the tabstrip by getting the parent of the 'new tab' button 
                TreeWalker treewalker = TreeWalker.ControlViewWalker;
                AutomationElement elmTabStrip = treewalker.GetParent(elmNewTab);
                // loop through all the tabs and get the names which is the page title 
                Condition condTabItem = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
                foreach (AutomationElement tabitem in elmTabStrip.FindAll(TreeScope.Children, condTabItem))
                {
                    if (tabitem.Current.Name.Contains("YouTube: воспроизводится аудио"))
                    {
                        cursongname = tabitem.Current.Name.Replace("- YouTube: воспроизводится аудио", "");
                    }

                }
            }
            Console.WriteLine(cursongname);
            FileStream fileStream = File.Open("Song.txt", FileMode.Open);

            fileStream.SetLength(0);
            fileStream.Close(); // 
            using (StreamWriter w = File.AppendText("Song.txt"))
            {
                w.Write(cursongname);
                w.Close();
            }
            Thread.Sleep(5000);
            ChromeCheck();
        }

    }
}
