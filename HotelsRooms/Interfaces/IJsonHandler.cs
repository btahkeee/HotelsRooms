namespace HotelsRooms.Interfaces
{
    public interface IJsonHandler
    {
        public T LoadJsonFile<T>(string filePath);
    }
}
