using Backend.Business_Logic_Layer;
using Microsoft.AspNetCore.Mvc;
using SharedModels.AuthViewModels;

namespace Backend.Presentation_Layer;

public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService _authService) {
        authService = _authService;
    }
    [HttpPost("/api/signin")]
    public async Task<ActionResult<SignInResponse>> SignIn([FromBody] SignInRequest request)
    {
        if (!ModelState.IsValid) return BadRequest();
        var signInResponse = await authService.SignInAsync(request);
        if (!signInResponse.IsSuccess) return BadRequest();
        return Ok(signInResponse);
    }
    [HttpPost("/api/refreshtoken")]
    public async Task<ActionResult<SignInResponse>> Refresh([FromBody] RefreshTokenViewModel request)
    {
        if (!ModelState.IsValid) return BadRequest();
        var refreshTokenResoponse = await authService.RefreshTokenAsync(request);
        if (!refreshTokenResoponse.IsSuccess) return BadRequest();
        return Ok(refreshTokenResoponse);
    }
    [HttpPost("/api/revoketoken")]
    public async Task<ActionResult> RevokeToken([FromBody] RefreshTokenViewModel request)
    {
        if (!ModelState.IsValid || !await authService.TryRevokeTokenAsync(request)) return BadRequest();
        return Ok();
    }

}
