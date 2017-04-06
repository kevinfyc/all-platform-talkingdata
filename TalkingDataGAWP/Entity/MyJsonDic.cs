namespace TalkingDataGAWP.Entity
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class MyJsonDic : Dictionary<string, object>
    {
        private string stringPrefix = "\"";

        private void addString(string s, StringBuilder sb)
        {
            sb.Append(this.stringPrefix).Append(s).Append(this.stringPrefix);
        }

        public string toJsonString()
        {
            if (base.Keys.Count <= 0)
            {
                return "{}";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (string str in base.Keys)
            {
                this.addString(str, sb);
                sb.Append(":");
                if (base[str].GetType().Equals(typeof(string)))
                {
                    this.addString(base[str].ToString(), sb);
                }
                else if (base[str].GetType().Equals(typeof(Dictionary<string, object>)))
                {
                    Dictionary<string, object> value = base[str] as Dictionary<string, object>;
                    MyJsonDic ret = new MyJsonDic();
                    foreach (var iter in value)
                        ret.Add(iter.Key, iter.Value);

                    string res = ret.toJsonString();
                    sb.Append(res);
                }
                else
                {
                    sb.Append(base[str]);
                }
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1).Append("}");
            return sb.ToString();
        }
    }
}

