using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TalkingDataGAWP.controllers
{
    public class iDeviceInterface
    {
        public string event_path;
        public string setting_path;
        public string account_path;
        public string preference_path;

        public virtual string GetAPPPropery()
        {
            return string.Empty;
        }

        public virtual string GetAppVersion()
        {
            return string.Empty;
        }

        public virtual string GetDeviceID()
        {
            return string.Empty;
        }

        public virtual void GetLocation(out double lat, out double lng)
        {
            lat = 0;
            lng = 0;
        }

        public virtual string GetDeviceModel()
        {
            return "";
        }
    }
}
