using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWinSerivce
{
    public partial class MyFirstWinService : ServiceBase
    {
        public MyFirstWinService()
        {
            InitializeComponent();

            CanPauseAndContinue = true;

            CanShutdown = true;


        }


        protected override void OnStart(string[] args)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Service OnStart\n";
            LogServiceEvent(logMessage);
        }

        protected override void OnPause()
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Service OnPause\n";
            LogServiceEvent(logMessage);
        }

        protected override void OnShutdown()
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Service OnShutdown\n";
            LogServiceEvent(logMessage);
        }

        protected override void OnStop()
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Service OnStap\n";
            LogServiceEvent(logMessage);
        }
        protected override void OnContinue()
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Service OnContinue\n";
            LogServiceEvent(logMessage);
        }


        private void InitializLogFile(ref string  logFilePath)
        {
            string logDirectory = @"C:\English";

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            logFilePath = Path.Combine(logDirectory, "MyServiceLog.txt");

        }
        private void LogServiceEvent(string logMessage)
        {
            string logFilePath = string.Empty;

            InitializLogFile(ref logFilePath);

            File.AppendAllText(logFilePath, logMessage);
        }


    }


}
