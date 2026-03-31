using Microsoft.AspNetCore.Mvc;
using TB5.WebApi.Database.AppDbContextModels;
using TB5.WebApi.Models;

namespace TB5.WebApi.Controllers
{
    // api/Product
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult GetProducts()
        {
            List<TblProduct> lst = db.TblProducts.ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            TblProduct? itemProduct = db.TblProducts
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (itemProduct is null)
            {
                return NotFound("Product not found.");
            }

            return Ok(itemProduct);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductCreateRequest request)
        {
            TblProduct product = new TblProduct
            {
                CreatedDateTime = DateTime.Now,
                IsDelete = false,
                Name = request.Name,
                Price = request.Price,
            };
            db.TblProducts.Add(product);
            int result = db.SaveChanges();

            bool isSuccess = result > 0;
            ProductCreateResponse response = new ProductCreateResponse
            {
                IsSuccess = isSuccess,
                Message = isSuccess ? "Product created successfully." : "Failed to create product.",
                Id = product.Id
            };
            return isSuccess ? Ok(response) : BadRequest(response);

            //if (result > 0)
            //{
            //    return Ok("Product created successfully.");
            //}

            //return BadRequest("Failed to create product.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] TblProduct product)
        {
            TblProduct? itemProduct = db.TblProducts
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (itemProduct is null)
            {
                return NotFound("Product not found.");
            }

            itemProduct.Name = product.Name;
            itemProduct.Price = product.Price;

            int result = db.SaveChanges();

            if (result > 0)
            {
                return Ok("Product updated successfully.");
            }

            return BadRequest("Failed to update product.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            TblProduct? itemProduct = db.TblProducts
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (itemProduct is null)
            {
                return NotFound("Product not found.");
            }

            db.TblProducts.Remove(itemProduct);
            int result = db.SaveChanges();

            if (result > 0)
            {
                return Ok("Product deleted successfully.");
            }

            return BadRequest("Failed to delete product.");
        }
    }
}