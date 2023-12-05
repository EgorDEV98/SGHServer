using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGHServer.API.RequestModel;
using SGHServer.Application.Repository.TokenRepository.Command.RefreshTokenCommand;
using SGHServer.Application.Repository.TokenRepository.Command.RevokeTokenCommand;

namespace SGHServer.API.Controllers;

public class TokenController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Refresh([FromBody] TokenApiModel tokenApiModel)
    {
        var command = new RefreshTokenCommand()
        {
            AccessToken = tokenApiModel.AccessToken,
            RefreshToken = tokenApiModel.RefreshToken
        };
        
        var authResponse = await Mediator.Send(command);
        if (authResponse == null)
        {
            return BadRequest("Ошибка запроса");
        }
        
        return Ok(authResponse);
    }

    [HttpPost, Authorize]
    public async Task<IActionResult> Revoke()
    {
        var email = User.Identity.Name;
        var command = new RevokeTokenCommand()
        {
            Email = email
        };
        await Mediator.Send(command);

        return NoContent();
    }
}