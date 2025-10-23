# 🧩 DatabaseBackupService – Overview

**DatabaseBackupService** is a Windows Service that performs an automatic backup of a SQL Server database when the service starts.  
It is lightweight, efficient, and designed to run **once on startup** to avoid constant resource consumption.

> ⛔ **No Timer is used inside the service** to avoid continuous looping.  
> ✅ For repeated backups, use Windows Task Scheduler.

---

## 🏗️ Project Structure

| File / Folder | Description |
|---------------|-------------|
| `DatabaseBackupService.cs` | Core service logic for database backup |
| `ProjectInstaller.cs` | Handles service installation and configuration |
| `App.config` | Configuration: DB connection + paths |
| `Backup` | Stores generated `.bak` files |
| `Logs` | Stores log file entries for the service |

---

## ⚙️ Configuration (`App.config`)

Update these settings before running the service:

```xml
<appSettings>
  <add key="ConnectionString" value="Server=.;Database=YourDB;Trusted_Connection=True;" />
  <add key="BackupFolder" value="C:\DatabaseBackupService\Backup" />
  <add key="LogFolder" value="C:\DatabaseBackupService\Logs" />
</appSettings>
```

### 📌 Default Folders Created Automatically

| Setting | Default Path |
|---------|-------------------------------|
| `BackupFolder` | `C:\DatabaseBackupService\Backup` |
| `LogFolder` | `C:\DatabaseBackupService\Logs` |

---

## 🔧 Running Modes

### 🧑‍💻 Console Debug Mode (Development)

Use during development to test the service **without installation**.

- Shows logs directly in the console
- Allows manual stop with a key press

**Steps:**  
1. Project Properties → **Application** → Output Type → `Console Application`  
2. Run normally for debugging  
3. Revert back to `Windows Application` before publishing  

### 🏢 Windows Service Mode (Production)

Runs silently in the background with no UI.

---

## 🛠️ Installation Guide (Windows Service Deployment)

### ✅ Prerequisites

- .NET Framework installed
- Admin privileges
- Build in **Release mode**

### 1️⃣ Build the Service

Visual Studio → Build → Select **Release** → Build Solution

Output EXE location:  
```
bin\Release\DatabaseBackupService.exe
```

### 2️⃣ Install the Service

1. Open **Command Prompt as Administrator**
2. Navigate to InstallUtil path:
   ```cmd
   cd "C:\Windows\Microsoft.NET\Framework644.0.30319"
   ```
3. Install the service:
   ```cmd
   InstallUtil.exe "C:\Path\DatabaseBackupService.exe"
   ```

---

## 🗄️ Backup Execution Logic

When the service starts:

✅ Creates a backup file with timestamp format:  
`Backup_YYYYMMDD_HHMMSS.bak`

✅ Saves it in the configured backup folder  
✅ Logs success or errors  

---

## 📑 Logging System

### Log file location:
```
Logs\ServiceLog.txt
```

### Logged Information:

- Service start & stop
- Backup success with file path
- Errors with details

#### ✔ Sample Log
```
[2025-10-23 10:15:22] Service Started.
[2025-10-23 10:15:23] Database backup successful: C:\DatabaseBackupService\Backup\Backup_20251023_101523.bak
```

---

## ⏱️ Scheduling Backups (Optional)

If backups are required periodically, use **Windows Task Scheduler**.

Example: Run daily at 2:00 AM

1. Open Task Scheduler
2. Create Task
3. Trigger → Daily → `2:00 AM`
4. Action → Start Program → select service EXE

---

## 📍 Future Enhancements (Roadmap)

| Feature | Status |
|---------|--------|
| Cloud backup (Azure/AWS/Google Drive) | ⏳ Planned |
| Compress backup `.zip` | ⏳ Planned |
| Email notification | ⏳ Planned |
| Multi-database backup support | ✅ Next release option |

---

© 2025 — DatabaseBackupService Documentation
