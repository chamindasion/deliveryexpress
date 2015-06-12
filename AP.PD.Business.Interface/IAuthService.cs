
using AP.PD.Shared;

namespace AP.PD.Business.Interface
{
    public interface IAuthService
    {
        UserDto FindUser(string userName, string password);
    }
}
