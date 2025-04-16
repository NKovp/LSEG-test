# Log Monitoring Console App

This is a simple **log monitoring application** built with **.NET Framework 4.8**.  
It parses a CSV-style log file, calculates job durations, and outputs warnings or errors based on execution time.

## ✅ Features

- Parses `logs.log` file (CSV with format: `HH:mm:ss, description, START|END, PID`)
- Tracks duration for each job (by PID)
- Outputs:
  - `WARNING` if duration > 5 minutes
  - `ERROR` if duration > 10 minutes
- Writes results to:
  - **Console**
  - **Output file**: `/OUTPUT/report.txt`

## 🛠 Requirements

- .NET Framework 4.8
- Visual Studio 2019 or newer

## 🧪 How to Run

1. Place your `logs.log` file in the project root.
2. Build and run the application.
3. Check the results in the terminal and in the `OUTPUT/report.txt` file.

## 📁 File Structure

```
LSEG app/
├── logs.log
├── OUTPUT/
│   └── report.txt
├── Program.cs
├── LSEG app.csproj
└── ...
```

## 📝 Example Output

```
[INFO]    PID: 57672, Task: scheduled task 796, Duration: 00:07
[WARNING] PID: 81258, Task: background job wmy, Duration: 06:44
[ERROR]   PID: 39547, Task: scheduled task 051, Duration: 11:29
```

---

Feel free to modify and expand this project as needed!
