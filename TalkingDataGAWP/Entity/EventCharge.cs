namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Runtime.Serialization;
    using TalkingDataGAWP;

    [DataContract]
    internal class EventCharge : EventBase
    {
        private bool isInitialize;
        private double mCurrencyAmount;
        private string mCurrencyType;
        private string mGameServer;
        private string mGameSessionID;
        private string mIapId;
        private int mLevel;
        private string mMission;
        private string mOrderId;
        private string mPaymentType;
        private ChargeStatus mStatus;
        private string mUserID;
        private double mVirtualCurrency;

        public EventCharge(string sessionId, TDGAAccount account, string mission, string orderId, string iapId, double currencyAmount, string currencyType, double virtualCurrency, string paymentType, ChargeStatus status) : base("G9")
        {
            this.mGameSessionID = sessionId;
            this.mUserID = account.accountID;
            this.mLevel = account.level;
            this.mGameServer = account.gameServer;
            this.mMission = mission;
            this.mOrderId = orderId;
            this.mIapId = iapId;
            this.mCurrencyAmount = currencyAmount;
            this.mCurrencyType = currencyType;
            this.mVirtualCurrency = virtualCurrency;
            this.mPaymentType = paymentType;
            this.mStatus = status;
        }

        protected override void initializeEventCustomizeMap()
        {
            if (!this.isInitialize)
            {
                base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mUserID).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("mission", this.mMission).eventDataAppendItem("orderId", this.mOrderId).eventDataAppendItem("iapId", this.mIapId).eventDataAppendItem("currencyAmount", this.mCurrencyAmount).eventDataAppendItem("virtualCurrencyAmount", this.mVirtualCurrency).eventDataAppendItem("currencyType", this.mCurrencyType).eventDataAppendItem("paymentType", this.mPaymentType).eventDataAppendItem("status", (int) this.mStatus);
                this.isInitialize = true;
            }
            else
            {
                base.eventDic["status"] = 2;
            }
        }

        public enum ChargeStatus
        {
            REQUEST = 1,
            SUCCESS = 2
        }
    }
}

