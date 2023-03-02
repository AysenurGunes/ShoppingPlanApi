using ShoppingPlanApi.Models;

namespace ShoppingPlanApi.DataAccess
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbcontext = new ShoppingPlanDbContext())
            {
                if (dbcontext.Categories.Any())
                {
                    return;
                }
                dbcontext.Categories.AddRange(new Category
                {
                    CategoryName = "Okul Alışverişi"

                }, new Category
                {
                    CategoryName = "Mutfak Alışverişi"

                }, new Category
                {
                    CategoryName = "Elbise Alışverişi"

                });
                dbcontext.Measurements.AddRange(new Measurement
                {
                    MeasurementName = "Adet"

                }, new Measurement
                {
                    MeasurementName = "Kilo"

                }, new Measurement
                {
                    MeasurementName = "Litre"

                }, new Measurement
                {
                    MeasurementName = "Metre"

                });

                dbcontext.Products.AddRange(
                    new Product
                    {
                        ProductName = "Kitap"

                    },
                    new Product
                    {
                        ProductName="Kalem"
                    },
                    new Product
                    {
                        ProductName="Cetvel"
                    },
                    new Product
                    {
                        ProductName="Ayakkabı"
                    },
                    new Product
                    {
                        ProductName="Ceket"
                    },
                    new Product
                    {
                        ProductName="Domates"
                    },
                    new Product
                    {
                        ProductName="Sogan"
                    },
                    new Product
                    {
                        ProductName="Süt"
                    }
                );
                dbcontext.Roles.AddRange(new Role
                {
                    RoleName = "Nuser"

                },
                new Role
                {
                    RoleName="Admin"
                });
                dbcontext.Users.AddRange(new User
                {
                    Name = "Ayşenur",
                    Surname="Güneş",
                    Email="gunesaysenur94@gmail.com",
                    RoleID=2,
                    Password="123456"

                },new User
                {
                    Name = "Ayşe",
                    Surname="Gün",
                    Email="gunesaysenur942@gmail.com",
                    RoleID=1,
                    Password="123456"

                });

                dbcontext.SaveChanges();
            }
        }
    }
}
