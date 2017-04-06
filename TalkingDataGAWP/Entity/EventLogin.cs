namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Runtime.Serialization;
    using TalkingDataGAWP;

    [DataContract]
    internal class EventLogin : EventBase
    {
        private string mAccount;
        private string mAccountId;
        private int mAccountType;
        private int mAge;
        private string mGameServer;
        private string mGameSessionID;
        private int mGender;
        private int mInterval;
        private int mLevel;

        public EventLogin(string gameSessionID, TDGAAccount account, int interval) : base("G3")
        {
            this.mGameSessionID = gameSessionID;
            this.mInterval = interval;
            this.mAccountId = account.accountID;
            this.mLevel = account.level;
            this.mGender = (int) account.gender;
            this.mAccountType = (int) account.accountType;
            this.mGameServer = account.gameServer;
            this.mAccount = account.accountName;
            this.mAge = account.age;
        }

        protected override void initializeEventCustomizeMap()
        {
            base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mAccountId).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("sex", this.mGender).eventDataAppendItem("account", this.mAccount).eventDataAppendItem("accountType", this.mAccountType).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("age", this.mAge).eventDataAppendItem("interval", this.mInterval);
        }
    }
}

