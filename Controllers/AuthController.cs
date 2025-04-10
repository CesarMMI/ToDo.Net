﻿using api.Dtos.Auth;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            return Ok(new AuthResponse { Data = await authService.Register(request) });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return Ok(new AuthResponse { Data = await authService.Login(request) });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshRequest request)
        {
            return Ok(new AuthResponse { Data = await authService.Refresh(request) });
        }
    }
}
