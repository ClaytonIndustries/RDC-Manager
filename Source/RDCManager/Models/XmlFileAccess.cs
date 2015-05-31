using System.IO;
using System.Xml.Serialization;

namespace RDCManager.Models
{
    public class XmlFileAccess : IFileAccess
    {
        public void Write<T>(string fileName, T item)
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(T));

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                serialiser.Serialize(writer, item);
            }
        }

        public T Read<T>(string fileName)
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(T));

            using (StreamReader reader = new StreamReader(fileName))
            {
                return (T)serialiser.Deserialize(reader);
            }
        }
    }
}