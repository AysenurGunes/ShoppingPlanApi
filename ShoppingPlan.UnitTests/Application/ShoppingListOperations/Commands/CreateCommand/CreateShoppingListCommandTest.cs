using AutoMapper;
using ShoppingPlan.UnitTests.TestSetup;
using ShoppingPlanApi.DataAccess;
using ShoppingPlanApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingPlan.UnitTests.Application.ShoppingListOperations.Commands.CreateCommand
{
    public class CreateShoppingListCommandTest:IClassFixture<CommonTestFixture<ShoppingList>>
    {
        private readonly ShoppingPlanDbContext _context;
        private readonly IMapper _mapper;
        public CreateShoppingListCommandTest(CommonTestFixture<ShoppingList> testFixture)
        {
            _context = testFixture.Context;
            _mapper= testFixture.Mapper;
        }
        //zaten varolan bir shoppinglist ismi girildiğinde hata veren test
        public void WhenAlreadyExistShoppingListNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange(Hazırlık)
           // var  shoppingList=new ShoppingList() { }
            //act(çalştırma)
            //assert (doğrulama)
        }
    }
}
