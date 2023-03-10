using KPAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace KPAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KPController : ControllerBase
    {
        private readonly ILogger<KPController> _logger;
        private readonly IRepository _repository;

        public KPController(ILogger<KPController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet] 
        [Route("/all")]
        public IEnumerable<Object> Get()
        {
            var items = _repository.GetItems();
            var itemImages = _repository.GetImages(); //to be fixed pass list of items and retrive images together

            foreach (var item in items)
            {
                var resultImages = itemImages.Where(x => x.ItemId.Equals(item.Id)).Select(x => x.Url);
                item.Images.AddRange(resultImages);
            }

            return items;
        }


        [HttpGet]
        [Route("/view")]
        public IEnumerable<Object> Get(int id)
        {
            var items = (id == 2) ?_repository.GetItems() : _repository.GetItems(id);
            var itemImages = _repository.GetImages(); //to be fixed pass list of items and retrive images together

            foreach (var item in items)
            {
                var resultImages = itemImages.Where(x => x.ItemId.Equals(item.Id)).Select(x => x.Url);
                item.Images.AddRange(resultImages);
            }

            return items;
        }

        // POST api/values
        [HttpPost]
        [Route("/save")]
        public void Post([FromBody] string[] skus)
        {
            _repository.InsertViewedOffer(skus);
        }
    }
}