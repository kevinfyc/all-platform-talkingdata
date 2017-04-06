namespace TalkingDataGAWP.command
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;

    internal class AsyncPostUtils
    {
        private byte[] _requestData;
        private ResponseCallback _responseCallBack;

        public void Post(Uri uri, byte[] requestData, ResponseCallback responseCallBack)
        {
            Debugger.Log(string.Concat(new object[] { "Start Post. URI:", uri.AbsoluteUri, ". RequestDataSize:", requestData.Length }));
            this._requestData = requestData;
            this._responseCallBack = responseCallBack;
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/octet-stream";
            request.BeginGetRequestStream(new AsyncCallback(this.RequestStreamCallback), request);
        }

        private void RequestStreamCallback(IAsyncResult ar)
        {
            Debugger.Log("Start writing request data.");
            HttpWebRequest asyncState = (HttpWebRequest) ar.AsyncState;
            BinaryWriter writer1 = new BinaryWriter(asyncState.EndGetRequestStream(ar));
            writer1.Write(this._requestData);
            writer1.Close();
            Debugger.Log("Call BeginGetResponse.");
            asyncState.BeginGetResponse(new AsyncCallback(this.ResponseCallbackEvent), asyncState);
        }

        private void ResponseCallbackEvent(IAsyncResult ar)
        {
            Debugger.Log("Received response callback. Process response.");
            HttpWebRequest asyncState = ar.AsyncState as HttpWebRequest;
            HttpWebResponse response = null;
            try
            {
                response = asyncState.EndGetResponse(ar) as HttpWebResponse;
                if ((response != null) && (response.StatusCode == HttpStatusCode.OK))
                {
                    Debugger.Log("Post successful.");
                }
                else
                {
                    Debugger.Log("Post failed. Status:" + ((response != null) ? response.StatusCode.ToString() : "") + ". Response:" + ((response != null) ? response.ToString() : ""));
                }
            }
            catch (WebException exception)
            {
                Debugger.Log("Post Failed. Exception:" + exception.StackTrace);
            }
            catch (Exception exception2)
            {
                Debugger.Log("Post Failed. Exception:" + exception2.StackTrace);
            }
            Debugger.Log("Call ResponseCallBack.");
            this._responseCallBack(response);
        }

        public delegate void ResponseCallback(HttpWebResponse response);
    }
}

