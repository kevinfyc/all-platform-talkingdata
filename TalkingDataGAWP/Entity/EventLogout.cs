namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Runtime.Serialization;
    using TalkingDataGAWP;

    [DataContract]
    internal class EventLogout : EventBase
    {
        private string mGameServer;
        private string mGameSessionID;
        private int mLevel;
        private long mSessionDuration;
        private long mSessionStartTime;
        private string mUserID;

        public EventLogout(string gameSessionID, TDGAAccount account, long start, long dur) : base("G4")
        {
            this.mGameSessionID = gameSessionID;
            this.mUserID = account.accountID;
            this.mLevel = account.level;
            this.mGameServer = account.gameServer;
            this.mSessionStartTime = start;
            this.mSessionDuration = dur;
        }

        protected override void initializeEventCustomizeMap()
        {
            base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mUserID).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("gameSessionStart", this.mSessionStartTime).eventDataAppendItem("duration", this.mSessionDuration / 0x3e8L);
        }
    }
}

