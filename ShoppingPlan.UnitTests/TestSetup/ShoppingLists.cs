using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPlan.UnitTests.TestSetup
{
    public static class ShoppingLists
    {
        //initilaize data
        public static void AddShoppingList(this ShoppingPlanDbContext context)
        {
            context.ShoppingLists.AddRange(new ShoppingList 
            {  CategoryID=1,CreatedDate=DateTime.UtcNow,CreatedUserID=1,Done=false,Notes="Test1",ShoppingListName="Test Shopping List1"},
            new ShoppingList
            {  CategoryID=1,CreatedDate=DateTime.UtcNow,CreatedUserID=1,Done=false,Notes="Test2",ShoppingListName="Test Shopping List2"});
        }

    }
}
