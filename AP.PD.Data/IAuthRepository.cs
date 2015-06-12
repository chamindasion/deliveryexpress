
using AP.PD.Domain;

namespace AP.PD.Data
{
    public interface IAuthRepository
    {
        UserDomain FindUser(string userName, string password);
    }
}
