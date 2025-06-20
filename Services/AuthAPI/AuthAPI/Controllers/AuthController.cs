using AuthAPI.Application.Commands.Account.Login;
using AuthAPI.Application.Commands.Account.Register;
using AuthAPI.Application.Commands.Token.ExchangeToken;
using AuthAPI.Application.Requests.Account;
using AuthAPI.Application.Requests.Token;
using AuthAPI.Application.Responses.Account;
using AuthAPI.Application.Responses.Token;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationRequest request)
    {
        var command = new RegistrationCommand(request);
        var response = await _sender.Send(command);
        if(!response.IsSuccess)
            return Conflict();
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand(request);
        var response = await _sender.Send(command);
        if(response.IsSuccess)
            return Unauthorized();
        return Ok(response);
    }

    [HttpPost("exchange-token")]
    public async Task<ActionResult<ExchangeTokenResponse>> ExchangeToken([FromBody] ExchangeTokenRequest request)
    {
        var command = new ExchangeTokenCommand(request);
        var response = await _sender.Send(command);
        return Ok(response);
    }
}
