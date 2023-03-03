using KPAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace KPAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KPController : ControllerBase
    {
        private readonly ILogger<KPController> _logger;
        private readonly IRepository _repository;

        public KPController(ILogger<KPController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet(Name = "GetData")]
        public IEnumerable<Object> Get()
        {
            var items = _repository.GetItems();
            var itemImages = _repository.GetImages();

            foreach (var item in items)
            {
                var resultImages = itemImages.Where(x => x.ItemId.Equals(item.Id)).Select(x => x.Url);
                item.Images.AddRange(resultImages);
            }

            return items;
        }
    }
}