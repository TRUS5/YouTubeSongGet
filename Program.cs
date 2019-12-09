using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Automation;
using System.Text;

namespace MilashkaChannel
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("Song.txt"))
            {
                using (FileStream fs = File.Create("Song.txt"))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("Тут будет песня Коляна.");
                    fs.Write(info, 0, info.Length);
                }
            }
            ChromeCheck();

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
                        Console.WriteLine(cursongname);
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
