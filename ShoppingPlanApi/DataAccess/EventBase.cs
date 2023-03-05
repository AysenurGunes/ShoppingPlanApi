using Microsoft.AspNetCore.Mvc;
using ShoppingPlanApi.Controllers;
using ShoppingPlanApi.Dtos;

namespace ShoppingPlanApi.DataAccess
{

    public class EventBase<TEntity>
    {

        //event declaretion
        public  EventHandler<TEntity> EventProcesss;
        public delegate List<TEntity> GetDelegate();
        public event GetDelegate EventProcesssGet;

        public EventBase()
        {
            EventProcesss += EventAdd;

        }

        public static void EventAdd(object sender, TEntity entity)
        {
            //actually must use rabbitmq
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7040/api/adminshoppinglist");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<TEntity>("adminShoppingList", entity);
                postTask.Wait();

                var result = postTask.Result;

            }
        }
        public  List<TEntity> EventGetProcess()
        {
          return  EventProcesssGet?.Invoke();
        }
    }
}
