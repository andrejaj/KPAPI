using KPAPI.Model;

namespace KPAPI
{
    public interface IRepository
    {
        IList<Image> GetImages();
        IList<Item> GetItems();

        IList<Item> GetItems(int viewId);
        void InsertViewedOffer(string[] skus);
    }
}
