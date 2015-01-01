using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace QLSV.Core.Utils.Core
{
    public static class Log2File
    {
        public enum LogFileType
        {
            Trace = 0,
            Message = 1,
            Exception = 2,
        }

        public static void Log(Exception ex)
        {
            var log = new EventLog
            {
                Source = "Xuat_Nhap_Excel/BizService"
            };
            log.WriteEntry(string.Concat(new object[] { ex.Message, Environment.NewLine, 
                                                        ex.Source, Environment.NewLine, 
                                                        ex.StackTrace, 
                                                        ex.TargetSite, 
                                                        ex.InnerException }), EventLogEntryType.Error, 100);
            log.Close();
        }

        public static void LogExceptionToFile(Exception ex)
        {
            var logPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            var logDirectory = logPath + @"\EXCEPTION";
            var filePath = "";
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
                filePath = logDirectory + @"\EXCEPTION.0.log";
            }
            else
            {
                var filePaths = Directory.GetFiles(logDirectory, "*.log");
                if (filePaths.Length == 0)
                {
                    filePath = logDirectory + @"\EXCEPTION.0.log";
                }
                else
                {
                    var fPath = filePaths[filePaths.Length - 1];
                    if (File.Exists(fPath))
                    {
                        var lastestFile = new FileInfo(fPath);
                        // > 2 MB
                        if (((lastestFile.Length / 1024f) / 1024f) > 2)
                        {
                            var file = new FileInfo(fPath);
                            var fileName = file.Name.Split('.');
                            filePath = logDirectory + @"\EXCEPTION."+ (int.Parse(fileName[1])+1) +@".log";
                        }
                        else
                        {
                            filePath = fPath;
                        }
                    }
                }
            }

            var a = Environment.NewLine;
            var logMessage = string.Concat(new object[]
            {
                ex.Message,
                Environment.NewLine,
                ex.Source, 
                Environment.NewLine,
                ex.StackTrace,
                Environment.NewLine,
                ex.TargetSite,
                Environment.NewLine,
                ex.InnerException
            });
            logMessage = DateTime.Now.ToString("HH:mm:ss") + " " + logMessage;
            var listener = new TextWriterTraceListener(filePath);
            listener.WriteLine(logMessage);
            listener.Flush();
            listener.Close();
        }

        public static void LogToFile(LogFileType logType, string logMessage)
        {
            //string LogPath = Environment.CurrentDirectory;
            var logPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);//@"C:\VNPT-BHXH";
            var logDirectory = logPath + @"\" + DateTime.Today.ToString("yyyyMMdd");
            string filePath;
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
                switch (logType)
                {
                    case LogFileType.Trace:
                        filePath = logDirectory + @"\TRACE.0.log";
                        break;
                    case LogFileType.Message:
                        filePath = logDirectory + @"\MESSAGE.0.log";
                        break;
                    case LogFileType.Exception:
                        filePath = logDirectory + @"\EXCEPTION.0.log";
                        break;
                    default:
                        filePath = logDirectory + @"\TRACE.0.log";
                        break;
                }
            }
            else
            {
                var filePaths = Directory.GetFiles(logDirectory, "*.log");
                if (filePaths.Length == 0)
                {
                    switch (logType)
                    {
                        case LogFileType.Trace:
                            filePath = logDirectory + @"\TRACE.0.log";
                            break;
                        case LogFileType.Message:
                            filePath = logDirectory + @"\MESSAGE.0.log";
                            break;
                        case LogFileType.Exception:
                            filePath = logDirectory + @"\EXCEPTION.0.log";
                            break;
                        default:
                            filePath = logDirectory + @"\TRACE.0.log";
                            break;
                    }
                }
                else
                {
                    var fileList = new List<string>();
                    foreach (var fPath in filePaths)
                    {
                        var file = new FileInfo(fPath);
                        var parts = file.Name.Split('.');
                        if (parts[0].ToUpper() == logType.ToString().ToUpper())
                        {
                            fileList.Add(fPath);
                        }
                    }

                    var lastestIndex = int.MinValue;
                    var lastestFilePath = "";
                    if (fileList.Count <= 0)
                    {
                        filePath = logDirectory + @"\" + logType.ToString().ToUpper() + ".0.log";
                    }
                    else
                    {
                        foreach (var fPath in fileList)
                        {
                            var file = new FileInfo(fPath);
                            var parts = file.Name.Split('.'); //fPath.Split('.');
                            if (Convert.ToInt32(parts[1]) >= lastestIndex)
                            {
                                lastestIndex = Convert.ToInt32(parts[1]);
                                lastestFilePath = fPath;
                            }
                        }

                        filePath = lastestFilePath;
                    }

                    if (File.Exists(filePath))
                    {
                        var lastestFile = new FileInfo(filePath);
                        // check if file size be larger than 5MB then create new one
                        if (((lastestFile.Length / 1024f) / 1024f) > 5)
                        {
                            lastestIndex++;
                            filePath = logDirectory + @"\" + logType.ToString().ToUpper() + "." + lastestIndex + ".log";
                        }
                    }
                }
            }

            logMessage = DateTime.Now.ToString("HH:mm:ss") + " " + logMessage;
            var listener = new TextWriterTraceListener(filePath);
            listener.WriteLine(logMessage);
            listener.Flush();
            listener.Close();
        }
    }
}
