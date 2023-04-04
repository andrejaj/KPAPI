using static KPAPI.Repository;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using KPAPI.Model;

namespace KPAPI
{
    public class Repository : IRepository
    {
        private readonly ILogger<Repository> _logger;
        private readonly string _connectionString;
        public Repository(ILogger<Repository> logger, string connectionString)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IList<Item> GetItems(int viewId)
        {
            List<Item> items = null;

            string sqlString = $@"
            SELECT 
                Item.Id as Id, Item.Title as Title, ItemOffer.Sku as SKu, Item.Description as Description, ItemOffer.Url as Url, ItemOffer.ValidUntil as ValidUntil, Author.FirstName as AuthorFirstName, Author.LastName as AuthorLastName, 
                Author.Nickname as AuthorNickName, ItemOffer.Price as Price,PriceType.Description as PriceType, ItemCondition.Description as Condition, Seller.Name as SellerName, Seller.Phone as SellerPhone 
            FROM 
                [KPProducts].[dbo].[ItemOffer]
            INNER JOIN 
                [KPProducts].[dbo].[Item] ON Item.Id = ItemOffer.ItemId
            INNER JOIN 
                [KPProducts].[dbo].[Seller] ON Seller.Id =  ItemOffer.SellerId
            LEFT JOIN 
                [KPProducts].[dbo].[Author] ON Author.Id = Item.AuthorId
            LEFT JOIN 
                [KPProducts].[dbo].[Currency] ON Currency.Id = ItemOffer.CurrencyId
            INNER JOIN 
                [KPProducts].[dbo].[PriceType] ON PriceType.PriceTypeId = ItemOffer.PriceTypeId 
            INNER JOIN 
                [KPProducts].[dbo].[ItemCondition] ON ItemCondition.Id = ItemOffer.ConditionId
            WHERE 
               [KPProducts].[dbo].[ItemOffer].ViewItem = @ViewId";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {

                    items = db.Query<Item>(sqlString, new { ViewId = viewId }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetItem failed to retrive data.");
            }
            return items ?? new List<Item>();
        }

        public IList<Item> GetItems()
        {
            List<Item> items = null;

            string sqlString = $@"
            SELECT 
                Item.Id as Id, Item.Title as Title, ItemOffer.Sku as SKu, Item.Description as Description, ItemOffer.Url as Url, ItemOffer.ValidUntil as ValidUntil, Author.FirstName as AuthorFirstName, Author.LastName as AuthorLastName, 
                Author.Nickname as AuthorNickName, ItemOffer.Price as Price,PriceType.Description as PriceType, ItemCondition.Description as Condition, Seller.Name as SellerName, Seller.Phone as SellerPhone 
            FROM 
                [KPProducts].[dbo].[ItemOffer]
            INNER JOIN 
                [KPProducts].[dbo].[Item] ON Item.Id = ItemOffer.ItemId
            INNER JOIN 
                [KPProducts].[dbo].[Seller] ON Seller.Id =  ItemOffer.SellerId
            LEFT JOIN 
                [KPProducts].[dbo].[Author] ON Author.Id = Item.AuthorId
            LEFT JOIN 
                [KPProducts].[dbo].[Currency] ON Currency.Id = ItemOffer.CurrencyId
            INNER JOIN 
                [KPProducts].[dbo].[PriceType] ON PriceType.PriceTypeId = ItemOffer.PriceTypeId 
            INNER JOIN 
                [KPProducts].[dbo].[ItemCondition] ON ItemCondition.Id = ItemOffer.ConditionId";
            
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {

                    items = db.Query<Item>(sqlString).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetItem failed to retrive data.");
            }
            return items ?? new List<Item>();
        }

        public IList<Image> GetImages()
        {
            List<Image> images = null;

            string sqlString = $@"
                SELECT 
                    Item.Id as ItemId, ItemImage.url as Url 
                From 
                    [KPProducts].[dbo].[Item]
                INNER JOIN 
                    [KPProducts].[dbo].[ItemImage] ON Item.Id = ItemImage.ItemId";

            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {

                    images = db.Query<Image>(sqlString).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetImages failed to retrive data.");
            }
            return images ?? new List<Image>();
        }

        public void InsertViewedOffer(string[] skus)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var arr = Array.ConvertAll(skus, int.Parse);
                    var count = db.Execute(@"UPDATE ItemOffer SET ViewItem = 1 WHERE SKU IN @skus", new { skus = arr });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"InsertViewedOffer failed to chnage view for skus withid {skus.ToString()}");
            }
        }
    }
}
