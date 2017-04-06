namespace TalkingDataGAWP.command
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    internal class JsonUtil
    {
        public static T bytesToObject<T>(byte[] array)
        {
            if ((array == null) || (array.Length == 0))
            {
                return default(T);
            }
            using (MemoryStream stream = new MemoryStream(array))
            {
                return (T) new DataContractJsonSerializer(typeof(T)).ReadObject(stream);
            }
        }

        public static byte[] objectToBytes(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                new DataContractJsonSerializer(obj.GetType()).WriteObject(stream, obj);
                return stream.ToArray();
            }
        }

        public static string objectToString(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            byte[] bytes = objectToBytes(obj);
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        public static T stringToObject<T>(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return default(T);
            }
            return bytesToObject<T>(Encoding.UTF8.GetBytes(str));
        }
    }
}

