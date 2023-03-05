using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<Product> Get()
        {
            return _shoppingPlan.GetAll().ToList();
        }

        [HttpGet("GetByID")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public Product Get([FromQuery] int id)
        {
            Expression<Func<Product, bool>> expression = (c => c.ProductID == id);
            return _shoppingPlan.GetByID(expression);
        }

        [HttpGet("GetSearchByName")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<Product> GetSearch([FromQuery] string Name)
        {
            Expression<Func<Product, bool>> expression = (c => c.ProductName.Contains(Name));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }

        [HttpGet("GetOrderByName")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<Product> GetOrder()
        {
            List<Product> products = Get().OrderBy(c => c.ProductName).ToList();
            return products;
        }

        [HttpPost]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public ActionResult Post([FromBody] Product product)
        {
            

            return StatusCode(_shoppingPlan.Add(product));
        }


        [HttpPut]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public ActionResult Put([FromBody] Product product)
        {
            if (product.ProductID == 0)
            {
                return BadRequest();
            }


            int result = _shoppingPlan.Edit(product);
            return StatusCode(result);
        }

        [HttpDelete]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public ActionResult Delete(Product product)
        {
            if (product.ProductID == 0)
            {
                return BadRequest();
            }

            int result = _shoppingPlan.Delete(product);
            return StatusCode(result);
        }
    }
}
