using System.IO;
using Newtonsoft.Json;

namespace RDCManager.Models
{
    public class JsonFileAccess : IFileAccess
    {
        public T Read<T>(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
        }

        public void Write<T>(string fileName, T item)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.Write(JsonConvert.SerializeObject(item));
            }
        }
    }
}