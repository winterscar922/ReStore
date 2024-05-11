using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly ILogger _logger;

        public ProductsController(StoreContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> getAllProducts()
        {
            try
            {
                var response = await _context.Products.ToListAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in getAllProducts API");
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> getProductById(int id)
        {
            try
            {
                var response = await _context.Products.FindAsync(id);

                if (response is null)
                {
                    throw new Exception("Error in Product API - Product Not Found");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}