using KPAPI.Model;

namespace KPAPI
{
    public interface IRepository
    {
        IList<Image> GetImages();
        IList<Item> GetItems();
    }
}
