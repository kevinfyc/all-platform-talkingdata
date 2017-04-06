namespace TalkingDataGAWP.command
{
    using System;
    using System.Diagnostics;

    internal class Debugger
    {
        private static readonly LogLevel MinLogLevel = LogLevel.Debug;

        public static void Assert(bool condition)
        {
            Assert(condition, "Default Failure Message.");
        }

        public static void Assert(bool condition, string msg)
        {
            if (!condition)
            {
                Log(msg + " failed");
            }
        }

        private static string GetCaller()
        {
            StackTrace trace = new StackTrace();
            StackFrame frame = null;
            for (int i = 1; i < trace.FrameCount; i++)
            {
                if (!trace.GetFrame(i).GetMethod().DeclaringType.FullName.Contains(trace.GetFrame(0).GetMethod().DeclaringType.FullName))
                {
                    frame = trace.GetFrame(i);
                    break;
                }
            }
            if (frame == null)
            {
                frame = trace.GetFrame(trace.FrameCount - 1);
            }
            return (frame.GetMethod().DeclaringType.FullName + "." + frame.GetMethod().Name);
        }

        public static void Log(string msg)
        {
            Log(LogLevel.Debug, msg);
        }

        public static void Log(LogLevel logLevel, string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                LogLevel minLogLevel = MinLogLevel;
                //UnityEngine.Debug.Log(msg);
            }
        }

        public enum LogLevel
        {
            Trace,
            Debug,
            Error
        }
    }
}

