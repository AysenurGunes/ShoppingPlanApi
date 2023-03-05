using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Dtos;
using ShoppingPlanApi.Models;
using ShoppingPlanApi.Validations;
using System.Diagnostics.Metrics;
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
            return _shoppingPlan.GetAll().Where(c => c.Done != true).ToList();
        }
        [HttpGet("GetAdmin")]
        [Authorize(Roles = Dtos.Types.Role.Admin)]
        public List<AdminShoppingList> GetAdmin()
        {
            EventBase<AdminShoppingList> eventBase = new EventBase<AdminShoppingList>();
            eventBase.EventProcesssGet += EventGet;

            return eventBase.EventGetProcess();
        }
        private static List<AdminShoppingList> EventGet()
        {
            //actually must use rabbitmq
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/adminshoppinglist/");


                var getTask = client.GetFromJsonAsync<List<AdminShoppingList>>("getall").Result;


                //var result = getTask.Result;
                //var readTask = result.Content.ReadAsAsync<IList<AdminShoppingList>>();
                //readTask.Wait();

                //List<AdminShoppingList> lists = readTask.Result;
                return getTask;

            }
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
            Expression<Func<ShoppingList, bool>> expression = (c => c.DoneDate.ToShortDateString() == doneDate.ToShortDateString());
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
        public ActionResult Post([FromBody] ShoppingListAddDto shoppingListAddDto)
        {
            var shoppingList = _mapper.Map<ShoppingList>(shoppingListAddDto);
            shoppingList.Done = false;
            shoppingList.CreatedDate = DateTime.UtcNow;
            //take from token
            shoppingList.CreatedUserID = 1;
            User user = new User();
            user.UserID = 1;
            shoppingList.User = user;
            Category category = new Category();
            category.CategoryID = shoppingList.CategoryID;
            shoppingList.Category = category;

            ShoppingListValidation validations = new ShoppingListValidation();
            validations.ValidateAndThrow(shoppingList);

            int statusCode = _shoppingPlan.Add(shoppingList);

            return StatusCode(statusCode);
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromBody] ShoppingListPutDto shoppingListPutDto)
        {
            if (shoppingListPutDto.ShoppingListID == 0)
            {
                return BadRequest();
            }


            var shoppingList = _mapper.Map<ShoppingList>(shoppingListPutDto);

            if (shoppingList.Done)
            {
                shoppingList.DoneDate = DateTime.UtcNow;

                EventBase<AdminShoppingList> eventBase = new EventBase<AdminShoppingList>();
                AdminShoppingList adminShoppingList = new AdminShoppingList
                {
                    ShoppingListID = shoppingListPutDto.ShoppingListID,
                    Notes1 = shoppingListPutDto.Notes,
                    ShoppingListName = shoppingListPutDto.ShoppingListName,
                    CategoryName = "category1"

                };
                eventBase.EventProcesss?.Invoke(this, adminShoppingList);
            }

            Category category = new Category();
            category.CategoryID = shoppingList.CategoryID;
            shoppingList.Category = category;

            User user = new User();
            user.UserID = 1;
            shoppingList.CreatedUserID = user.UserID;
            shoppingList.User = user;

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
