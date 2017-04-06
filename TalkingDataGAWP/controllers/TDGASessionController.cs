namespace TalkingDataGAWP.controllers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using TalkingDataGAWP;
    using TalkingDataGAWP.command;
    using TalkingDataGAWP.Entity;

    internal class TDGASessionController
    {
        private const long NEW_SESSION_INTERVAL = 0x7530L;
        public static string sGameSessionId = "";

        internal static void changeAccount(TDGAAccount old, TDGAAccount current)
        {
            restartSession(old, current);
        }

        private static string createSessionID()
        {
            DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0);
            long num = (DateTime.UtcNow.Ticks - time.Ticks) / 0x2710L;
            return MD5Core.GetHashString(Encoding.UTF8.GetBytes(num.ToString()));
        }

        private static void generateGameSessionId(long time, int interval, TDGAAccount account)
        {
            sGameSessionId = createSessionID();
            TDGAPreference.setActiveSessionID(sGameSessionId);
            TDGAPreference.setActiveSessionStartTime(time);
            TDGAPreference.setActiveSessionEndTime(0L);
            TDGAPreference.setSessionStartCurrentTimeMillis();
            TDGAEventListManager.addEvent(new EventLogin(sGameSessionId, account, interval));
        }

        public static void pauseSession()
        {
            TDGAPreference.setActiveSessionEndTime(DateTimeUtils.getCurrentTime());
            if (TDGAAccount.sAccount != null)
            {
                TDGAAccount.sAccount.updateGameDuration();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void restartSession(TDGAAccount preAccount, TDGAAccount nextAccount)
        {
            long num = TDGAPreference.getActiveSessionStartTime();
            long time = DateTimeUtils.getCurrentTime();
            TDGAEventListManager.addEvent(new EventLogout(TDGAPreference.getActiveSessionId(), preAccount, TDGAPreference.getSessionStartCurrentTimeMillis(), time - num));
            generateGameSessionId(time, 0, nextAccount);
        }

        private static void saveSessionEnd(string sessionId, long start, long end)
        {
            TDGAPreference.setActiveSessionEndTime(end);
            TDGAEventListManager.addEvent(new EventLogout(sessionId, TDGAAccount.sAccount, TDGAPreference.getSessionStartCurrentTimeMillis(), end - start));
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void sessionResume()
        {
            long start = TDGAPreference.getActiveSessionStartTime();
            long end = TDGAPreference.getActiveSessionEndTime();
            string sessionId = TDGAPreference.getActiveSessionId();
            int interval = 0;
            long time = DateTimeUtils.getCurrentTime();
            bool flag = false;
            sGameSessionId = sessionId;
            if (start == 0x364L)
            {
                flag = true;
                interval = 0;
            }
            else if ((((time - end) > 0x7530L) && ((time - start) > 0x7530L)) || (time < start))
            {
                flag = true;
                if (end == 0)
                {
                    saveSessionEnd(sessionId, start, start);
                    interval = 0;
                }
                else
                {
                    saveSessionEnd(sessionId, start, end);
                    interval = ((int) (time - end)) / 0x3e8;
                }
            }
            if (flag)
            {
                generateGameSessionId(time, interval, TDGAAccount.sAccount);
            }
        }
    }
}

