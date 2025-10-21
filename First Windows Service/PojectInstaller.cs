using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;


namespace MyFirstWinSerivce
{
    [RunInstaller(true)]
    public partial class PojectInstaller : Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller;
        ServiceInstaller serviceInstaller;
        public PojectInstaller()
        {
            InitializeComponent();

            // configure the servce
            serviceProcessInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.NetworkService
            };

            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "MyFirstWinService",
                DisplayName = "My First Windows Service",
                StartType = ServiceStartMode.Manual
            };
            
            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);

        }
    }
}
