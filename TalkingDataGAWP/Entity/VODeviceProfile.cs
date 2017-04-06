namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Runtime.Serialization;
    using TalkingDataGAWP.controllers;

    [DataContract]
    internal class VODeviceProfile
    {
        private static VODeviceProfile _instance;
        [DataMember]
        public string apnName = string.Empty;
        [DataMember]
        public string apnOperator = string.Empty;
        [DataMember]
        public bool apnProxy;
        [DataMember]
        public string carrier = string.Empty;
        [DataMember]
        public int cid;
        [DataMember]
        public string country = string.Empty;
        [DataMember]
        public string deviceId = string.Empty;
        [DataMember]
        public bool isJailBroken;
        [DataMember]
        public string language = string.Empty;
        [DataMember]
        public double lat;
        [DataMember]
        public double lng;
        [DataMember]
        public string manufacture = string.Empty;
        [DataMember]
        public string mobileModel = string.Empty;
        [DataMember]
        public string networkOperator = string.Empty;
        [DataMember]
        public int osSdkVersion;
        [DataMember]
        public string osVersion = string.Empty;
        [DataMember]
        public string pixel = string.Empty;
        [DataMember]
        public string simOperator = string.Empty;
        [DataMember]
        public string tdudid = string.Empty;
        [DataMember]
        public int timezone = 8;
        [DataMember]
        public string wap = string.Empty;

        private VODeviceProfile()
        {
            DeviceProfileController.getLocation(out this.lat, out this.lng);
            this.carrier = "";// DeviceNetworkInformation.get_CellularMobileOperator();
            this.deviceId = DeviceProfileController.getDeviceID();
            this.mobileModel = DeviceProfileController.getMobileModel();
            this.osSdkVersion = Environment.OSVersion.Version.Major;
            this.osVersion = Environment.OSVersion.Version.ToString();
            DeviceProfileController.getPixelMetric(this);
        }

        public static VODeviceProfile getInstance()
        {
            if (_instance == null)
            {
                _instance = new VODeviceProfile();
            }
            return _instance;
        }
    }
}

