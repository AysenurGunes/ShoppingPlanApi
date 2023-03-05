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
        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<Category> Get()
        {
            return _shoppingPlan.GetAll().ToList();
        }

        [HttpGet("GetByID")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public Category Get([FromQuery] int id)
        {
            Expression<Func<Category, bool>> expression = (c => c.CategoryID == id);
            return _shoppingPlan.GetByID(expression);
        }

        [HttpGet("GetSearchByName")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<Category> GetSearch([FromQuery] string Name)
        {
            Expression<Func<Category, bool>> expression = (c => c.CategoryName.Contains(Name));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }

        [HttpGet("GetOrderByName")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<Category> GetOrder()
        {
            List<Category> categories = Get().OrderBy(c => c.CategoryName).ToList();
            return categories;
        }

        [HttpPost]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public ActionResult Post([FromBody] Category category)
        {

            return StatusCode(_shoppingPlan.Add(category));
        }


        [HttpPut]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public ActionResult Put([FromBody] Category category)
        {
            if (category.CategoryID == 0)
            {
                return BadRequest();
            }


            int result = _shoppingPlan.Edit(category);
            return StatusCode(result);
        }

        [HttpDelete]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
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
