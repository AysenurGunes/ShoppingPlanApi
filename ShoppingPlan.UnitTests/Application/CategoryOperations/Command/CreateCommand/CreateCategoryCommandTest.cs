using AutoMapper;
using FluentAssertions;
using ShoppingPlan.UnitTests.TestSetup;
using ShoppingPlanApi.Controllers;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPlan.UnitTests.Application.CategoryOperations.Command.CreateCommand
{
    public class CreateCategoryCommandTest:IClassFixture<CommonTestFixture<Category>>
    {
        private readonly ShoppingPlanDbContext _context;
        private readonly IMapper _mapper;
        private readonly IShoppingPlan<Category> _shoppingPlan;
        public CreateCategoryCommandTest(CommonTestFixture<Category> testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
            _shoppingPlan= testFixture.ShoppingPlan;
        }
        //zaten varolan bir categori ismi girildiğinde hata veren test
        public void WhenAlreadyExistCategoryNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange(Hazırlık)
            var category = new Category() { CategoryName = "Test_ WhenAlreadyExistCategoryNameIsGiven_InvalidOperationException_ShouldBeReturn" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            CategoryController categoryController = new CategoryController(_shoppingPlan);
            //act(çalştırma)
            //assert (doğrulama)

            FluentActions
                .Invoking(()=>categoryController.Post(category))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kategori zaten mevcut");
        }
    }
}
