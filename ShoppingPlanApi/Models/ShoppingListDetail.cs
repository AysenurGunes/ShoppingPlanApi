using System.Reflection.Metadata.Ecma335;

namespace ShoppingPlanApi.Models
{
    public class ShoppingListDetail
    {
        public int ShoppingListDetailID { get; set; }
        public int ShoppingListID { get; set; }
        public ShoppingList ShoppingList { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int MeasurementID { get; set; }
        public Measurement Measurement { get; set; }
        public bool Done { get; set; }
        public string Notes { get; set; }
        public int UpdatedUserID { get; set; }
        public User User { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
