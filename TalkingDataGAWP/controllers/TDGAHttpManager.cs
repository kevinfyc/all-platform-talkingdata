namespace TalkingDataGAWP.controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;
    using System.Threading;
    using TalkingDataGAWP.command;
    using TalkingDataGAWP.Entity;

    internal class TDGAHttpManager
    {
        private static TDGAHttpManager _manager = new TDGAHttpManager();
        private bool _sending;
        private bool loopRunning;
        private Thread loopThread;

        public void AsynPostResponseCallBack(HttpWebResponse response)
        {
            TDGAHttpManager manager = this;
            lock (manager)
            {
                this._sending = false;
                if ((response != null) && (response.StatusCode == HttpStatusCode.OK))
                {
                    Debugger.Log("send data to server--->success");
                    TDGAEventListManager.sendEventSuccess();
                }
                else
                {
                    TDGAEventListManager.sendEventFaild();
                    Debugger.Log("send data to server--->faild");
                }
            }
        }

        public static TDGAHttpManager getInstance()
        {
            return _manager;
        }

        private string getJsonArray(string appProfile, string deviceProfile, List<string> list)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{\"appProfile\":").Append(appProfile).Append(",").Append("\"deviceProfile\":").Append(deviceProfile).Append(",").Append("\"events\":");
            builder.Append("[");
            for (int i = 0; i < list.Count; i++)
            {
                builder.Append(list[i]);
                if (i < (list.Count - 1))
                {
                    builder.Append(",");
                }
            }
            builder.Append("]").Append("}");
            return builder.ToString();
        }

        public void sendDataRightNow()
        {
            new Thread(new ThreadStart(this.sendEventToServer)).Start();
        }

        public void sendDataWithDetalTime()
        {
            if ((this.loopThread == null) || !this.loopThread.IsAlive)
            {
                this.loopRunning = true;
                this.loopThread = new Thread(new ThreadStart(this.threadRunLoop));
                this.loopThread.Start();
            }
        }

        private void sendEventToServer()
        {
            if (!this.isSending && (TDGAEventListManager.eventCount() > 0))
            {
                TDGAEventListManager.saveEventList();
                List<string> list = TDGAEventListManager.loadEventList();
                string appProfile = JsonUtil.objectToString(VOAppProfile.getInstance());
                string deviceProfile = JsonUtil.objectToString(VODeviceProfile.getInstance());
                string s = this.getJsonArray(appProfile, deviceProfile, list);
                Debugger.Log("send data to server--->" + s);
                var async = new AsyncPostUtils();
                var url = new Uri(TDConfiguration.SEND_LOG_URL, UriKind.Absolute);

                byte[] data;

                if (string.IsNullOrEmpty(s) || s.Length == 0)
                    data = null;
                else
                {
                    var bytes = Encoding.UTF8.GetBytes(s);
                    using(System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        Unity.IO.Compression.GZipStream compressor = null;
                        try
                        {
                            compressor = new Unity.IO.Compression.GZipStream(ms, Unity.IO.Compression.CompressionMode.Compress, true);
                        }
                        catch(Exception e)
                        {
                            var sss = e.Message;
                        }
                        compressor.Write(bytes, 0, bytes.Length);
                        compressor.Close();

                        data = ms.ToArray();
                    }

                }
                var cbk = new AsyncPostUtils.ResponseCallback(this.AsynPostResponseCallBack);
                async.Post(url, data, cbk);
            }
        }

        public void stopSendDataWithDetalTime()
        {
            try
            {
                if (this.loopThread != null)
                {
                    this.loopRunning = false;
                    this.loopThread.Abort();
                    this.loopThread = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void threadRunLoop()
        {
            while (this.loopRunning)
            {
                this.sendEventToServer();
                Thread.Sleep(TDConfiguration.MaxWaitTime);
            }
        }

        public bool isSending
        {
            get
            {
                return this._sending;
            }
        }
    }
}

