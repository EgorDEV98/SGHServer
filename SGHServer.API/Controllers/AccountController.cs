using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGHServer.API.RequestModel;
using SGHServer.Application.Repository.AccountRepository.Command.LoginAccountCommand;
using SGHServer.Application.Repository.AccountRepository.Command.RegisterAccountCommand;
using SGHServer.Application.Repository.AccountRepository.Query.UserInformationQuery;

namespace SGHServer.API.Controllers;

[ApiController]
public class AccountController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Registration([FromBody] RegistrationModel loginModel)
    {
        var command = new RegisterAccountCommand()
        {
            Name = loginModel.Name,
            Email = loginModel.Email,
            Password = loginModel.Password
        };
        var registrationResult = await Mediator.Send(command);
        
        return Ok(registrationResult);
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var command = new LoginAccountCommand()
        {
            Email = loginModel.Email,
            Password = loginModel.Password
        };
        var loginResult = await Mediator.Send(command);

        return Ok(loginResult);
    }

    [HttpGet, Authorize]
    public async Task<IActionResult> GetInfo()
    {
        var usersid = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
        int.TryParse(usersid, out var userId);
        
        var command = new UserInformationCommand()
        {
            Id = userId
        };
        var userInforamtionVM = await Mediator.Send(command);

        return Ok(userInforamtionVM);
    }
}