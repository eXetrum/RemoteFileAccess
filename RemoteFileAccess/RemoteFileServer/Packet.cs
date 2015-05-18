using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RemoteFileServer
{
    // Тип сообщения
    public enum MSG_TYPE : int
    {
        // Получить содержимое текущей папки
        LIST = 0,
        // Сменить дирректорию
        CWD,
        // Задать атрибуты
        SET_ATTRIBUTE,
        // Поиск файла
        SEARCH
    }
    // Укажем что объекты типа Packet сериализуемые
    [Serializable]
    public class Packet
    {
        // Кодировка
        public static Encoding encoding = new UTF8Encoding();
        // Данные в пакете
        public byte[] data { get; private set; }   
        // Тип сообщения
        public MSG_TYPE msg { get; private set; }
        // Конструктор
        public Packet(MSG_TYPE msg, byte[] data)
        {
            this.msg = msg;
            this.data = data;
        }
        // Методы серриализации пакета в байты и наоборот
        #region Convert Packet to bytes <=> bytes to Packet
        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
        #endregion
    }
}
