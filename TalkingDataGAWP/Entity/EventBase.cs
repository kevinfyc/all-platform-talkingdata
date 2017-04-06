namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    internal abstract class EventBase
    {
        [CompilerGenerated]
        private MyJsonDic _eventDic;
        [DataMember]
        public string eventID = string.Empty;
        [DataMember]
        public long eventOccurTime;

        public EventBase(string eventID)
        {
            this.eventID = eventID;
            DateTime time = new DateTime(0x7b2, 1, 1, 0, 0, 0);
            this.eventOccurTime = (DateTime.UtcNow.Ticks - time.Ticks) / 0x2710L;
            this.eventDic = new MyJsonDic();
        }

        public EventBase eventDataAppendItem(string key, object value)
        {
            if (value != null)
            {
                this.eventDic.Add(key, value);
            }
            return this;
        }

        protected abstract void initializeEventCustomizeMap();
        public bool isEventCountable()
        {
            return false;
        }

        public string toJsonString()
        {
            this.initializeEventCustomizeMap();
            object[] objArray1 = new object[] { "{\"eventID\":\"", this.eventID, "\",\"eventOccurTime\":", this.eventOccurTime, ",\"eventData\":", this.eventDic.toJsonString(), "}" };
            return string.Concat(objArray1);
        }

        protected MyJsonDic eventDic
        {
            [CompilerGenerated]
            get
            {
                return this._eventDic;
            }
            [CompilerGenerated]
            set
            {
                this._eventDic = value;
            }
        }
    }
}

