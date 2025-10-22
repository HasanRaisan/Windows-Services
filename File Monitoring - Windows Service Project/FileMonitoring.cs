
using File_Monitoring_Windows_Service.Utlties;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Threading;


namespace File_Monitoring_Windows_Service
{
    public partial class FileMonitoring : ServiceBase
    {
            // ... = ConfigurationManager.AppSettings["key"]

        public FileMonitoring()
        {
            InitializeComponent();
            CanShutdown = true;
            CanPauseAndContinue = true;

        }

        string sourceFolder;
        string destinationFolder;
        private FileSystemWatcher watcher;

        protected override void OnStart(string[] args)
        {
            try
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Normal;

                Log.LogMessage("Service Started.");

                _EquipmentFolders();
                _WatchSourceFolder();

            }
            catch (Exception  ex) 
            {
                Log.LogMessage($"Erorr in OnStart: {ex.Message}");
            }

        }

        protected override void OnStop()
        {
            try
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                Log.LogMessage("Service Stopped.");
            }
            catch (Exception ex)
            {
                Log.LogMessage($"Error in OnStop: {ex.Message}");
            }
        }


        private void _EquipmentFolders()
        {
            // Read folder paths from App.config
            sourceFolder = ConfigurationManager.AppSettings["SourceFolderPath"];
            destinationFolder = ConfigurationManager.AppSettings["DestinationFolderPath"];

            // Handle missing or empty configuration values
            if (string.IsNullOrWhiteSpace(sourceFolder))
            {
                sourceFolder = @"C:\FileMonitoring\Source"; // Default source folder
                Log.LogMessage("SourceFolder is missing in App.config. Using default: " + sourceFolder);
            }

      
            if (string.IsNullOrWhiteSpace(destinationFolder))
            {
                destinationFolder = @"C:\FileMonitoring\Destination"; // Default destination folder
                Log.LogMessage("Destination Folder is missing in App.config. Using default:" + destinationFolder);
            }


            Directory.CreateDirectory(sourceFolder);

            Directory.CreateDirectory(destinationFolder);
        }

        private void _WatchSourceFolder()
        {

            try
            {
                watcher = new FileSystemWatcher
                {
                    Path = sourceFolder,
                    NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime,
                    Filter = "*.*",
                    EnableRaisingEvents = true,
                    IncludeSubdirectories = true,
                };

                watcher.Created += OnFileCreated;


            }
            catch (Exception ex)
            {
                Log.LogMessage($"Error processing Watcher: {ex.Message}");
            }

        }
        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                string sourceFile = e.FullPath;
                string extension = Path.GetExtension(sourceFile);
                string destinationFile = Path.Combine(destinationFolder, $"{Guid.NewGuid()}{extension}");

                // Wait until file is ready
                WaitForFile(sourceFile);

                Log.LogMessage($"New File Detected: {sourceFile}");

                File.Move(sourceFile, destinationFile);

                Log.LogMessage($"File Moved: {sourceFile} -> {destinationFile}");
            }
            catch (Exception ex)
            {
                Log.LogMessage($"Error processing file {e.FullPath}: {ex.Message}");
            }
        }

        private void WaitForFile(string file)
        {
            // Wait until the file is fully written
            int retries = 10;
            while (retries > 0)
            {
                try
                {
                    using (FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        if (stream.Length > 0)
                            break;
                        if (stream.Length == 0)
                            Log.LogMessage("Cannot open the file, retrying...");
                    }
                }
                catch
                {
                    Thread.Sleep(1000);
                }
                retries--;
            }
        }
        public void StartInConsole()
        {
            OnStart(null); // Trigger OnStart logic
            Console.WriteLine("Press Enter to stop the service...");
            Console.ReadLine(); // Wait for user input to simulate service stopping
            OnStop(); // Trigger OnStop logic
            Console.ReadKey();
        }
    }
}
