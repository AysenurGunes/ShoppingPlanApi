using ShoppingPlanApi.Models;

namespace ShoppingPlanApi.Dtos
{
    public class UserPutDto
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
