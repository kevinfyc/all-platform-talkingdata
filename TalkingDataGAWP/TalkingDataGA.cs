namespace TalkingDataGAWP
{
    using System;
    using System.Collections.Generic;
    using TalkingDataGAWP.command;
    using TalkingDataGAWP.controllers;
    using TalkingDataGAWP.Entity;

    public class TalkingDataGA
    {
        internal static bool isAlreadyInit;
        private static string sAppId;
        private static string sPartnerId;
        
        private static iDeviceInterface m_deviceInterface = null;
        public static iDeviceInterface idf
        {
            get
            {
                if (m_deviceInterface == null)
                    m_deviceInterface = new iDeviceInterface();

                return m_deviceInterface;
            }
            set
            {
                m_deviceInterface = value;
            }
        }

        public static void Activated()
        {
            startSession();
        }

        public static void Closing()
        {
            pauseSession();
        }

        public static void Deactivated()
        {
            pauseSession();
        }


        public static string getAppKey()
        {
            return sAppId;
        }

        public static string getDeviceID()
        {
            return TDGAPreference.getDeviceID();
        }

        public static string getPartnerId()
        {
            return sPartnerId;
        }

        public static void init(string appkey, string channel, iDeviceInterface device)
        {
            idf = device;

            if (!isAlreadyInit)
            {
                Debugger.Log("Init SDK Version:" + Constants.SDK_VERSION + "   APPID:" + appkey + "  Channel:" + channel);
                VOAppProfile.getInstance().partner = validChannel(channel);
                VOAppProfile.getInstance().sequenceNumber = appkey;
                VODeviceProfile.getInstance();

                Debugger.Log("finish add handler");
                if (isAppkValided(appkey))
                {
                    sAppId = appkey;
                    sPartnerId = channel;
                    long t = DateTimeUtils.getCurrentTime();
                    TDGAPreference.init();
                    if (TDGAPreference.getInitTime() == 0)
                    {
                        TDGAEventListManager.addEvent(new EventInit());
                        TDGAPreference.setInitTime(t);
                    }
                    TDGAAccount.sAccount = TDGAAccount.getTDGAccount();
                    isAlreadyInit = true;
                    startSession();
                }
                else
                {
                    Debugger.Log("Your appKey is invalid ,please apply for a new one");
                }
            }
        }

        private static bool isAppkValided(string appkey)
        {
            return (!string.IsNullOrEmpty(appkey) && (appkey.Length == 0x20));
        }

        public static void onEvent(string eventid)
        {
            onEvent(eventid, null);
        }

        public static void onEvent(string eventid, Dictionary<string, object> map)
        {
            if ((eventid != null) && !eventid.Trim().Equals(string.Empty))
            {
                TDGAEventListManager.addEvent(new EventCustomEvent(TDGASessionController.sGameSessionId, TDGAAccount.sAccount, eventid, map));
            }
        }

        private static void pauseSession()
        {
            TDGASessionController.pauseSession();
            TDGAHttpManager.getInstance().stopSendDataWithDetalTime();
            TDGAEventListManager.saveEventList();
        }

        private static void startSession()
        {
            TDGASessionController.sessionResume();
            TDGAHttpManager.getInstance().sendDataWithDetalTime();
        }

        private static string validChannel(string channel)
        {
            if (string.IsNullOrEmpty(channel.Trim()))
            {
                channel = "TD_unknown";
            }
            return channel;
        }
    }
}

