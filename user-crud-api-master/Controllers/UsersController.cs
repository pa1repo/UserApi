namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Users;
using WebApi.Services;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Route("GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(CreateRequest model)
    {
        await _userService.Create(model);
        return Ok(new { message = "User created" });
    }

    [HttpPut("{id}")]
    [Route("Update")]
    public async Task<IActionResult> Update(UpdateRequest model)
    {
        await _userService.Update(model.user_id, model);
        return Ok(new { message = "User updated" });
    }

    [HttpDelete, Route("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.Delete(id);
        return Ok(new { message = "User deleted" });
    }
}