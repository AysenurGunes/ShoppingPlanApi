namespace ShoppingPlanApi.Dtos
{
    public class ShoppingListDetailAddDto
    {
        public int ShoppingListID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int MeasurementID { get; set; }
        public string Notes { get; set; }
    }
}
