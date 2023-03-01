using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System.Linq.Expressions;

namespace ShoppingPlanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingPlan<ShoppingList> _shoppingPlan;
        // private readonly IMapper _mapper;
        public ShoppingListController(IShoppingPlan<ShoppingList> shoppingPlan)
        {
            _shoppingPlan = shoppingPlan;
            //_mapper = mapper;
        }
        [HttpGet("GetAll")]
        public List<ShoppingList> Get()
        {
            return _shoppingPlan.GetAll().ToList();
        }

        [HttpGet("GetByID")]
        public ShoppingList Get([FromQuery] int id)
        {
            Expression<Func<ShoppingList, bool>> expression = (c => c.ShoppingListID == id);
            return _shoppingPlan.GetByID(expression);
        }

        [HttpGet("GetSearchByName")]
        public List<ShoppingList> GetSearch([FromQuery] string Name)
        {
            Expression<Func<ShoppingList, bool>> expression = (c => c.ShoppingListName.Contains(Name));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }

        [HttpGet("GetOrderByName")]
        public List<ShoppingList> GetOrder()
        {
            List<ShoppingList> shoppingLists = Get().OrderBy(c => c.ShoppingListName).ToList();
            return shoppingLists;
        }

        [HttpPost]
        public ActionResult Post([FromBody] ShoppingList shoppingList)
        {
            //PostBookValidation validations = new PostBookValidation();
            //validations.ValidateAndThrow(ShoppingList);

            return StatusCode(_shoppingPlan.Add(shoppingList));
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromBody] ShoppingList shoppingList)
        {
            if (shoppingList.ShoppingListID != 0)
            {
                return BadRequest();
            }

            //BookValidation validations = new BookValidation();
            //validations.ValidateAndThrow(book1);

            int result = _shoppingPlan.Edit(shoppingList);
            return StatusCode(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(ShoppingList shoppingList)
        {
            if (shoppingList.ShoppingListID != 0)
            {
                return BadRequest();
            }

            int result = _shoppingPlan.Delete(shoppingList);
            return StatusCode(result);
        }
    }
}
