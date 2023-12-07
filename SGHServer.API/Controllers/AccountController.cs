using Microsoft.AspNetCore.Mvc;
using SGHServer.API.RequestModel;
using SGHServer.Application.Repository.AccountRepository.Command.RegisterAccountCommand;

namespace SGHServer.API.Controllers;

[ApiController]
public class AccountController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Registration([FromBody] LoginModel loginModel)
    {
        var command = new RegisterAccountCommand()
        {
            Email = loginModel.Email,
            Password = loginModel.Password
        };
        var registrationResult = await Mediator.Send(command);
        
        return Ok(registrationResult);
    }
}