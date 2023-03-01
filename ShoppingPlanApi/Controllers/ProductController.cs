using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System.Linq.Expressions;

namespace ShoppingPlanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IShoppingPlan<Product> _shoppingPlan;
        // private readonly IMapper _mapper;
        public ProductController(IShoppingPlan<Product> shoppingPlan)
        {
            _shoppingPlan = shoppingPlan;
            //_mapper = mapper;
        }
        [HttpGet("GetAll")]
        public List<Product> Get()
        {
            return _shoppingPlan.GetAll().ToList();
        }

        [HttpGet("GetByID")]
        public Product Get([FromQuery] int id)
        {
            Expression<Func<Product, bool>> expression = (c => c.ProductID == id);
            return _shoppingPlan.GetByID(expression);
        }

        [HttpGet("GetSearchByName")]
        public List<Product> GetSearch([FromQuery] string Name)
        {
            Expression<Func<Product, bool>> expression = (c => c.ProductName.Contains(Name));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }

        [HttpGet("GetOrderByName")]
        public List<Product> GetOrder()
        {
            List<Product> products = Get().OrderBy(c => c.ProductName).ToList();
            return products;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            //PostBookValidation validations = new PostBookValidation();
            //validations.ValidateAndThrow(Product);

            return StatusCode(_shoppingPlan.Add(product));
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Product product)
        {
            if (product.ProductID != 0)
            {
                return BadRequest();
            }

            //BookValidation validations = new BookValidation();
            //validations.ValidateAndThrow(book1);

            int result = _shoppingPlan.Edit(product);
            return StatusCode(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Product product)
        {
            if (product.ProductID != 0)
            {
                return BadRequest();
            }

            int result = _shoppingPlan.Delete(product);
            return StatusCode(result);
        }
    }
}
