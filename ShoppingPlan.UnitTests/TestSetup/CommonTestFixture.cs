using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoppingPlanApi.AutoMapper;
using ShoppingPlanApi.DataAccess;


namespace ShoppingPlan.UnitTests.TestSetup
{
    public class CommonTestFixture<TEntity>
    {
        public ShoppingPlanDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public IShoppingPlan<TEntity> ShoppingPlan { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<ShoppingPlanDbContext>().UseInMemoryDatabase(databaseName: "ShoppingPlanTestDb").Options;
            Context = new ShoppingPlanDbContext(options);
            Context.Database.EnsureCreated();
            Context.AddShoppingList();
            Context.AddCategories();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}
