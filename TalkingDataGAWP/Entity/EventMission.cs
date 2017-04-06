namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Runtime.Serialization;
    using TalkingDataGAWP;

    [DataContract]
    internal class EventMission : EventBase
    {
        private string mCause;
        private string mGameServer;
        private string mGameSessionID;
        private int mLevel;
        private string mMission;
        private MissionStatus mStatus;
        private long mTimeConsuming;
        private string mUserID;

        public EventMission(string gameSessionID, TDGAAccount account, string mission, string cause, long timeConsuming, MissionStatus status) : base("G6")
        {
            this.mGameSessionID = gameSessionID;
            this.mUserID = account.accountID;
            this.mLevel = account.level;
            this.mGameServer = account.gameServer;
            this.mMission = mission;
            this.mCause = cause;
            this.mStatus = status;
            this.mTimeConsuming = timeConsuming;
        }

        protected override void initializeEventCustomizeMap()
        {
            base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mUserID).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("mission", this.mMission).eventDataAppendItem("cause", this.mCause).eventDataAppendItem("status", (int) this.mStatus).eventDataAppendItem("timeConsuming", this.mTimeConsuming);
        }

        public enum MissionStatus
        {
            COMPLETED = 2,
            FAILED = 3,
            START = 1
        }
    }
}

