using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ProductTest.Application.DTOs;
using ProductTest.Application.DTOs.Request.Auth;
using ProductTest.Application.DTOs.Response.Auth;
using ProductTest.Application.Features.v2.Auth.Commands.Login;
using ProductTest.Application.Features.v2.Auth.Commands.Logout;
using ProductTest.Application.Features.v2.Auth.Commands.RefreshToken;
using ProductTest.Application.Features.v2.Auth.Commands.Register;
using ProductTest.Presentation.Authorization.Attributes;
using ProductTest.Presentation.Resources;

namespace ProductTest.Presentation.Controllers.v2
{
    /// <summary>
    /// Authentication controller for user login, registration, and token management
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public sealed class AuthController(IMediator mediator, IStringLocalizer<SharedResource> localizer) : ControllerBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new LoginCommand(request), cancellationToken);
            return Ok(BaseApiResponse<LoginResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new RegisterCommand(request), cancellationToken);
            return Ok(BaseApiResponse<RegisterResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
        }

        [HttpPost("logout")]
        [Authorize]
        [AuthorizePermissions("auth.logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new LogoutCommand(request), cancellationToken);
            return Ok(BaseApiResponse<LogoutResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new RefreshTokenCommand(request), cancellationToken);
            return Ok(BaseApiResponse<RefreshTokenResponse>.SuccessResult(response, localizer["OperationCompletedSuccessfully"]));
        }
    }
}