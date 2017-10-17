
namespace RDCManager.Models
{
    public interface IRDCGroupManager
    {
        RDCGroup CreateNew();
        void Delete(RDCGroup rdcGroup);
        bool Save();
    }
}