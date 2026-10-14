using System.Collections.Concurrent;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

// Simple in-memory store, registered as a singleton in Program.cs.
public class UserService : IUserService
{
    private readonly ConcurrentDictionary<int, User> _users = new();
    private int _nextId = 0;

    public IEnumerable<User> GetAll() => _users.Values.OrderBy(u => u.Id);

    public User? GetById(int id) => _users.TryGetValue(id, out var user) ? user : null;

    public bool EmailExists(string email, int? excludeId = null) =>
        _users.Values.Any(u =>
            u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Id != excludeId);

    public User Create(UserRequest request)
    {
        var user = new User
        {
            Id = Interlocked.Increment(ref _nextId),
            Name = request.Name!.Trim(),
            Email = request.Email!.Trim(),
            Age = request.Age,
            Department = request.Department!.Trim()
        };
        _users[user.Id] = user;
        return user;
    }

    public User? Update(int id, UserRequest request)
    {
        if (!_users.ContainsKey(id)) return null;

        var updated = new User
        {
            Id = id,
            Name = request.Name!.Trim(),
            Email = request.Email!.Trim(),
            Age = request.Age,
            Department = request.Department!.Trim()
        };
        _users[id] = updated;
        return updated;
    }

    public bool Delete(int id) => _users.TryRemove(id, out _);
}
