using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // GET api/users
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers() => Ok(_userService.GetAll());

    // GET api/users/5
    [HttpGet("{id:int}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _userService.GetById(id);
        if (user is null)
            return NotFound(new { error = $"User with id {id} was not found." });

        return Ok(user);
    }

    // POST api/users
    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] UserRequest request)
    {
        if (_userService.EmailExists(request.Email!))
            return Conflict(new { error = "A user with this email already exists." });

        var user = _userService.Create(request);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // PUT api/users/5
    [HttpPut("{id:int}")]
    public ActionResult<User> UpdateUser(int id, [FromBody] UserRequest request)
    {
        if (_userService.GetById(id) is null)
            return NotFound(new { error = $"User with id {id} was not found." });

        if (_userService.EmailExists(request.Email!, excludeId: id))
            return Conflict(new { error = "Another user already uses this email." });

        return Ok(_userService.Update(id, request));
    }

    // DELETE api/users/5
    [HttpDelete("{id:int}")]
    public IActionResult DeleteUser(int id)
    {
        if (!_userService.Delete(id))
            return NotFound(new { error = $"User with id {id} was not found." });

        return NoContent();
    }

    // Deliberately throws so the error-handling middleware can be demonstrated.
    // GET api/users/error-test
    [HttpGet("error-test")]
    public IActionResult ErrorTest() => throw new InvalidOperationException("Test exception");
}
