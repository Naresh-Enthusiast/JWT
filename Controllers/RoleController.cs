using System.Runtime.CompilerServices;
using AuthApi.common.Models;
using AuthApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRole([FromBody]CreateRoleDTO dto)
        {
            if (await _roleManager.RoleExistsAsync(dto.RoleName))
            {
                return BadRequest($"Role '{dto.RoleName}' already Exists.");
            }
            return Ok($"Role '{dto.RoleName}' created Succesfully.");
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return NotFound($"User with email '{dto.Email}' not found.");

            var roleExists = await _roleManager.RoleExistsAsync(dto.RoleName);
            if (!roleExists)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(dto.RoleName));
                if (!roleResult.Succeeded)
                    return BadRequest($"Failed to create role '{dto.RoleName}'.");
            }

           
            var result = await _userManager.AddToRoleAsync(user, dto.RoleName);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok($"Role '{dto.RoleName}' assigned to user '{dto.Email}' successfully.");
        }


    }
}
