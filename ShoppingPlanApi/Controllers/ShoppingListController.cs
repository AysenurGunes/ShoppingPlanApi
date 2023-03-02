﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingPlan<ShoppingList> _shoppingPlan;
        private readonly IMapper _mapper;
        public ShoppingListController(IShoppingPlan<ShoppingList> shoppingPlan, IMapper mapper)
        {
            _shoppingPlan = shoppingPlan;
            _mapper = mapper;

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
        public List<ShoppingList> GetSearchByName([FromQuery] string Name)
        {
            Expression<Func<ShoppingList, bool>> expression = (c => c.ShoppingListName.Contains(Name));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }
        [HttpGet("GetSearchByCategory")]
        public List<ShoppingList> GetSearchByCategory([FromQuery] string categoryName)
        {
            Expression<Func<ShoppingList, bool>> expression = (c => c.Category.CategoryName.Contains(categoryName));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }
        [HttpGet("GetSearchByDoneDate")]
        public List<ShoppingList> GetSearchByDoneDate([FromQuery] DateTime doneDate)
        {
            Expression<Func<ShoppingList, bool>> expression = (c => c.DoneDate.ToShortDateString()==doneDate.ToShortDateString());
            return _shoppingPlan.GetSpecial(expression).ToList();
        }

        [HttpGet("GetOrderByName")]
        public List<ShoppingList> GetOrder()
        {
            List<ShoppingList> shoppingLists = Get().OrderBy(c => c.ShoppingListName).ToList();
            return shoppingLists;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Post([FromBody] ShoppingListWithDetailAddDto shoppingListWithDetailAddDto)
        {
            var shoppingList = _mapper.Map<ShoppingList>(shoppingListWithDetailAddDto.ShoppingListAddDto);
            shoppingList.Done = false;
            shoppingList.CreatedDate = DateTime.UtcNow;
            //take from token
            shoppingList.CreatedUserID = 1;

            ShoppingListValidation validations = new ShoppingListValidation();
            validations.ValidateAndThrow(shoppingList);

            int statusCode = _shoppingPlan.Add(shoppingList);

           // ShoppingListDetailController shoppingListDetailController=new ShoppingListDetailController(_shoppingPlan,mapper)
            return StatusCode(statusCode);
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ShoppingListPutDto shoppingListPutDto)
        {
            if (id != 0)
            {
                return BadRequest();
            }
          

            var shoppingList = _mapper.Map<ShoppingList>(shoppingListPutDto);

            if (shoppingList.Done)
            {
                shoppingList.DoneDate = DateTime.UtcNow;
            }

            ShoppingListValidation validations = new ShoppingListValidation();
            validations.ValidateAndThrow(shoppingList);

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
