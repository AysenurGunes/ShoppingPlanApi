namespace ShoppingPlanApi.DataAccess
{

    public class EventBase<TEntity>
    {
        //event declaretion
        public EventHandler<TEntity> EventProcesss;
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
    }
}
