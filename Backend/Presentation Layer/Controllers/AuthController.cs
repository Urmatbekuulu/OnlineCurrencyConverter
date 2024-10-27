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
    public async Task<ActionResult<SignInResponse>> SignIn([FromBody] SignInRequest request)
    {
        if (!ModelState.IsValid) return BadRequest();
        var signInResponse = await authService.SignInAsync(request);
        if (!signInResponse.IsSuccess) return BadRequest();
        return Ok(signInResponse);
    }
}
