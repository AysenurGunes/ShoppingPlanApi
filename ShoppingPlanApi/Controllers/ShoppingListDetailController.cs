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
    public class ShoppingListDetailController : ControllerBase
    {
        private readonly IShoppingPlan<ShoppingListDetail> _shoppingPlan;
        private readonly IMapper _mapper;
        public ShoppingListDetailController(IShoppingPlan<ShoppingListDetail> shoppingPlan, IMapper mapper)
        {
            _shoppingPlan = shoppingPlan;
            _mapper = mapper;
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
        public ActionResult Post([FromBody] ShoppingListDetailAddDto shoppingListDetailAddDto)
        {
            ShoppingListDetailAddValidation validations = new ShoppingListDetailAddValidation();
            validations.ValidateAndThrow(shoppingListDetailAddDto);

            var shoppingListDetail = _mapper.Map<ShoppingListDetail>(shoppingListDetailAddDto);
            shoppingListDetail.Done = false;
            
            return StatusCode(_shoppingPlan.Add(shoppingListDetail));
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] ShoppingListDetailPutDto shoppingListDetailPutDto)
        {
            if (id != 0)
            {
                return BadRequest();
            }

            ShoppingListDetailPutValidation validations = new ShoppingListDetailPutValidation();
            validations.ValidateAndThrow(shoppingListDetailPutDto);

            var shoppingListDetail = _mapper.Map<ShoppingListDetail>(shoppingListDetailPutDto);
            shoppingListDetail.UpdatedDate = DateTime.UtcNow;
            //take from token
            shoppingListDetail.UpdatedUserID = 1;
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
