using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Monitoring_Windows_Service.Utlties
{
    static public class Log
    {
        static public void LogMessage(string message)
        {
            string LogDrictory = ConfigurationManager.AppSettings["LogFolderPath"];

            if (string.IsNullOrEmpty(message))
                throw new IOException("The message must not be empty");            

            if(string.IsNullOrEmpty(LogDrictory))
            {
                LogDrictory = @"C:\FileMonitoring\Logs";
                Log.LogMessage("Destination Folder is missing in App.config. Using default:" + LogDrictory);
            }

            Directory.CreateDirectory(LogDrictory);

            string LogFilePath = Path.Combine(LogDrictory, ConfigurationManager.AppSettings["LogFileName"]);


            File.AppendAllText(LogFilePath, $"[{DateTime.Now}] {message}{Environment.NewLine}");

            if (Environment.UserInteractive)
                Console.WriteLine($"[{DateTime.Now}] {message}{Environment.NewLine}");
        }
    }
}
