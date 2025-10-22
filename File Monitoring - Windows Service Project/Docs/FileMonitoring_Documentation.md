# ✅ **Project Documentation: File Monitoring Windows Service**

---

## 🧾 **1. Project Overview**

The **File Monitoring Windows Service** is a background service developed in **C# (.NET Framework)** that monitors a source directory for new files. When a new file is detected, the service automatically **renames it using a GUID**, **moves it to a destination folder**, and **logs all operations**.

This service is suitable for automated file processing systems such as:

- Document intake
- Secure file transfers
- Automated ETL operations

---

## 🚀 **2. Features**

| Feature                  | Description                                                            |
| ------------------------ | ---------------------------------------------------------------------- |
| **Folder Monitoring**    | Detects newly added files using FileSystemWatcher.                     |
| **Auto File Processing** | Renames files with a GUID and moves them to a destination folder.      |
| **Logging**              | Records all events, errors, and file operations in log files.          |
| **Configurable Paths**   | Source, Destination, and Logs folders are configured in App.config.    |
| **Dual Mode**            | Can run as a **Windows Service** or in **Console Mode** for debugging. |
| **Automatic Startup**    | Service configured to start automatically with Windows.                |

---

## 📂 **3. App.config Settings**

```xml
<appSettings>
    <add key="SourceFolderPath" value="C:\English\File Monitoring Windows Service\Source" />
    <add key="DestinationFolderPath" value="C:\English\File Monitoring Windows Service\Destination" />
    <add key="LogFolderPath" value="C:\English\File Monitoring Windows Service\Logs" />
    <add key="LogFileName" value="Logs.txt" />
</appSettings>
```

---

## 🏗️ **4. Project Structure**

```
FileMonitoringService/
│
├── FileMonitoring.cs          // Core service logic
├── ProjectInstaller.cs        // Installer for Windows Service
├── Program.cs                 // Entry point (Console mode / Service mode switch)
├── App.config                 // Configurable settings
└── Utilities/
      └── Log/                 // Generated logs (at runtime)
```

---

## 🔧 **5. Running Modes**

### 🧑‍💻 Console Debug Mode

Used during development to test functionality **without installing the service**.

**When running in console mode:**

- Logs appear in the console.
- Service can be stopped manually using a key press.

**Steps:**

1. Go to **Project Properties → Application → Output type → Console Application**
2. Run and debug normally.
3. Don’t forget to **revert the output type** back to **Windows Application** before deployment.

---

### 🏢 Windows Service Mode

Used in production — runs silently in the background **without user interface**.

---

## 🛠️ **6. Installation Guide (Windows Service Deployment)**

### **Prerequisites**

- .NET Framework installed
- Administrator privileges
- Build the project in **Release** mode

---

### **Step 1: Build the Service**

In Visual Studio:

> Build → Configuration: Release → Build Solution

Output EXE will be located at:

```
bin\Release\FileMonitoringService.exe
```

---

### **Step 2: Install the Service**

1. Go to:

```
C:\Windows\Microsoft.NET\Framework64\v4.0.30319
```

2. Copy the path of **InstallUtil.exe**
3. Open **Command Prompt as Administrator**
4. Run the following commands:

```cmd
cd "C:\Windows\Microsoft.NET\Framework64\v4.0.30319"
InstallUtil.exe "C:\Path\FileMonitoringService.exe"
```

---

### **Step 3: Start the Service**

```cmd
sc start FileMonitoringService
```

---

## ❌ **7. Uninstallation Guide**

```cmd
sc stop FileMonitoringService
sc delete FileMonitoringService
```

---

## 🧪 **8. Testing Scenarios**

| Test Case                      | Expected Result                                  |
| ------------------------------ | ------------------------------------------------ |
| Add file to source folder      | File renamed using GUID and moved to destination |
| Check logs                     | Log file contains timestamped events             |
| Restart service                | Monitoring resumes normally                      |
| File locked by another process | Error logged without crashing service            |

---

## 📜 **9. Logging Sample Output**

```
[2025-01-18 14:10:00] Service Started.
[2025-01-18 14:10:12] File detected: C:\Source\data1.csv
[2025-01-18 14:10:12] File moved to: C:\Destination\5b3e12fa-88c3.csv
```

---

## 🧱 **10. Technologies Used**

- C# (.NET Framework)
- Windows Services
- FileSystemWatcher
- Logging and Exception Handling

---

## 📌 **11. Future Enhancements (Optional)**

- Add **email notifications** on errors
- Add **database logging**
- Support multiple file types with rules
- Integrate with **Windows Event Viewer**
