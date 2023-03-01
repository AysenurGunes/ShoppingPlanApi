using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System.Linq.Expressions;

namespace ShoppingPlanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IShoppingPlan<User> _shoppingPlan;
        // private readonly IMapper _mapper;
        public UserController(IShoppingPlan<User> shoppingPlan)
        {
            _shoppingPlan = shoppingPlan;
            //_mapper = mapper;
        }
        [HttpGet("GetAll")]
        public List<User> Get()
        {
            return _shoppingPlan.GetAll().ToList();
        }

        [HttpGet("GetByID")]
        public User Get([FromQuery] int id)
        {
            Expression<Func<User, bool>> expression = (c => c.UserID == id);
            return _shoppingPlan.GetByID(expression);
        }

        [HttpGet("GetSearchByName")]
        public List<User> GetSearch([FromQuery] string Name)
        {
            Expression<Func<User, bool>> expression = (c => c.Name.Contains(Name));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }

        [HttpGet("GetOrderByName")]
        public List<User> GetOrder()
        {
            List<User> users = Get().OrderBy(c => c.Name).ToList();
            return users;
        }

        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            //PostBookValidation validations = new PostBookValidation();
            //validations.ValidateAndThrow(User);

            return StatusCode(_shoppingPlan.Add(user));
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromBody] User user)
        {
            if (user.UserID != 0)
            {
                return BadRequest();
            }

            //BookValidation validations = new BookValidation();
            //validations.ValidateAndThrow(book1);

            int result = _shoppingPlan.Edit(user);
            return StatusCode(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(User user)
        {
            if (user.UserID != 0)
            {
                return BadRequest();
            }

            int result = _shoppingPlan.Delete(user);
            return StatusCode(result);
        }
    }
}
