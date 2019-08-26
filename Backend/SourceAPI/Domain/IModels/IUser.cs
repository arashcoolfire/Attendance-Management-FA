using WebApplication18.Domain.Enums;

namespace WebApplication18.Domain.IModels
{
    public interface IUser
    {
        int UserId { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        UserRole UserRole { get; set; }
        bool UserActived { get; set; }
    }
}
