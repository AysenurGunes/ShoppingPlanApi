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

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<Role> Get()
        {
            return _shoppingPlan.GetAll().ToList();
        }

        [HttpGet("GetByID")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public Role Get([FromQuery] int id)
        {
            Expression<Func<Role, bool>> expression = (c => c.RoleID == id);
            return _shoppingPlan.GetByID(expression);
        }

        [HttpGet("GetSearchByName")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<Role> GetSearch([FromQuery] string Name)
        {
            Expression<Func<Role, bool>> expression = (c => c.RoleName.Contains(Name));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }

        [HttpGet("GetOrderByName")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<Role> GetOrder()
        {
            List<Role> roles = Get().OrderBy(c => c.RoleName).ToList();
            return roles;
        }

        [HttpPost]

        [Authorize(Roles = Dtos.Types.Role.Admin)]
        public ActionResult Post([FromBody] Role role)
        {
            return StatusCode(_shoppingPlan.Add(role));
        }


        [HttpPut]

        [Authorize(Roles =Dtos.Types.Role.Admin)]
        public ActionResult Put([FromBody] Role role)
        {
            if (role.RoleID == 0)
            {
                return BadRequest();
            }


            int result = _shoppingPlan.Edit(role);
            return StatusCode(result);
        }

        [HttpDelete]

        [Authorize(Roles = Dtos.Types.Role.Admin)]
        public ActionResult Delete(Role role)
        {
            if (role.RoleID == 0)
            {
                return BadRequest();
            }

            int result = _shoppingPlan.Delete(role);
            return StatusCode(result);
        }
    }
}
