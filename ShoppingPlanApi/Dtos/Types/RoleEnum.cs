using System.ComponentModel;

namespace ShoppingPlanApi.Dtos.Types
{
    public enum RoleEnum
    {
        [Description(Role.Admin)]
        Admin = 2,
        [Description(Role.Nuser)]
        Nuser = 1
    }
    public class Role
    {
        public const string Admin = "Admin";
        public const string Nuser = "Nuser";
    }
}
