using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Authentication.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/admin/users
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList(); // Retrieve all users
            return Ok(users); // Return 200 OK with list of users
        }

        // GET: api/admin/users/{id}
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id); // Find user by ID
            if (user == null)
            {
                return NotFound(); // Return 404 Not Found if user not found
            }
            return Ok(user); // Return 200 OK with user details
        }

        // POST: api/admin/users
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser(CreateUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };

                var result = await _userManager.CreateAsync(user, model.Password); // Create new user

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.Role) && await _roleManager.RoleExistsAsync(model.Role))
                    {
                        await _userManager.AddToRoleAsync(user, model.Role); // Add user to specified role
                    }

                    return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user); // Return 201 Created with user details
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description); // Add errors to ModelState if user creation fails
                }
            }

            return BadRequest(ModelState); // Return 400 Bad Request with ModelState errors
        }

        // PUT: api/admin/users/{id}
        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(string id, EditUser model)
        {
            if (id != model.Id)
            {
                return BadRequest(); // Return 400 Bad Request if ID in URL doesn't match ID in model
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id); // Find user by ID

                if (user == null)
                {
                    return NotFound(); // Return 404 Not Found if user not found
                }

                user.Email = model.Email;
                user.UserName = model.Email;

                var result = await _userManager.UpdateAsync(user); // Update user details

                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var rolesToAdd = model.Roles.Except(userRoles).ToList();
                    var rolesToRemove = userRoles.Except(model.Roles).ToList();

                    await _userManager.AddToRolesAsync(user, rolesToAdd); // Add new roles to user
                    await _userManager.RemoveFromRolesAsync(user, rolesToRemove); // Remove roles not in updated list

                    return Ok(user); // Return 200 OK with updated user details
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description); // Add errors to ModelState if user update fails
                }
            }

            return BadRequest(ModelState); // Return 400 Bad Request with ModelState errors
        }

        // DELETE: api/admin/users/{id}
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id); // Find user by ID

            if (user == null)
            {
                return NotFound(); // Return 404 Not Found if user not found
            }

            var result = await _userManager.DeleteAsync(user); // Delete user

            if (result.Succeeded)
            {
                return NoContent(); // Return 204 No Content upon successful deletion
            }

            return BadRequest(); // Return 400 Bad Request if deletion fails
        }

        // Other actions can be similarly converted to API endpoints as needed
    }
}
