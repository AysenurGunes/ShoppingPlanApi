using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Dtos;
using ShoppingPlanApi.Models;
using ShoppingPlanApi.Validations;
using System.Linq.Expressions;

namespace ShoppingPlanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IShoppingPlan<User> _shoppingPlan;
        private readonly IMapper _mapper;
        public UserController(IShoppingPlan<User> shoppingPlan, IMapper mapper)
        {
            _shoppingPlan = shoppingPlan;
            _mapper = mapper;
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
        public ActionResult Post([FromBody] UserAddDto userAddDto)
        {
            UserAddValidation validations = new UserAddValidation();
            validations.ValidateAndThrow(userAddDto);

            var user = _mapper.Map<User>(userAddDto);
            return StatusCode(_shoppingPlan.Add(user));
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] UserPutDto userPutDto)
        {
            if (id != 0)
            {
                return BadRequest();
            }
            UserPutValidation validations = new UserPutValidation();
            validations.ValidateAndThrow(userPutDto);

            var user=_mapper.Map<User>(userPutDto);
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
