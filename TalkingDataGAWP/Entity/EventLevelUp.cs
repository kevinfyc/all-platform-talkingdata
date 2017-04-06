namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Runtime.Serialization;
    using TalkingDataGAWP;

    [DataContract]
    internal class EventLevelUp : EventBase
    {
        private string mGameServer;
        private string mGameSessionID;
        private int mLevel;
        private string mMission;
        private int mPreLevel;
        private long mTimeConsuming;
        private string mUserID;

        public EventLevelUp(string sessionId, TDGAAccount account, string mission, int preLevel, long timeConsuming) : base("G5")
        {
            this.mGameSessionID = sessionId;
            this.mUserID = account.accountID;
            this.mLevel = account.level;
            this.mGameServer = account.gameServer;
            this.mMission = mission;
            this.mTimeConsuming = timeConsuming;
            this.mPreLevel = preLevel;
        }

        protected override void initializeEventCustomizeMap()
        {
            base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mUserID).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("mission", this.mMission).eventDataAppendItem("preLevel", this.mPreLevel).eventDataAppendItem("timeConsuming", this.mTimeConsuming / 0x3e8L);
        }
    }
}

