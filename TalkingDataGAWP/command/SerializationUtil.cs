namespace TalkingDataGAWP.command
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    internal class SerializationUtil
    {
        public static T bytesToObject<T>(byte[] array)
        {
            using (MemoryStream stream = new MemoryStream(array))
            {
                return (T) new DataContractJsonSerializer(typeof(T)).ReadObject(stream);
            }
        }

        public static byte[] objectToBytes(object obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new DataContractJsonSerializer(obj.GetType()).WriteObject(stream, obj);
                return stream.ToArray();
            }
        }

        public static string objectToString(object obj)
        {
            byte[] bytes = objectToBytes(obj);
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        public static T stringToObject<T>(string str)
        {
            return bytesToObject<T>(Encoding.UTF8.GetBytes(str));
        }
    }
}

