using LSEG_app.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LSEG_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Path to the log file
            string logPath = "logs.log";

            // Check if the log file exists before proceeding
            if (!File.Exists(logPath))
            {
                Console.WriteLine($"Log file not found. Filename: {logPath}");
                return;
            }

            // Read all lines from the log file and parse them into structured objects
            var logs = File.ReadAllLines(logPath)
                .Select(line => ParseLogEntry(line))
                .Where(e => e != null)
                .ToList();

            // Group all log entries by their PID (each job/task has a unique PID)
            var jobGroups = logs.GroupBy(log => log.PID);
            var jobDurations = new List<JobDuration>();

            foreach (var group in jobGroups)
            {
                // Find the first START and END entry for this job
                var start = group.FirstOrDefault(e => e.Action == "START");
                var end = group.FirstOrDefault(e => e.Action == "END");

                // Only calculate duration if both START and END entries exist
                if (start != null && end != null)
                {
                    jobDurations.Add(new JobDuration
                    {
                        PID = group.Key,
                        Description = start.Description,
                        StartTime = start.Time,
                        EndTime = end.Time
                    });
                }
            }

            // Create path relative to the base directory of the app
            string outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\OUTPUT");
            Directory.CreateDirectory(outputDir); // Create the directory if it doesn't exist

            string reportPath = Path.Combine(outputDir, "report.txt");

            using (var writer = new StreamWriter(reportPath, false)) // false = overwrite
            {
                // Sort and output job durations, categorizing them by how long they took
                foreach (var job in jobDurations.OrderBy(j => j.StartTime))
                {
                    var duration = job.Duration;
                    string status = "INFO";

                    if (duration > TimeSpan.FromMinutes(10))
                    {
                        status = "ERROR";
                    }
                    else if (duration > TimeSpan.FromMinutes(5))
                    {
                        status = "WARNING";
                    }

                    string output = $"[{status}] " +
                        $"PID: {job.PID}, " +
                        $"Task: {job.Description}, " +
                        $"Duration: {duration:mm\\:ss}";

                    Console.WriteLine(output);       // Output to console
                    writer.WriteLine(output);        // Output to file
                }
            }
        }

        /// <summary>
        /// // Parses a single line of the log into a LogEntry object
        /// CSV format: DATE, NAME, TYPE, PID
        /// </summary>
        /// <param name="line">log line.</param>
        /// <returns>Log entry object</returns>
        static LogEntry ParseLogEntry(string line)
        {
            var parts = line.Split(',');
            if (parts.Length != 4)
                return null;

            if (!DateTime.TryParseExact(parts[0], "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time))
                return null;

            return new LogEntry
            {
                Time = time,
                Description = parts[1].Trim(),
                Action = parts[2].Trim(),
                PID = parts[3].Trim()
            };
        }
    }
}
