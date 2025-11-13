using AuthAPI.Application.Commands.Account.Login;
using AuthAPI.Application.Commands.Account.Register;
using AuthAPI.Application.Commands.Token.ExchangeToken;
using AuthAPI.Application.Requests.Account;
using AuthAPI.Application.Requests.Token;
using AuthAPI.Application.Responses.Account;
using AuthAPI.Application.Responses.Token;
using AuthAPI.Configuration.JwtSettings;
using AuthAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;
    private readonly JwtSettings _jwtSettings;

    public AuthController(ISender sender, IOptions<JwtSettings> options)
    {
        _sender = sender;
        _jwtSettings = options.Value;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationRequest request)
    {
        var command = new RegistrationCommand(request);
        var response = await _sender.Send(command);
        if (!response.IsSuccess)
            return BadRequest(response.FailureMessage);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand(request);
        var response = await _sender.Send(command);
        if (!response.IsSuccess)
            return BadRequest(response.FailureMessage);
        return Ok(response);
    }

    [HttpPost("exchange-token")]
    public async Task<ActionResult<ExchangeTokenResponse>> ExchangeToken([FromBody] ExchangeTokenRequest request)
    {
        var command = new ExchangeTokenCommand(request);
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return BadRequest(result.FailureMessage);

        var response = new ExchangeTokenResponse(result.IdToken!);

        Response.Cookies.AppendSecuredCookies(new[]
        {
            ("access_token", result.AccessToken!, DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes)),
            ("refresh_token", result.RefreshToken!.Token, result.RefreshToken.ExpiresAt)
        });

        return Ok(response);
    }

    //[HttpPost("refresh")]
    //public async Task<ActionResult> RefreshToken()
    //{
        
    //}

    [HttpPost("logout")]
    public ActionResult Logout()
    {
        Response.Cookies.Delete("access_token");
        Response.Cookies.Delete("refresh_token");

        return NoContent();
    }
}
