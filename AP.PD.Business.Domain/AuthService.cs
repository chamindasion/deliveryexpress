
using AP.PD.Business.Interface;
using AP.PD.Data;
using AP.PD.Shared;

namespace AP.PD.Business.Domain
{
    public class AuthService : IAuthService
    {
        private readonly AuthRepository _repository = new AuthRepository();

        public UserDto FindUser(string userName, string password)
        {
            var user = _repository.FindUser(userName, password);
            UserDto userDto = null;
            if (user != null)
            {
                userDto = new UserDto()
                {
                    Id = user.Id,
                    LoginId = user.LoginId,
                    Password = user.Password,
                    RoleId = user.RoleId
                };

                if (user.Role != null)
                {
                    var role = new RoleDto()
                    {
                        Id = user.Role.Id,
                        Name = user.Role.Name,
                        Description = user.Role.Description
                    };
                    userDto.Role = role;
                }
            }
            return userDto;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
