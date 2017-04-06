namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Runtime.Serialization;
    using TalkingDataGAWP;

    [DataContract]
    internal class EventReward : EventBase
    {
        private string mGameServer;
        private string mGameSessionID;
        private int mLevel;
        private string mMission;
        private string mReason;
        private string mUserID;
        private double mVirtualCurrencyAmount;

        public EventReward(string sessionId, TDGAAccount account, string mission, double virtualCurrencyAmount, string reason) : base("G15")
        {
            this.mGameSessionID = sessionId;
            this.mUserID = account.accountID;
            this.mLevel = account.level;
            this.mGameServer = account.gameServer;
            this.mMission = mission;
            this.mVirtualCurrencyAmount = virtualCurrencyAmount;
            this.mReason = reason;
        }

        protected override void initializeEventCustomizeMap()
        {
            base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mUserID).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("mission", this.mMission).eventDataAppendItem("virtualCurrencyAmount", this.mVirtualCurrencyAmount).eventDataAppendItem("reason", this.mReason);
        }
    }
}

