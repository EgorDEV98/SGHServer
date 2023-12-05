using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SGHServer.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{ 
    IMediator? _mediator;
    
    protected IMediator? Mediator
        => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}