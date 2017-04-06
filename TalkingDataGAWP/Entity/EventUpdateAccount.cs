namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Runtime.Serialization;
    using TalkingDataGAWP;

    [DataContract]
    internal class EventUpdateAccount : EventBase
    {
        private string mAccount;
        private string mAccountId;
        private int mAccountType;
        private int mAge;
        private string mGameServer;
        private string mGameSessionID;
        private int mGender;
        private int mLevel;

        public EventUpdateAccount(string gameSessionID, TDGAAccount account) : base("G7")
        {
            this.mGameSessionID = gameSessionID;
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
            base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mAccountId).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("sex", this.mGender).eventDataAppendItem("account", this.mAccount).eventDataAppendItem("accountType", this.mAccountType).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("age", this.mAge);
        }
    }
}

