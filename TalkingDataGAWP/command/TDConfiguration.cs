namespace TalkingDataGAWP.command
{
    using System;

    internal class TDConfiguration
    {
        public static string appChannel = string.Empty;
        public static string AppKey = string.Empty;
        public const string BaseDir = "TalkingDataGA";
        internal static int COARSE_PACKAGE_SIZE = 0x14000;
        internal static bool debug_mode = true;
        internal static int File_PACKAGE_SIZE = 0x100000;
        public static bool isNeedReportException = false;
        internal static readonly int MaxEventCount = 0x3e8;
        internal static TimeSpan MaxWaitTime = new TimeSpan(0, 0, 3, 0);
        internal static readonly string SDK_TYPE = SDKTYPE.wphone_8;
        internal static readonly string SEND_LOG_URL = "http://gaandroid.talkingdata.net/g/d?crc=";
        internal static TimeSpan sessionContinueInterval = new TimeSpan(0, 0, 30);
    }
}

