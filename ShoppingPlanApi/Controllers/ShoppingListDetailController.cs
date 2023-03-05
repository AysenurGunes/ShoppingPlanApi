using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Dtos;
using ShoppingPlanApi.Models;
using ShoppingPlanApi.Validations;
using System.Linq.Expressions;
using System.Security.Claims;

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

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<ShoppingListDetail> Get()
        {
            return _shoppingPlan.GetAll().ToList();
        }

        [HttpGet("GetByID")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public ShoppingListDetail Get([FromQuery] int id)
        {
            Expression<Func<ShoppingListDetail, bool>> expression = (c => c.ShoppingListDetailID == id);
            return _shoppingPlan.GetByID(expression);
        }

        [HttpGet("GetSearchByListName")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<ShoppingListDetail> GetSearchByListName([FromQuery] string Name)
        {
            Expression<Func<ShoppingListDetail, bool>> expression = (c => c.ShoppingList.ShoppingListName.Contains(Name));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }
        [HttpGet("GetSearchByCategory")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<ShoppingListDetail> GetSearchByCategory([FromQuery] string categoryName)
        {
            Expression<Func<ShoppingListDetail, bool>> expression = (c => c.ShoppingList.Category.CategoryName.Contains(categoryName));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }

        [HttpGet("GetOrderByListName")]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public List<ShoppingListDetail> GetOrder()
        {
            List<ShoppingListDetail> shoppingListDetials = Get().OrderBy(c => c.ShoppingList.ShoppingListName).ToList();
            return shoppingListDetials;
        }

        [HttpPost]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public ActionResult Post([FromBody] ShoppingListDetailAddDto shoppingListDetailAddDto)
        {
            ShoppingListDetailAddValidation validations = new ShoppingListDetailAddValidation();
            validations.ValidateAndThrow(shoppingListDetailAddDto);

            var shoppingListDetail = _mapper.Map<ShoppingListDetail>(shoppingListDetailAddDto);
            shoppingListDetail.Done = false;
            
            return StatusCode(_shoppingPlan.Add(shoppingListDetail));
        }


        [HttpPut]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public ActionResult Put([FromBody] ShoppingListDetailPutDto shoppingListDetailPutDto)
        {
            if (shoppingListDetailPutDto.ShoppingListDetailID == 0)
            {
                return BadRequest();
            }

            ShoppingListDetailPutValidation validations = new ShoppingListDetailPutValidation();
            validations.ValidateAndThrow(shoppingListDetailPutDto);

            var shoppingListDetail = _mapper.Map<ShoppingListDetail>(shoppingListDetailPutDto);
            shoppingListDetail.UpdatedDate = DateTime.UtcNow;

            int result = _shoppingPlan.Edit(shoppingListDetail);
            return StatusCode(result);
        }

        [HttpDelete]

        [Authorize(Roles = $"{Dtos.Types.Role.Admin},{Dtos.Types.Role.Nuser}")]
        public ActionResult Delete(ShoppingListDetail shoppingListDetail)
        {
            if (shoppingListDetail.ShoppingListDetailID == 0)
            {
                return BadRequest();
            }

            int result = _shoppingPlan.Delete(shoppingListDetail);
            return StatusCode(result);
        }
    }
}
