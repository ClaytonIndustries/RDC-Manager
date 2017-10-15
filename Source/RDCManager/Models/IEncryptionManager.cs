
namespace RDCManager.Models
{
    public interface IEncryptionManager
    {
        string AesEncrypt(string value);
        string AesDecrypt(string value);
    }
}