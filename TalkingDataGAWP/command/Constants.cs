namespace TalkingDataGAWP.command
{
    using System;

    internal class Constants
    {
        public const string ACCOUNT_NAME = "account";
        public const string ACCOUNT_TYPE = "accountType";
        public const string AGE = "age";
        public const string CAUSE = "cause";
        internal static int COARSE_PACKAGE_SIZE = 0x14000;
        public const string CURRENCY_AMOUNT = "currencyAmount";
        public const string CUSTOMER_ACTION_DATA = "actionData";
        public const string CUSTOMER_ACTION_ID = "actionID";
        internal static bool debug_mode = true;
        public const string DURATION = "duration";
        internal static int File_PACKAGE_SIZE = 0x100000;
        public const string GAME_SERVER = "gameServer";
        public const string GAME_SESSION_START = "gameSessionStart";
        public const string GAME_SESSIONID = "gameSessionID";
        public const string IAP_ID = "iapId";
        public const string INTERVAL = "interval";
        public const string ITEM_ID = "itemid";
        public const string ITEM_NUMBER = "itemnumber";
        public const string LEVEL = "level";
        internal static TimeSpan MaxWaitTime = new TimeSpan(0, 0, 3, 0);
        public const string MISSION_ID = "mission";
        public const string ORDER_ID = "orderId";
        internal static readonly string PartnerId = "TD";
        public const string PAYMENT_TYPE = "paymentType";
        public const string PRE_LEVEL = "preLevel";
        public const string PUBLISH_CHANNEL = "TDGA";
        public const string REWARD_REASON = "reason";
        internal static readonly string SDK_TYPE = "wphone+";
        internal static readonly string SDK_VERSION = "2.0.7";
        internal static readonly string SEND_LOG_URL = "http://gf.tenddata.com/g/d";
        internal static TimeSpan sessionContinueInterval = new TimeSpan(0, 0, 30);
        public const string SEX = "sex";
        public const string STATUS = "status";
        public const string TIME_CONSUMING = "timeConsuming";
        public const string USER_ID = "userID";
        public const string VIRTUAL_CURRENCY = "virtualCurrencyAmount";
        public const string VIRTUAL_CURRENCY_TYPE = "currencyType";
    }
}

