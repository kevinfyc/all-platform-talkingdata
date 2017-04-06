namespace TalkingDataGAWP.command
{
    using System;

    internal class SDKTYPE
    {
        public static string wphone_7 = "wphone+";
        public static string wphone_8 = "wphone+";

        public static bool isSDKFor_WP8()
        {
            return TDConfiguration.SDK_TYPE.Equals(wphone_8);
        }
    }
}

