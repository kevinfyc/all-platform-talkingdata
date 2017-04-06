namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using TalkingDataGAWP.command;

    internal class VOAppProfile
    {
        [CompilerGenerated]
        private string _appDisplayName;
        [CompilerGenerated]
        private string _appPackageName;
        [CompilerGenerated]
        private string _appVersionName;
        [CompilerGenerated]
        private long _installationTime;
        [CompilerGenerated]
        private bool _isCracked;
        [CompilerGenerated]
        private string _partner;
        [CompilerGenerated]
        private long _purchaseTime;
        [CompilerGenerated]
        private string _sdkType;
        [CompilerGenerated]
        private string _sdkVersion;
        [CompilerGenerated]
        private string _sequenceNumber;
        private static VOAppProfile instance;

        private VOAppProfile()
        {
            this.appPackageName = AppProfileUtil.GetPackageName();
            this.appVersionName = AppProfileUtil.GetAPPPropery("Version");
            this.installationTime = AppProfileUtil.GetInstallionTime();
            this.appDisplayName = AppProfileUtil.GetAPPPropery("Title");
            this.sdkType = "wp_Native_SDK";
            this.sdkVersion = Constants.SDK_VERSION;
            this.isCracked = AppProfileUtil.isCrack();
        }

        public static VOAppProfile getInstance()
        {
            if (instance == null)
            {
                instance = new VOAppProfile();
            }
            return instance;
        }

        public string getJsonString()
        {
            return JsonUtil.objectToString(this);
        }

        public string appDisplayName
        {
            [CompilerGenerated]
            get
            {
                return this._appDisplayName;
            }
            [CompilerGenerated]
            set
            {
                this._appDisplayName = value;
            }
        }

        public string appPackageName
        {
            [CompilerGenerated]
            get
            {
                return this._appPackageName;
            }
            [CompilerGenerated]
            set
            {
                this._appPackageName = value;
            }
        }

        public string appVersionName
        {
            [CompilerGenerated]
            get
            {
                return this._appVersionName;
            }
            [CompilerGenerated]
            set
            {
                this._appVersionName = value;
            }
        }

        public long installationTime
        {
            [CompilerGenerated]
            get
            {
                return this._installationTime;
            }
            [CompilerGenerated]
            set
            {
                this._installationTime = value;
            }
        }

        public bool isCracked
        {
            [CompilerGenerated]
            get
            {
                return this._isCracked;
            }
            [CompilerGenerated]
            set
            {
                this._isCracked = value;
            }
        }

        public string partner
        {
            [CompilerGenerated]
            get
            {
                return this._partner;
            }
            [CompilerGenerated]
            set
            {
                this._partner = value;
            }
        }

        public long purchaseTime
        {
            [CompilerGenerated]
            get
            {
                return this._purchaseTime;
            }
            [CompilerGenerated]
            set
            {
                this._purchaseTime = value;
            }
        }

        public string sdkType
        {
            [CompilerGenerated]
            get
            {
                return this._sdkType;
            }
            [CompilerGenerated]
            set
            {
                this._sdkType = value;
            }
        }

        public string sdkVersion
        {
            [CompilerGenerated]
            get
            {
                return this._sdkVersion;
            }
            [CompilerGenerated]
            set
            {
                this._sdkVersion = value;
            }
        }

        public string sequenceNumber
        {
            [CompilerGenerated]
            get
            {
                return this._sequenceNumber;
            }
            [CompilerGenerated]
            set
            {
                this._sequenceNumber = value;
            }
        }
    }
}

