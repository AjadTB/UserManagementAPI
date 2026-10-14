using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public interface IUserService
{
    IEnumerable<User> GetAll();
    User? GetById(int id);
    bool EmailExists(string email, int? excludeId = null);
    User Create(UserRequest request);
    User? Update(int id, UserRequest request);
    bool Delete(int id);
}
