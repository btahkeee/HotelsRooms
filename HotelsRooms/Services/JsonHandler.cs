using HotelsRooms.Interfaces;
using Newtonsoft.Json;

namespace HotelsRooms.Services
{
    public class JsonHandler : IJsonHandler
    {
        public T LoadJsonFile<T>(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}
