namespace TalkingDataGAWP.controllers
{
    using System;
    using System.IO.IsolatedStorage;
    using TalkingDataGAWP.command;

    internal class TDGAPreference
    {
        private static string ACCOUNT_GAME_KEY = "pref.accountgame.key";
        private static string ACCOUNT_ID_KEY = "pref.accountid.key";
        private static string ACTIVE_SESSION_ID_KEY = "TDGApref.activesessionid.key";
        private static string APPLIST_SEND_TIME = "pref.applisttime.key";
        public const long DEFAULT_INIT_TIME = 0L;
        public const long DEFAULT_SESSION_END_TIME = 0L;
        public const long DEFAULT_SESSION_START_TIME = 0x364L;
        private static string DEVICE_ID_KEY = "pref.deviceid.key";
        private static string MESSION_ID = "TDGApref.missionid.key";
        private static string PREF_INIT_KEY = "pref.init.key";
        private static string SESSION_END_TIME_LONG = "TDGApref.session.end.key";
        private static string SESSION_START_CURRENT_TIME_MILLIS = "TDGApref.session.startsystem.key";
        private static string SESSION_START_TIME_LONG = "TDGApref.session.start.key";
        private static string PREFERENCE_FILE = "pref_file";

        private static TVR.SaveTool _saveTool = null;
        private static TVR.SaveTool m_saveTool
        {
            get
            {
                if(_saveTool == null)
                {
                    _saveTool = new TVR.SaveTool();
                    _saveTool.Initialize();
                    _saveTool.SetPath(TalkingDataGA.idf.preference_path, PREFERENCE_FILE);
                }

                return _saveTool;
            }
        }

        public static string getAccountGameServer(string accountid)
        {
            string str = string.Empty;
            TVR.JsonString ret;
            if (m_saveTool.ReadArchive<TVR.JsonString>(accountid + ACCOUNT_GAME_KEY, out ret))
            {
                return ret.value;
            }
            return str;
        }

        public static string getAccountID()
        {
            string str = string.Empty;
            TVR.JsonString ret;
            if (m_saveTool.ReadArchive<TVR.JsonString>(ACCOUNT_ID_KEY, out ret))
            {
                return ret.value;
            }
            return str;
        }

        public static long getActiveSessionEndTime()
        {
            long num = 0L;
            if (m_saveTool == null)
            {
                init();
            }

            TVR.JsonLong ret;
            if (m_saveTool.ReadArchive<TVR.JsonLong>(SESSION_END_TIME_LONG, out ret))
            {
                return ret.value;
            }
            return num;
        }

        public static string getActiveSessionId()
        {
            string str = string.Empty;
            TVR.JsonString ret;
            if (m_saveTool.ReadArchive<TVR.JsonString>(ACTIVE_SESSION_ID_KEY, out ret))
            {
                return ret.value;
            }
            return str;
        }

        public static long getActiveSessionStartTime()
        {
            long num = 0x364L;
            TVR.JsonLong ret;
            if (m_saveTool.ReadArchive<TVR.JsonLong>(SESSION_START_TIME_LONG, out ret))
            {
                return ret.value;
            }
            return num;
        }

        public static string getDeviceID()
        {
            string str = string.Empty;
            TVR.JsonString ret;
            if (m_saveTool.ReadArchive<TVR.JsonString>(DEVICE_ID_KEY, out ret))
            {
                return ret.value;
            }
            return str;
        }

        public static long getInitTime()
        {
            long num = 0L;
            TVR.JsonLong ret;
            if (m_saveTool.ReadArchive<TVR.JsonLong>(PREF_INIT_KEY, out ret))
            {
                return ret.value;
            }
            return num;
        }

        public static string getMissionID()
        {
            string str = string.Empty;
            TVR.JsonString ret;
            if (m_saveTool.ReadArchive<TVR.JsonString>(MESSION_ID, out ret))
            {
                return ret.value;
            }
            return str;
        }

        public static long getSessionStartCurrentTimeMillis()
        {
            long num = 0x364L;
            TVR.JsonLong ret;
            if (m_saveTool.ReadArchive<TVR.JsonLong>(SESSION_START_CURRENT_TIME_MILLIS, out ret))
            {
                return ret.value;
            }
            return num;
        }

        public static void init()
        {
        }

        public static void setAccountGameServer(string accountId, string gameserver)
        {
            setKeyValue(accountId + ACCOUNT_GAME_KEY, gameserver);
        }

        public static void setAccountID(string accountId)
        {
            setKeyValue(ACCOUNT_ID_KEY, accountId);
        }

        public static void setActiveSessionEndTime(long end)
        {
            setKeyValue(SESSION_END_TIME_LONG, end);
        }

        public static void setActiveSessionID(string sid)
        {
            setKeyValue(ACTIVE_SESSION_ID_KEY, sid);
        }

        public static void setActiveSessionStartTime(long start)
        {
            setKeyValue(SESSION_START_TIME_LONG, start);
        }

        public static void setDeviceID(string deviceid)
        {
            setKeyValue(DEVICE_ID_KEY, deviceid);
        }

        public static void setInitTime(long t)
        {
            setKeyValue(PREF_INIT_KEY, t);
        }

        private static void setKeyValue(string key, object value)
        {
            TVR.JsonObject ret = new TVR.JsonObject(value);
            m_saveTool.WriteArchive(key, ret, true);
            m_saveTool.Save();
        }

        public static void setMissionID(string missonId)
        {
            setKeyValue(MESSION_ID, missonId);
        }

        public static void setSessionStartCurrentTimeMillis()
        {
            setKeyValue(SESSION_START_CURRENT_TIME_MILLIS, DateTimeUtils.getCurrentTime());
        }
    }
}

