
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace File_Monitoring_Windows_Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller;
        private ServiceInstaller ServiceInstaller;

        public ProjectInstaller()
        {
            InitializeComponent();

            serviceProcessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            ServiceInstaller = new ServiceInstaller
            {
                StartType = ServiceStartMode.Automatic,
                ServiceName = "FileMonitoringService",
                DisplayName = "File Monitoring Service",
                Description = "Monitors a folder, renames and moves files automatically.",
                ServicesDependedOn = new string[] {"RpcSc", "EventLog", "LanmanWorkstation" }
            };
            
            Installers.Add(serviceProcessInstaller);
            Installers.Add(ServiceInstaller);
        }
    }
}
