namespace TalkingDataGAWP.controllers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using TalkingDataGAWP.command;
    using TalkingDataGAWP.Entity;

    internal class TDGAEventListManager
    {
        private static List<string> g_EventList = null;
        internal static string glFileName = "tdga_talkingdata_file";
        static TVR.SaveTool m_saveTool = null;
        static TVR.SaveTool saveTool
        {
            get
            {
                if(m_saveTool == null)
                {
                    m_saveTool = new TVR.SaveTool();
                    m_saveTool.Initialize();
                    m_saveTool.SetPath(TalkingDataGA.idf.event_path, glFileName);
                }

                return m_saveTool;
            }
        }

        public static void addEvent(EventBase mevent)
        {
            init();
            string msg = mevent.toJsonString();
            Debugger.Log(msg);
            g_EventList.Add(msg);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void addEvent(EventBase mevent, bool sendRightNow)
        {
            init();
            string msg = mevent.toJsonString();
            Debugger.Log(msg);
            g_EventList.Add(msg);
            if (sendRightNow)
            {
                TDGAHttpManager.getInstance().sendDataRightNow();
            }
        }

        public static int eventCount()
        {
            init();
            return g_EventList.Count;
        }

        public static void init()
        {
            if (g_EventList == null)
            {
                g_EventList = loadEventList();
            }
        }

        public static List<string> loadEventList()
        {
            TVR.JsonList<string> ret;
            if (saveTool.ReadArchive<TVR.JsonList<string>>(glFileName, out ret))
            {
                return ret.value;
            }
            return new List<string>();
        }

        public static void saveEventList()
        {
            Monitor.Enter(g_EventList);
            if (g_EventList != null)
            {
                saveTool.WriteArchive(glFileName, new TVR.JsonList<string>(g_EventList));
            }
            g_EventList.Clear();
            Monitor.Exit(g_EventList);
        }

        public static void sendEventFaild()
        {
            init();
            g_EventList.AddRange(loadEventList());
        }

        public static void sendEventSuccess()
        {
            saveTool.DeleteArchive(glFileName);
        }
    }
}

