// using UnityEngine;
// using FGUFW;
// using System.IO;
// using System;
// using System.Threading;
// using System.Text;

// namespace FGUFW.ULog
// {
//     public static class Log
//     {
//         public const string RUNTIME_BEGIN = ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>";
//         public const string RUNTIME_END = "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<";
//         public const int WRITE_FILE_CACHE_LENGTH = 128;

//         private static LogInfoItem[] writeFileCache;
//         private static int writeFileCacheIndex;
//         private static object writeFileLock = new object();

//         public static string LogFolderPath => $"{Application.persistentDataPath}/ULog";
//         public static string CurrentLogFilePath{get;private set;}

//         [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
//         private static void init()
//         {
//             Application.logMessageReceivedThreaded += onUnityLogMessage;
//             Application.quitting += onApplicationQuit;
//             //code
//             writeFileCache = new LogInfoItem[WRITE_FILE_CACHE_LENGTH];
//             writeFileCacheIndex = 0;

//             if(!Directory.Exists(LogFolderPath))Directory.CreateDirectory(LogFolderPath);
//             CurrentLogFilePath = $"{LogFolderPath}/{DateTime.Now.ToString("yyyyMMdd")}.log";
//         }
        

//         private static void onApplicationQuit()
//         {
//             Application.logMessageReceivedThreaded -= onUnityLogMessage;
//             Application.quitting -= onApplicationQuit;
//             //code
            
//         }

//         static bool quit = false;
//         private static void onUnityLogMessage(string condition, string stackTrace, LogType type)
//         {
//             if(!quit)
//             {
//                 quit = true;
//                 Debug.Log(type);
//             }
//             return;
//             lock(writeFileLock)
//             {
//                 var logInfoItem = new LogInfoItem();
//                 logInfoItem.Type = type;
//                 logInfoItem.Time = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss:ffff");
//                 logInfoItem.ThreadID = Thread.CurrentThread.ManagedThreadId;
//                 logInfoItem.Message = condition;
//                 logInfoItem.StackTrace = stackTrace;
//                 writeFileCache[writeFileCacheIndex++]=logInfoItem;
//             }
//         }


//         private static void logBegin()
//         {
//             StreamWriter sw = new StreamWriter(CurrentLogFilePath,true,Encoding.UTF8);
//             sw.WriteLine(RUNTIME_BEGIN);
//             sw.Close();
//         }

//         private static void logEnd()
//         {
//             writeLogFile();
//             StreamWriter sw = new StreamWriter(CurrentLogFilePath,true,Encoding.UTF8);
//             sw.WriteLine(RUNTIME_END);
//             sw.Close();
//         }

//         private static void writeLogFile()
//         {
//             int length = writeFileCacheIndex;
//             if(length==0)return;
//             StreamWriter sw = new StreamWriter(CurrentLogFilePath,true,Encoding.UTF8);
//             for (int i = 0; i < length; i++)
//             {
//                 sw.WriteLine(writeFileCache[i].ToString());
//             }
//             sw.Close();
//         }

//         public static void d(object obj,string module,string coder,string flag="*")
//         {
//             int index = writeFileCacheIndex;
//             Debug.Log(obj);
//             writeFileCache[index].Module = module;
//             writeFileCache[index].Coder = coder;
//             writeFileCache[index].Flag = flag;
//         }

//         private static void log(int type, object obj,string module,string coder,string flag)
//         {
//             lock (writeFileLock)
//             {
//                 int index = writeFileCacheIndex;
                
//             }
//         }



//     }

//     public struct LogInfoItem
//     {

//         /// <summary>
//         /// 时间戳 yyyy/MM/dd hh:mm:ss:ffff
//         /// </summary>
//         public string Time;

//         /// <summary>
//         /// 线程Id
//         /// </summary>
//         public int ThreadID;

//         /// <summary>
//         /// 功能模块
//         /// </summary>
//         public string Module;

//         /// <summary>
//         /// 编写者
//         /// </summary>
//         public string Coder;

//         /// <summary>
//         /// 标记
//         /// </summary>
//         public string Flag;

//         /// <summary>
//         /// 信息
//         /// </summary>
//         public string Message;

//         /// <summary>
//         /// 堆栈
//         /// </summary>
//         public string StackTrace;

//         /// <summary>
//         /// 日志级别
//         /// </summary>
//         public LogType Type;

//         public override string ToString()
//         {
//             StringBuilder sb = new StringBuilder();
//             sb.AppendLine(Time);
//             sb.AppendLine(ThreadID.ToString());
//             sb.AppendLine(string.IsNullOrEmpty(Module)?"*":Module);
//             sb.AppendLine(string.IsNullOrEmpty(Coder)?"*":Coder);
//             sb.AppendLine(string.IsNullOrEmpty(Flag)?"*":Flag);
//             sb.AppendLine("--------");
//             sb.AppendLine(Message);
//             sb.AppendLine(StackTrace);
//             sb.AppendLine(Type.ToString());
//             return sb.ToString();
//         }

//         public static LogInfoItem Pares(string[] lines,ref int index)
//         {
//             var item = new LogInfoItem();
//             item.Time = lines[index++];
//             item.ThreadID = lines[index++].ToInt32();
//             item.Module = lines[index++];
//             item.Coder = lines[index++];
//             item.Flag = lines[index++];
//             index++;
//             item.Message = lines[index++];
//             item.StackTrace = lines[index++];
//             item.Type = lines[index++].ToEnum<LogType>();
//             return item;
//         }

//     }

// }