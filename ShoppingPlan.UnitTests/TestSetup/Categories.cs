using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPlan.UnitTests.TestSetup
{
    public static class Categories
    {
        public static void AddCategories(this ShoppingPlanDbContext context)
        {
            context.Categories.AddRange(
             new Category
             { CategoryName = "Test Category1" },
            new Category
            { CategoryName = "Test Category2" }
           );
        }
    }
}
