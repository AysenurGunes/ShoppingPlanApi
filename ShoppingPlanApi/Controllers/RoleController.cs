using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System.Linq.Expressions;

namespace ShoppingPlanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IShoppingPlan<Role> _shoppingPlan;
        // private readonly IMapper _mapper;
        public RoleController(IShoppingPlan<Role> shoppingPlan)
        {
            _shoppingPlan = shoppingPlan;
            //_mapper = mapper;
        }
        [HttpGet("GetAll")]
        public List<Role> Get()
        {
            return _shoppingPlan.GetAll().ToList();
        }

        [HttpGet("GetByID")]
        public Role Get([FromQuery] int id)
        {
            Expression<Func<Role, bool>> expression = (c => c.RoleID == id);
            return _shoppingPlan.GetByID(expression);
        }

        [HttpGet("GetSearchByName")]
        public List<Role> GetSearch([FromQuery] string Name)
        {
            Expression<Func<Role, bool>> expression = (c => c.RoleName.Contains(Name));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }

        [HttpGet("GetOrderByName")]
        public List<Role> GetOrder()
        {
            List<Role> roles = Get().OrderBy(c => c.RoleName).ToList();
            return roles;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Role role)
        {
            //PostBookValidation validations = new PostBookValidation();
            //validations.ValidateAndThrow(Role);

            return StatusCode(_shoppingPlan.Add(role));
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Role role)
        {
            if (role.RoleID != 0)
            {
                return BadRequest();
            }

            //BookValidation validations = new BookValidation();
            //validations.ValidateAndThrow(book1);

            int result = _shoppingPlan.Edit(role);
            return StatusCode(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Role role)
        {
            if (role.RoleID != 0)
            {
                return BadRequest();
            }

            int result = _shoppingPlan.Delete(role);
            return StatusCode(result);
        }
    }
}
