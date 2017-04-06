namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Runtime.Serialization;
    using TalkingDataGAWP;

    [DataContract]
    internal class EventPurchase : EventBase
    {
        private string mGameServer;
        private string mGameSessionID;
        private string mItemId;
        private int mItemNumber;
        private int mLevel;
        private string mMission;
        private string mUserID;
        private double mVirtualCurrency;

        public EventPurchase(string sessionId, TDGAAccount account, string mission, double virtualCurrency, string itemId, int itemNumber) : base("G10")
        {
            this.mGameSessionID = sessionId;
            this.mUserID = account.accountID;
            this.mLevel = account.level;
            this.mGameServer = account.gameServer;
            this.mMission = mission;
            this.mVirtualCurrency = virtualCurrency;
            this.mItemId = itemId;
            this.mItemNumber = itemNumber;
        }

        protected override void initializeEventCustomizeMap()
        {
            base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mUserID).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("mission", this.mMission).eventDataAppendItem("virtualCurrencyAmount", this.mVirtualCurrency).eventDataAppendItem("itemid", this.mItemId).eventDataAppendItem("itemnumber", this.mItemNumber);
        }
    }
}

