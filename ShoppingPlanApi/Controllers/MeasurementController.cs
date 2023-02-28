using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System.Linq.Expressions;

namespace ShoppingPlanApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly IShoppingPlan<Measurement> _shoppingPlan;
        // private readonly IMapper _mapper;
        public MeasurementController(IShoppingPlan<Measurement> Measurement)
        {
            _shoppingPlan = Measurement;
            //_mapper = mapper;
        }
        [HttpGet("GetAll")]
        public List<Measurement> Get()
        {
            return _shoppingPlan.GetAll().ToList();
        }

        [HttpGet("GetByID")]
        public Measurement Get([FromQuery] int id)
        {
            Expression<Func<Measurement, bool>> expression = (c => c.MeasurementID == id);
            return _shoppingPlan.GetByID(expression);
        }

        [HttpGet("GetSearchByName")]
        public List<Measurement> GetSearch([FromQuery] string Name)
        {
            Expression<Func<Measurement, bool>> expression = (c => c.MeasurementName.Contains(Name));
            return _shoppingPlan.GetSpecial(expression).ToList();
        }

        [HttpGet("GetOrderByName")]
        public List<Measurement> GetOrder()
        {
            List<Measurement> measurements = Get().OrderBy(c => c.MeasurementName).ToList();
            return measurements;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Measurement measurement)
        {
            //PostBookValidation validations = new PostBookValidation();
            //validations.ValidateAndThrow(Measurement);

            return StatusCode(_shoppingPlan.Add(measurement));
        }


        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Measurement measurement)
        {
            if (measurement.MeasurementID != 0)
            {
                return BadRequest();
            }

            //BookValidation validations = new BookValidation();
            //validations.ValidateAndThrow(book1);

            int result = _shoppingPlan.Edit(measurement);
            return StatusCode(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Measurement measurement)
        {
            if (measurement.MeasurementID != 0)
            {
                return BadRequest();
            }

            int result = _shoppingPlan.Delete(measurement);
            return StatusCode(result);
        }
    }
}
