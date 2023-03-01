using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Dtos;
using ShoppingPlanApi.Helper;
using ShoppingPlanApi.Models;
using System.Linq.Expressions;

namespace ShoppingPlanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginProcessController
    {
        private readonly IShoppingPlan<User> _shoppingPlan;
        public LoginProcessController(IShoppingPlan<User> shoppingPlan)
        {
            _shoppingPlan = shoppingPlan;
        }
        [HttpPost("Login")]
        public string Login([FromQuery] LoginDto login)
        {

            string token = "";
            try
            {
                List<User> logins=new List<User>();
                logins = GetByMailandPass(login.mailAdress, login.password);
                if (logins.Count>0)
                {
                    GenerateToken generateToken = new GenerateToken();
                  token= generateToken.GenerateTokenJwt(logins.First().UserID, logins.First().RoleID);
                }
            }
            catch (Exception)
            {
            }

            return token;
        }
        [HttpGet("GetByMailandPass")]
        private List<User> GetByMailandPass([FromQuery] string mail, string pass)
        {
            Expression<Func<User, bool>> expression = (c => c.Email == mail && c.Password == pass);
            return _shoppingPlan.GetSpecial(expression).ToList();
        }
    }
}
