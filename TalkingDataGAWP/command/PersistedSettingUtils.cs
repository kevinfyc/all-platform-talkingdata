namespace TalkingDataGAWP.command
{
    using System;
    using System.Collections.Generic;
    using System.IO.IsolatedStorage;

    internal class PersistedSettingUtils
    {
        private static readonly Dictionary<string, object> _cache = new Dictionary<string, object>();
        private static TVR.SaveTool m_saveTool = null;
        private static readonly string setting_filename = "setting";
        private static TVR.SaveTool saveTool
        {
            get
            {
                if(m_saveTool == null)
                {
                    m_saveTool = new TVR.SaveTool();
                    m_saveTool.Initialize();
                    m_saveTool.SetPath(TalkingDataGA.idf.setting_path, setting_filename);
                }

                return m_saveTool;
            }
        }


        public static void AddOrUpdateValue(string key, object value)
        {
            bool flag = false;
            try
            {
                TVR.JsonObject ret;
                if (saveTool.ReadArchive<TVR.JsonObject>(key, out ret))
                {
                    if (ret.value != value)
                    {
                        saveTool.WriteArchive(key, new TVR.JsonObject(value));
                        flag = true;
                    }
                }
                else
                {
                    saveTool.WriteArchive(key, new TVR.JsonObject(value));
                    flag = true;
                }
            }
            catch (KeyNotFoundException)
            {
                saveTool.WriteArchive(key, new TVR.JsonObject(value));
                flag = true;
            }
            catch (ArgumentException)
            {
                saveTool.WriteArchive(key, new TVR.JsonObject(value));
                flag = true;
            }
            if (flag)
            {
                Save();
                UpdateCache(key, value);
            }
        }

        private static void Save()
        {
            saveTool.Save();
        }

        private static void UpdateCache(string key, object value)
        {
            Dictionary<string, object> dictionary = _cache;
            lock (dictionary)
            {
                if (_cache.ContainsKey(key))
                {
                    _cache[key] = value;
                }
                else
                {
                    _cache.Add(key, value);
                }
            }
        }
    }
}

