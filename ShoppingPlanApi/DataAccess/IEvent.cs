namespace ShoppingPlanApi.DataAccess
{
    public interface IEvent<T>
    {
        void EventAdd(object sender,T entity);
    }
}
