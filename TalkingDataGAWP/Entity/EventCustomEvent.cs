namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using TalkingDataGAWP;

    [DataContract]
    internal class EventCustomEvent : EventBase
    {
        private Dictionary<string, object> mActionData;
        private string mActionID;
        private string mGameServer;
        private string mGameSessionID;
        private int mLevel;
        private string mUserID;

        public EventCustomEvent(string gameSessionID, TDGAAccount account, string actionID, Dictionary<string, object> actionData) : base("G8")
        {
            this.mGameSessionID = gameSessionID;
            this.mUserID = account.accountID;
            this.mLevel = account.level;
            this.mGameServer = account.gameServer;
            this.mActionID = actionID;
            this.mActionData = actionData;
            if (this.mActionData == null)
            {
                this.mActionData = new Dictionary<string, object>();
            }
        }

        protected override void initializeEventCustomizeMap()
        {
            base.eventDataAppendItem("gameSessionID", this.mGameSessionID).eventDataAppendItem("userID", this.mUserID).eventDataAppendItem("level", this.mLevel).eventDataAppendItem("gameServer", this.mGameServer).eventDataAppendItem("actionID", this.mActionID).eventDataAppendItem("actionData", this.mActionData);
        }
    }
}

