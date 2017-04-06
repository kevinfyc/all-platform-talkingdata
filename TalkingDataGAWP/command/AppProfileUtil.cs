namespace TalkingDataGAWP.command
{
    using System;
    using System.Reflection;
    using System.Xml;
    using System.Xml.Linq;

    internal class AppProfileUtil
    {
        public static string GetAPPPropery(string Key)
        {
            return TalkingDataGAWP.TalkingDataGA.idf.GetAPPPropery();
        }

        public static string getAppVersion()
        {
            return TalkingDataGAWP.TalkingDataGA.idf.GetAppVersion();
        }

        public static long GetInstallionTime()
        {
            return 0L;
        }

        public static string GetPackageName()
        {
            try
            {
                return Assembly.GetCallingAssembly().GetName().Name;
            }
            catch (Exception exception)
            {
                Debugger.Log("GetPackageName: " + exception.Message);
                return "Un_known";
            }
        }

        public static long GetPurchaseTime()
        {
            return 0L;
        }

        public static bool isCrack()
        {
            return false;
        }
    }
}

