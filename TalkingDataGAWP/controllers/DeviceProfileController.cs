namespace TalkingDataGAWP.controllers
{
    using System;
    using System.Runtime.InteropServices;
    using TalkingDataGAWP.command;
    using TalkingDataGAWP.Entity;

    internal class DeviceProfileController
    {
        private static iDeviceInterface m_deviceInterface = TalkingDataGA.idf;

        public static string getDeviceID()
        {
            string deviceid = TDGAPreference.getDeviceID();
            if ((deviceid == null) || (deviceid.Length <= 10))
            {
                deviceid = m_deviceInterface.GetDeviceID();
                TDGAPreference.setDeviceID(deviceid);
            }
            return deviceid;
        }

        public static void getLocation(out double lat, out double lng)
        {
            lat = 0.0;
            lng = 0.0;
            //try
            //{
            //    GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(0);
            //    if (watcher.get_Permission() != 1)
            //    {
            //        Debugger.Log("missing permission ID_CAP_LOCATION");
            //    }
            //    else
            //    {
            //        GeoCoordinate coordinate = watcher.get_Position().get_Location();
            //        if (!coordinate.get_IsUnknown())
            //        {
            //            lat = coordinate.get_Latitude();
            //            lng = coordinate.get_Latitude();
            //        }
            //    }
            //}
            //catch (Exception exception1)
            //{
            //    lat = 0.0;
            //    lng = 0.0;
            //    Debugger.Log(exception1.Message);
            //    Debugger.Log("maybe missing permission ID_CAP_LOCATION");
            //}
        }

        public static string getMobileModel()
        {
            return "";
        }

        public static void getPixelMetric(VODeviceProfile device)
        {
        }
    }
}

