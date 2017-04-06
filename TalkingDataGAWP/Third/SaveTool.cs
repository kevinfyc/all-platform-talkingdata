////////////////////////////////////////////////////////////////////////
// 
// 作者：Kevin
// 时间：
// 功能：
//
///////////////////////////////////////////////////////////////////////

using System.Collections;

namespace TVR
{

    #region Json for buildin type
    public class JsonInt
    {
        public JsonInt(int _value)
        {
            value = _value;
        }

        public int value;
    }

    public class JsonFloat
    {
        public JsonFloat(float _value)
        {
            value = _value;
        }

        public float value;
    }

    public class JsonLong
    {
        public JsonLong(long _value)
        {
            value = _value;
        }

        public long value;
    }

    public class JsonString
    {
        public JsonString(string _value)
        {
            value = _value;
        }

        public string value;
    }

    public class JsonObject<T>
    {
        public JsonObject(T _value)
        {
            value = _value;
        }

        public T value;
    }

    public class JsonObject
    {
        public JsonObject(object _value)
        {
            value = _value;
        }

        public object value;
    }

    public class JsonArray<T>
    {
        public JsonArray(T[] _value)
        {
            value = _value;
        }

        public T[] value;
    }

    public class JsonList<T>
    {
        public JsonList(System.Collections.Generic.List<T> _value)
        {
            value = _value;
        }

        public System.Collections.Generic.List<T> value;
    }

    public class JsonDictionary<T, S>
    {
        public JsonDictionary(System.Collections.Generic.Dictionary<T, S> _value)
        {
            value = _value;
        }

        public System.Collections.Generic.Dictionary<T, S> value;
    }
    #endregion

    public class SaveTool
    {
        private System.Collections.Generic.Dictionary<string, string> m_pool = new System.Collections.Generic.Dictionary<string, string>();
        private string m_filepath;

        private string m_data = "";

        private StringFast m_stringFast = new StringFast(1000);

        private bool m_isDirty = false;

        private float m_saveCheckTime = 0;

        const float SAVE_CHECK_TIME = 1.0f;

        bool m_IsInit = false;
#if UNITY_PS4 && !UNITY_EDITOR
        Sony.PS4.SavedGame.SaveLoad.SavedGameSlotParams SaveParams = new Sony.PS4.SavedGame.SaveLoad.SavedGameSlotParams();
#endif

        public bool IsInit
        {
            get
            {
                return m_IsInit;
            }
        }

        public void Initialize()
        {
            BootData();
            decode2map();
            m_IsInit = true;
        }

        public void SetPath(string path, string filename)
        {
            m_filepath = path + @"\" + filename;
        }


        private bool WriteFile(string str)
        {
            if (!m_IsInit)
            {
                return false;
            }
            System.IO.FileStream fs = new System.IO.FileStream(m_filepath, System.IO.FileMode.Create);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
            try
            {
                sw.Write(str);
            }
            catch (System.Exception e)
            {
                return false;
            }

            sw.Close();
            fs.Close();
            return true;
        }

        private bool BootData()
        {
            if (!System.IO.File.Exists(m_filepath))
            {
                return false;
            }

            System.IO.FileStream fs = new System.IO.FileStream(m_filepath, System.IO.FileMode.Open);
            System.IO.StreamReader sr = new System.IO.StreamReader(fs, System.Text.Encoding.Default);

            try
            {
                lock (m_data)
                {
                    m_data = sr.ReadToEnd();
                }
            }
            catch (System.Exception e)
            {
                return false;
            }

            sr.Close();
            fs.Close();
            return true;
        }

        private void encode2str()
        {
            lock (m_stringFast)
            {
                m_stringFast.Clear();
            }

            var enumerator = m_pool.GetEnumerator();
            while (enumerator.MoveNext())
            {
                lock (m_stringFast)
                {
                    m_stringFast.Append(enumerator.Current.Key);
                    m_stringFast.Append("<->");
                    m_stringFast.Append(enumerator.Current.Value);
                    m_stringFast.Append("=-=");
                }
            }
            lock (m_data)
            {
                m_data = m_stringFast.ToString();
            }
        }

        private void decode2map()
        {
            lock (m_pool)
            {
                m_pool.Clear();
            }
            string[] resultString = System.Text.RegularExpressions.Regex.Split(m_data, "=-=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            for (int i = 0; i < resultString.Length; i++)
            {
                string str = resultString[i];
                var kv = System.Text.RegularExpressions.Regex.Split(str, "<->", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (kv.Length != 2)
                    continue;

                lock (m_pool)
                {
                    m_pool[kv[0]] = kv[1];
                }
            }
        }

        /// <summary>
        /// 写存档
        /// </summary>
        /// <param name="key"> 用于标志存档的key，可通过key读取存档 </param>
        /// <param name="value"> 存档的内容 </param>
        /// <param name="isForce"> 是否要覆盖存储 </param>
        /// <param name="isSave"> 是否保存 保存会产生IO </param>
        /// <returns> true 代表成功 反之失败 </returns>
        public bool WriteArchive(string key, object value, bool isForce = true)
        {
            if (!m_IsInit)
            {
                return false;
            }
            string ret;
            if (!isForce)
            {
                if (m_pool.TryGetValue(key, out ret))
                    return false;
            }

            ret = TalkingDataGAWP.command.JsonUtil.objectToString(value);

            lock (m_pool)
            {
                m_pool[key] = ret;

                Save();
            }

            if (!m_isDirty)
            {
                m_isDirty = true;
                m_saveCheckTime = SAVE_CHECK_TIME;
            }

            return true;
        }

        /// <summary>
        /// 读取存档 内存操作不会有IO
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"> 存储时候的key </param>
        /// <param name="value"> 存档内容 </param>
        /// <returns> true 代表成功 反之失败 </returns>
        public bool ReadArchive<T>(string key, out T value)
        {
            if (!m_IsInit)
            {
                value = default(T);
                return false;
            }

            value = default(T);

            string ret;
            if (!m_pool.TryGetValue(key, out ret))
                return false;

            value = TalkingDataGAWP.command.JsonUtil.stringToObject<T>(ret);

            return true;
        }

        public bool DeleteArchive(string key)
        {
            string ret;
            if (!m_pool.TryGetValue(key, out ret))
                return false;

            lock (m_pool)
            {
                m_pool.Remove(key);

                Save();
            }

            return true;
        }

        /// <summary>
        /// 保存到硬盘 IO操作
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            encode2str();

            return WriteFile(m_data);
        }

        /// <summary>
        /// 清除存档
        /// </summary>
        /// <returns></returns>
        public bool ClearArchive()
        {
            m_pool.Clear();

            return Save();
        }
    }
}