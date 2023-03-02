using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System.Linq.Expressions;

namespace ShoppingPlanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IShoppingPlan<Category> _shoppingPlan;
        // private readonly IMapper _mapper;
        public CategoryController(IShoppingPlan<Category> shoppingPlan)
        {
            _shoppingPlan = shoppingPlan;
            //_mapper = mapper;
        }
        [HttpGet("GetAll")]
        public List<Category> Get()
        {
            return _shoppingPlan.GetAll().ToList();
        }

        [HttpGet("GetByID")]
        public Category Get([FromQuery] int id)
        {
            Expression<Func<Category, bool>> expression = (c => c.CategoryID == id);
            return _shoppingPlan.GetByID(expression);
        }

        [HttpGet("GetSearchByName")]
        public List<Category> GetSearch([FromQuery] string Name)
        {
            Expression<Func<Category, bool>> expression = (c => c.CategoryName.Contains(Name));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }

        [HttpGet("GetOrderByName")]
        public List<Category> GetOrder()
        {
            List<Category> categories = Get().OrderBy(c => c.CategoryName).ToList();
            return categories;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Category category)
        {
            //PostBookValidation validations = new PostBookValidation();
            //validations.ValidateAndThrow(Category);

            return StatusCode(_shoppingPlan.Add(category));
        }


        [HttpPut]
        public ActionResult Put([FromBody] Category category)
        {
            if (category.CategoryID == 0)
            {
                return BadRequest();
            }

            //BookValidation validations = new BookValidation();
            //validations.ValidateAndThrow(book1);

            int result = _shoppingPlan.Edit(category);
            return StatusCode(result);
        }

        [HttpDelete]
        public ActionResult Delete(Category category)
        {
            if (category.CategoryID == 0)
            {
                return BadRequest();
            }

            int result = _shoppingPlan.Delete(category);
            return StatusCode(result);
        }
    }
}
