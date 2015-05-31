namespace RDCManager.Models
{
    public interface IFileAccess
    {
        void Write<T>(string fileName, T item);

        T Read<T>(string fileName);
    }
}
