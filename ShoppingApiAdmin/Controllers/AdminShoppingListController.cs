using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingApiAdmin.DataAccess;
using ShoppingApiAdmin.Models;

namespace ShoppingApiAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminShoppingListController : ControllerBase
    {
        private readonly ShoppingApiAdminDbContext _context;
        public AdminShoppingListController(ShoppingApiAdminDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetAll")]
        
        public List<AdminShoppingList> Get()
        {
            return _context.AdminShoppingLists.ToList();
        }
        [HttpPost]
        public ActionResult Post([FromBody] AdminShoppingList adminShoppingList)
        {
            try
            {
                _context.AdminShoppingLists.Add(adminShoppingList);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
