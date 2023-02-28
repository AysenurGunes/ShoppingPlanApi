using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;

namespace ShoppingPlanApi.Models
{
    public class ShoppingList
    {
        public int ShoppingListID { get; set; }
        public string ShoppingListName { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DoneDate { get; set; }
        public bool Done { get; set; }
        public int CreatedUserID { get; set; }
        public User User { get; set; }
        public string Notes { get; set; }
    }
}
