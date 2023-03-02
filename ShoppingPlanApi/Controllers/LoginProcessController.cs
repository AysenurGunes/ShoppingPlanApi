
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Dtos;
using ShoppingPlanApi.Jwt;
using ShoppingPlanApi.Models;
using ShoppingPlanApi.Response;
using System.Linq.Expressions;

namespace ShoppingPlanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginProcessController
    {
       // private readonly IShoppingPlan<User> _shoppingPlan;
        private readonly IConfiguration _configuration;
        private readonly ShoppingPlanDbContext _context;
        public LoginProcessController(ShoppingPlanDbContext context, IConfiguration configuration)
        {
            // _shoppingPlan = shoppingPlan;
            _context = context;
            _configuration = configuration;
        }
        [HttpPost("Login")]
        public string Login([FromQuery] LoginDto login)
        {

            string token = "";
            try
            {
               // List<User> logins=new List<User>();

              User  login1 = GetByMailandPass(login.mailAdress, login.password);
                if (login1!=null)
                {
                    GenerateToken generateToken = new GenerateToken(_configuration);
                    
                  token= generateToken.GenerateTokenJwt(login1).Result;

                    return token;
                }
            }
            catch (Exception)
            {
            }

            return token;
        }
        [HttpGet("GetByMailandPass")]
        private User GetByMailandPass([FromQuery] string mail, string pass)
        {
            //Expression<Func<User, bool>> expression = (c => c.Email == mail && c.Password == pass);

            return _context.Users.Where(c => c.Email == mail && c.Password == pass).Include(c => c.Role).FirstOrDefault();
        }
    }
}
