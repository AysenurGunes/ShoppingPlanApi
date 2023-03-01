namespace ShoppingPlanApi.Dtos
{
    public class ShoppingListPutDto
    {
        public string ShoppingListName { get; set; }
        public int CategoryID { get; set; }
        public bool Done { get; set; }
        public string Notes { get; set; }
    }
}
