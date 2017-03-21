using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPS_Monitor
{
    public partial class Form1 : Form
    {
        public static Thread autoThread;
        public static string logPath = @"UPS_MonitorLog.txt";
        public static string cfgFilePath = @"UPS_MonitorConfig.txt"; 
        public static string ipToMonitor = "127.0.0.1"; 
        Icon iconGreen = new Icon("IconGreen.ico");
        Icon iconYellow = new Icon("IconOrange.ico");
        Icon iconRed = new Icon("IconRed.ico");
        public static ContextMenu notifyIcon_menu = new ContextMenu();
        public static bool exit = false;

        public static int sleep = 0;
        public static int fails = 0;
        public static bool pause = false;

        public Form1()
        {
            InitializeComponent();
            CreateNotificationIconMenu();
            MinimizeToTray();
            Log("Program started", false);
            ReadSettings();
            StartAutoProcess();
            checkBoxAutoScroll.Checked = true;
            buttonReset.Enabled = false;
            notifyIcon_menu.MenuItems[1].Enabled = false;


        }

        private void CreateNotificationIconMenu()
        {
            notifyIcon_menu.MenuItems.Add(0, new MenuItem("Show Log", new System.EventHandler(ShowLog_Click)));
            notifyIcon_menu.MenuItems.Add(1, new MenuItem("Reset", new System.EventHandler(Reset_Click)));
            notifyIcon_menu.MenuItems.Add(2, new MenuItem("Snooze for 1 hour", new System.EventHandler(PauseOneHour_Click)));
            notifyIcon_menu.MenuItems.Add(3, new MenuItem("Show main window", new System.EventHandler(ShowMainWindow_Click)));
            notifyIcon_menu.MenuItems.Add(4, new MenuItem("Exit", new System.EventHandler(Exit_Click)));

            notifyIcon.ContextMenu = notifyIcon_menu;
        }

        private void MinimizeToTray()
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon.Visible = true;
            Log("Minimized to tray", false);
        }

        private void StartAutoProcess()
        {
            autoThread = new Thread(AutoProcess);
            try
            {
                autoThread.Start();
                Log("AutoThread started successfully", false);
            }
            catch (Exception ex)
            {
                Log("Error: Failed to start AutoThread", false);
                Log("Error message: " + ex.Message, false);
            }
        }

        private void AutoProcess()
        {
            Thread.Sleep(5000);
            if (checkBoxAutoScroll.Checked)
            {
                this.Invoke((MethodInvoker)(() => listBoxLog.TopIndex = listBoxLog.Items.Count - 1));
            }
            while (true)
            {
                if (!pause)
                {
                    bool pingResult = PingHost(ipToMonitor);
                    if (pingResult)
                    {
                        Log("NAS online. New check in 5 minutes", true);
                        this.Invoke((MethodInvoker)(() => buttonReset.Enabled = false));
                        this.Invoke((MethodInvoker)(() => notifyIcon_menu.MenuItems[1].Enabled = false));
                        sleep = 300; //sec
                        fails = 0;
                    }
                    else
                    {
                        Log("NAS OFFLINE. Shutting down in " + (5 - fails) + " minutes.", true);
                        this.Invoke((MethodInvoker)(() => buttonReset.Enabled = true));
                        this.Invoke((MethodInvoker)(() => notifyIcon_menu.MenuItems[1].Enabled = true));

                        fails++;
                        sleep = 60; //sec
                    }

                    if (fails == 6)
                    {
                        Log("Computer is shutting down...", true);
                        Process.Start("shutdown", "/f /s /t 10");
                    }
                }
                int i = 0;
                while (i < sleep)
                {
                    if (sleep == 300) UpdateIcon("green", "NAS online. New check in " + (sleep - i) + " seconds");
                    else if (sleep == 3600)
                    {
                        UpdateIcon("yellow", "NAS montor paused for " + (sleep - i) + " seconds");
                    }
                    else UpdateIcon("red", "NAS OFFLINE! New check in " + (sleep - i) + " seconds");

                    if (checkBoxAutoScroll.Checked) this.Invoke((MethodInvoker)(() => listBoxLog.TopIndex = listBoxLog.Items.Count - 1));

                    Thread.Sleep(1000);
                    i++;
                }
            }
        }

        private void UpdateIcon(string color, string message)
        {
            if (color == "red") this.Invoke((MethodInvoker)(() => notifyIcon.Icon = iconRed));
            else if (color == "yellow") this.Invoke((MethodInvoker)(() => notifyIcon.Icon = iconYellow));
            else this.Invoke((MethodInvoker)(() => notifyIcon.Icon = iconGreen));
            this.Invoke((MethodInvoker)(() => notifyIcon.Text = message));
        }

       

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            return pingable;
        }

        private void Log(string message, bool crossThread)
        {
            if (crossThread)
            {
                this.Invoke((MethodInvoker)(() => listBoxLog.Items.Add(DateTime.Now + ": " + message)));
                if (checkBoxAutoScroll.Checked) this.Invoke((MethodInvoker)(() => listBoxLog.TopIndex = listBoxLog.Items.Count - 1));
            }
            else
            {
                listBoxLog.Items.Add(DateTime.Now + ": " + message);
                if (checkBoxAutoScroll.Checked) listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
            }

            if (!File.Exists(logPath))
            {
                using (StreamWriter sw = File.CreateText(logPath))
                {
                    sw.WriteLine(DateTime.Now + ": LOGFILE CRERATED");
                }
            }
            using (StreamWriter sw = File.AppendText(logPath))
            {
                sw.WriteLine(DateTime.Now + ": " + message);
            }
        }

        protected void Exit_Click(Object sender, System.EventArgs e)
        {
            Log("Program shut down by user", false);
            exit = true;
            ExitProgram();
        }
        protected void ShowMainWindow_Click(Object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Activate();
        }
        protected void PauseOneHour_Click(Object sender, System.EventArgs e)
        {
            PauseOneHour();
        }

        protected void Reset_Click(Object sender, System.EventArgs e)
        {
            Reset();
        }

        protected void ShowLog_Click(Object sender, System.EventArgs e)
        {
            Log("Logfile opened", false);
            System.Diagnostics.Process.Start("notepad.exe", logPath);
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BringWindowToFront();
        }

        private void BringWindowToFront()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
            else
            {
                this.Activate();
            }
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (!exit)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                Log("Program minimized", false);
            }
        }
        private void ExitProgram()
        {
            autoThread.Abort();
            Application.Exit();
        }

        private void buttonClearLog_Click(object sender, EventArgs e)
        {
            listBoxLog.Items.Clear();
            Log("Log window cleared by user", false);
        }

        private void buttonShowLog_Click(object sender, EventArgs e)
        {
            Log("Logfile opened", false);
            System.Diagnostics.Process.Start("notepad.exe", logPath);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            fails = 0;
            Log("Reset clicked", false);
        }

        private void buttonPauseOneHour_Click(object sender, EventArgs e)
        {
            PauseOneHour();
        }

        private void PauseOneHour()
        {
            pause = !pause;
            if (pause)
            {
                sleep = 3600;
                Log("NAS monitor snoozed for 1 hour", false);
                buttonPauseOneHour.Text = "Unsnooze";
                notifyIcon.ContextMenu.MenuItems[2].Text = "Unsnooze";
            }
            else
            {
                Log("NAS monitor unsnoozed", false);
                sleep = 0;
                buttonPauseOneHour.Text = "Snooze one hour";
                notifyIcon.ContextMenu.MenuItems[2].Text = "Snooze for 1 hour";
            }
        }

        private void ReadSettings()
        {
            bool setupNeeded = false;
            if (!File.Exists(cfgFilePath))
            {
                setupNeeded = true;
                Log("No configuration file found", false);

                using (StreamWriter sw = File.CreateText(cfgFilePath))
                {
                    sw.WriteLine("# Enter an IP of the master hoast, e.g. a NAS connected to the same UPS as your computer.");
                    sw.WriteLine("# ");
                    sw.WriteLine("# The NAS will be pinged every 5 minutes.");
                    sw.WriteLine("# If the ping fails the shut down sequense will start.");
                    sw.WriteLine("# The NAS will be pinged once a minute for 5 minutes.");
                    sw.WriteLine("# If no response from the NAS during this time the computer will be forced to shut down.");
                    sw.WriteLine("#  ");
                    sw.WriteLine(@"# IP address to be monitored (e.g. 192.168.0.1)");
                    sw.WriteLine(@"127.0.0.1");
                   
                }
                Log("Configuration file created", false);
                MessageBox.Show("Please enter your configuration.\n\nThe configuration file will now be opened.\n\nDon't forget to click \"Load New Config\" when you are done.", "Configuration Needed", MessageBoxButtons.OK);
                BringWindowToFront();
                EditConfig();
            }
            var lines = File.ReadAllLines(cfgFilePath);
            int count = 0;

            foreach (var line in lines)
            {
                if (line[0] != '#')
                {
                    if (count == 0)
                    {
                        ipToMonitor = line;
                        Log("IP to monitor set: " + line, false);
                    }
                    count++;
                }
            }
            if (!setupNeeded) buttonLoadConfig.Enabled = false;
        }
        private void EditConfig()
        {
            try
            {
                Log("Configuration file opened", false);
                System.Diagnostics.Process.Start("notepad.exe", cfgFilePath);
                buttonLoadConfig.Enabled = true;
            }
            catch (Exception ex)
            {
                Log("Error: Failed to open configuration file", false);
                Log("Error message: " + ex.Message, false);
            }
        }

        private void buttonEditConfig_Click(object sender, EventArgs e)
        {
            EditConfig();
        }

        private void buttonLoadConfig_Click(object sender, EventArgs e)
        {
            ReadSettings();
            buttonLoadConfig.Enabled = false;
        }

    }
}
