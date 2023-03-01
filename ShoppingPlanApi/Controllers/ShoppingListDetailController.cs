using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System.Linq.Expressions;

namespace ShoppingPlanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListDetailController : ControllerBase
    {
        private readonly IShoppingPlan<ShoppingListDetail> _shoppingPlan;
        // private readonly IMapper _mapper;
        public ShoppingListDetailController(IShoppingPlan<ShoppingListDetail> shoppingPlan)
        {
            _shoppingPlan = shoppingPlan;
            //_mapper = mapper;
        }
        [HttpGet("GetAll")]
        public List<ShoppingListDetail> Get()
        {
            return _shoppingPlan.GetAll().ToList();
        }

        [HttpGet("GetByID")]
        public ShoppingListDetail Get([FromQuery] int id)
        {
            Expression<Func<ShoppingListDetail, bool>> expression = (c => c.ShoppingListDetailID == id);
            return _shoppingPlan.GetByID(expression);
        }

        [HttpGet("GetSearchByListName")]
        public List<ShoppingListDetail> GetSearch([FromQuery] string Name)
        {
            Expression<Func<ShoppingListDetail, bool>> expression = (c => c.ShoppingList.ShoppingListName.Contains(Name));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }

        [HttpGet("GetOrderByListName")]
        public List<ShoppingListDetail> GetOrder()
        {
            List<ShoppingListDetail> shoppingListDetials = Get().OrderBy(c => c.ShoppingList.ShoppingListName).ToList();
            return shoppingListDetials;
        }

        [HttpPost]
        public ActionResult Post([FromBody] ShoppingListDetail shoppingListDetail)
        {
            //PostBookValidation validations = new PostBookValidation();
            //validations.ValidateAndThrow(ShoppingListDetail);

            return StatusCode(_shoppingPlan.Add(shoppingListDetail));
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromBody] ShoppingListDetail shoppingListDetail)
        {
            if (shoppingListDetail.ShoppingListDetailID != 0)
            {
                return BadRequest();
            }

            //BookValidation validations = new BookValidation();
            //validations.ValidateAndThrow(book1);

            int result = _shoppingPlan.Edit(shoppingListDetail);
            return StatusCode(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(ShoppingListDetail shoppingListDetail)
        {
            if (shoppingListDetail.ShoppingListDetailID != 0)
            {
                return BadRequest();
            }

            int result = _shoppingPlan.Delete(shoppingListDetail);
            return StatusCode(result);
        }
    }
}
