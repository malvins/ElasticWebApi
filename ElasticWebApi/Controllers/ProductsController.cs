using ElasticWebApi.Model.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ElasticWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
                        IElasticClient elasticClient,
                        ILogger<ProductsController> logger)
        {
            _logger = logger;
            _elasticClient = elasticClient;
        }

        [HttpGet(Name ="search")]
        public async Task<IActionResult> Search([FromQuery]string keyword)
        {
            var result = await _elasticClient.SearchAsync<Product>(
                             s => s.Query(
                                 q => q.QueryString(
                                     d => d.Query('*' + keyword + '*')
                                 )).Size(5000));

            _logger.LogInformation("ProductsController Get - ", DateTime.UtcNow);
            return Ok(result.Documents.ToList());
        }

        [HttpPost(Name ="create")]
        public async Task<IActionResult> Create([FromBody]Product product)
        {
            // Add product to ELS index
            var product1 = new Product
            {
                Description = "Product 1",
                Id = Guid.NewGuid(),
                Price = 200,
                Measurement = "40",
                Quantity = 43888200,
                Title = "Nike Shoes",
                Unit = "10",
                CreatedAt = new DateTime().AddHours(1)
            };

            var product2 = new Product
            {
                Description = "Product 2",
                Id = Guid.NewGuid(),
                Price = 30000000,
                Measurement = "38",
                Quantity = 299,
                Title = "Adidas Shoes",
                Unit = "10",
                CreatedAt = new DateTime().AddHours(1)
            };

            var product3 = new Product
            {
                Description = "Product 3",
                Id = Guid.NewGuid(),
                Price = 2000000,
                Measurement = "30",
                Quantity = 92,
                Title = "Delicio Shoes",
                Unit = "10",
                CreatedAt = new DateTime().AddHours(2)
            };

            // Index product dto
            await _elasticClient.IndexDocumentAsync(product1);
            await _elasticClient.IndexDocumentAsync(product2);
            await _elasticClient.IndexDocumentAsync(product3);

            _logger.LogInformation("ProductsController Get - ", DateTime.UtcNow);
            return Ok();
        }

    }
}
