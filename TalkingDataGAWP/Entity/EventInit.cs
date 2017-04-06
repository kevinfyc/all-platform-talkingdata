namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    internal class EventInit : EventBase
    {
        public EventInit() : base("G2")
        {
        }

        protected override void initializeEventCustomizeMap()
        {
        }
    }
}

