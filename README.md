# ✅ **Windows Services Projects Repository**

---

## 🧾 **Introduction**

This repository contains several **Windows Service projects** developed in **C# (.NET Framework)**, each demonstrating practical use cases of background services, file monitoring, database operations, and fault handling.  
Windows Services allow tasks to run in the background without user interaction, providing automation, reliability, and scalability for system and application-level operations.

---

## 🚀 **Projects Overview**

### 1. **File Monitoring Windows Service**
- **Description:** Monitors a source folder for new files, automatically renames them with a GUID, moves them to a destination folder, and logs all operations.
- **Features:** Folder monitoring, auto file processing, logging, dual mode (Console & Service), configurable paths.
- **Documentation:** [FileMonitoring_Documentation.md](./File%20Monitoring%20-%20Windows%20Service%20Project/Docs/FileMonitoring_Documentation.md)
- **Folder:** `File Monitoring - Windows Service Project`

---
### 2. **Database Backup Service**
- **Description:** Performs scheduled backups of SQL Server databases, saves backup files with timestamps, and logs all operations.
- **Features:** Automated backups, dynamic configuration via `App.config`, logging, service dependencies, console debug mode.
- **Documentation:** [FileMonitoring_Documentation.md](./File%20Monitoring%20-%20Windows%20Service%20Project/Docs/FileMonitoring_Documentation.md)
- **Folder:** `DatabaseBackupService`
---

### 3. **Debug Mode & Fault Handling**
- **Description:** Implements fault handling in a Windows Service with optional console debug mode. Demonstrates how to safely log and recover from service errors.
- **Features:** Console debug mode, logging exceptions, background thread management, service recovery simulation.
- **Folder:** `Debug Mode In Console - Fault Handling In Code`

---

### 4. **Full Service State Implementation**
- **Description:** Demonstrates a complete Windows Service lifecycle including all states: Installed, Start-Pending, Running, Paused, Stop-Pending, Stopped, and Uninstalled.
- **Features:** Debug mode in console, fault handling, logging service states, practical examples of `OnStart`, `OnStop`, `OnPause`, and `OnContinue`.
- **Folder:** `Full Service State Implementation - With Debug Mode In Console`

---

### 5. **Miscellaneous**
- `.gitignore` file to exclude binaries and logs from version control.
- Submodules have been converted to normal folders for easier project management.

---

## 📌 **Notes**
- All projects demonstrate **real-world Windows Service scenarios**.
- Each project includes **documentation**, **logging**, and **debugging capabilities**.
- Use `App.config` to configure paths, intervals, and other settings where applicable.
- Recommended to run console debug mode during development for easier testing before deployment as a service.

---

