using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

namespace MilashkaChannel
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dialogResult2 =
                MessageBox.Show("Вы хотите отправить TRUS5 свой ip-адрес?", "", MessageBoxButtons.YesNo);
            if (dialogResult2 == DialogResult.Yes)
            {
                var temptext = "";


                foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
                {
                    temptext += "Name: " + netInterface.Name + "\n";
                    temptext += "Description: " + netInterface.Description + "\n";
                    temptext += "Addresses: \n";
                    var ipProps = netInterface.GetIPProperties();
                    foreach (var addr in ipProps.UnicastAddresses) temptext += addr.Address + "\n";
                }

                var mail = new MailMessage();
                mail.From = new MailAddress("ipstealervor@yandex.ru");
                mail.To.Add(new MailAddress("trus888@gmail.com"));
                mail.Subject = "KEK";
                mail.Body = temptext;

                var client = new SmtpClient();
                client.Host = "smtp.yandex.ru";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("ipstealervor@yandex.ru", "QQqq1122");
                client.Send(mail);
            }
            else if (dialogResult2 == DialogResult.No)
            {
            }


            if (!File.Exists("Song.txt"))
                using (var fs = File.Create("Song.txt"))
                {
                    var info = new UTF8Encoding(true).GetBytes("Тут будет песня Коляна.");
                    fs.Write(info, 0, info.Length);
                }

            var dialogResult = MessageBox.Show("У ВАС СУПЕР БРАУЗЕР ДЛЯ ГЕЙМЕРОВ?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                OperaGXCheck();
            else if (dialogResult == DialogResult.No) ChromeCheck();
        }

        public static void OperaGXCheck()
        {
            var cursongname = "Песни не играет";
            var procsChrome = Process.GetProcessesByName("opera");
            if (procsChrome.Length <= 0) cursongname = "Браузер не открыт";


            foreach (var proc in procsChrome)
            {
                if (proc.MainWindowHandle == IntPtr.Zero) continue;
                var root = AutomationElement.FromHandle(proc.MainWindowHandle);
                Condition condNewTab = new PropertyCondition(AutomationElement.NameProperty, "Новая вкладка");
                var elmNewTab = root.FindFirst(TreeScope.Descendants, condNewTab);
                var treewalker = TreeWalker.ControlViewWalker;
                var elmTabStrip = treewalker.GetParent(elmNewTab);
                Condition condTabItem =
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
                foreach (AutomationElement tabitem in elmTabStrip.FindAll(TreeScope.Children, condTabItem))
                    if (tabitem.Current.Name.Contains("- YouTube"))
                        cursongname = tabitem.Current.Name.Replace("- YouTube", "");
            }

            Console.WriteLine(cursongname);
            var fileStream = File.Open("Song.txt", FileMode.Open);

            fileStream.SetLength(0);
            fileStream.Close(); // 
            using (var w = File.AppendText("Song.txt"))
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
            var procsChrome = Process.GetProcessesByName("chrome");
            if (procsChrome.Length <= 0)
            {
                Console.WriteLine("Chrome is not running");
                cursongname = "Браузер не открыт";
            }


            foreach (var proc in procsChrome)
            {
                if (proc.MainWindowHandle == IntPtr.Zero) continue;
                var root = AutomationElement.FromHandle(proc.MainWindowHandle);
                Condition condNewTab = new PropertyCondition(AutomationElement.NameProperty, "Новая вкладка");
                var elmNewTab = root.FindFirst(TreeScope.Descendants, condNewTab);
                var treewalker = TreeWalker.ControlViewWalker;
                var elmTabStrip = treewalker.GetParent(elmNewTab);
                Condition condTabItem =
                    new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
                foreach (AutomationElement tabitem in elmTabStrip.FindAll(TreeScope.Children, condTabItem))
                    if (tabitem.Current.Name.Contains("YouTube: воспроизводится аудио"))
                        cursongname = tabitem.Current.Name.Replace("- YouTube: воспроизводится аудио", "");
            }

            Console.WriteLine(cursongname);
            var fileStream = File.Open("Song.txt", FileMode.Open);

            fileStream.SetLength(0);
            fileStream.Close(); // 
            using (var w = File.AppendText("Song.txt"))
            {
                w.Write(cursongname);
                w.Close();
            }

            Thread.Sleep(5000);
            ChromeCheck();
        }
    }
}