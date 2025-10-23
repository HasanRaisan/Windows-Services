using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;

namespace DatabaseBackupService
{
    public partial class DatabaseBackupService : ServiceBase
    {
        
        // private Timer backupTimer;
        private string connectionString;
        private string backupFolder;
        private string logFolder;
        // private int backupIntervalMinutes;
        public DatabaseBackupService()
        {
            InitializeComponent();

            _LoadBackupServiceConfig();
        }
        private void _LoadBackupServiceConfig()
        {
            // Read configuration values
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            backupFolder = ConfigurationManager.AppSettings["BackupFolder"];
            logFolder = ConfigurationManager.AppSettings["LogFolder"];

            // Handle missing or invalid Backup Interval
            // if (!int.TryParse(ConfigurationManager.AppSettings["BackupIntervalMinutes"], out int backupIntervalMinutes) || backupIntervalMinutes <= 0)
            // {
            //     backupIntervalMinutes = 60; // Default value
            //     Log("BackupIntervalMinutes is missing or invalid in App.config. Using default: " + backupIntervalMinutes);
            // }

            // Handle missing or empty configuration values for folders
            if (string.IsNullOrWhiteSpace(backupFolder))
            {
                backupFolder = @"C:\DatabaseBackupService\Backup"; // Default value
                Log("BackupFolder is missing in App.config. Using default: " + backupFolder);
            }

            if (string.IsNullOrWhiteSpace(logFolder))
            {
                logFolder = @"C:\DatabaseBackupService\Logs"; // Default value
                Log("LogFolder is missing in App.config. Using default: " + logFolder);
            }

            // Ensure directories exist
            Directory.CreateDirectory(backupFolder);
            Directory.CreateDirectory(logFolder);
        }
        protected override void OnStart(string[] args)
        {

            Log("Service Started.");

            /*
             * Timer has been removed to avoid continuous running of the service.
             * The backup will be executed only once at service startup.
             * Scheduling recurring backups is now left to the user through external tools such as Windows Task Scheduler.
             */

            // Set up a timer to trigger backups periodically
            // backupTimer = new Timer(PerformBackup, null, TimeSpan.Zero, TimeSpan.FromMinutes(backupIntervalMinutes));
            // Initialize Timer to trigger PerformBackup
        
            // backupTimer = new Timer(
            //     callback: PerformBackup,                  // Callback method
            //     state: null,                              // State object (not used here)
            //     dueTime: TimeSpan.Zero,                   // Start immediately
            //     period: TimeSpan.FromMinutes(backupIntervalMinutes) // Interval
            // );

            PerformBackup(null);


            //Log($"Backup schedule initiated: every {backupIntervalMinutes} minute(s).");
            Log($"Backup schedule initiated.");


        }
        protected override void OnStop()
        {
            //backupTimer?.Dispose();
            Log("Service Stopped.");
        }
        private void PerformBackup(object state)
        {
            try
            {
                string backupFileName = Path.Combine(backupFolder, $"Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak");

                // Perform database backup
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string backupQuery = $@"BACKUP DATABASE [{connection.Database}] TO DISK = '{backupFileName}' WITH INIT";
                    using (SqlCommand command = new SqlCommand(backupQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                // Log successful backup
                Log($"Database backup successful: {backupFileName}");

            }
            catch (Exception ex)
            {
                Log($"Error during backup: {ex.Message}");
            }
        }
        private void Log(string message)
        {
            string logFilePath = Path.Combine(logFolder, "ServiceLog.txt");
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n";

            File.AppendAllText(logFilePath, logMessage);

            // Output to console if running in debug mode
            if (Environment.UserInteractive)
            {
                Console.WriteLine(logMessage);
            }
        }
        public void StartInConsole()
        {
            OnStart(null);
            Console.WriteLine("Press Enter to stop the service...");
            Console.ReadLine();
            OnStop();
        }
    }
}
