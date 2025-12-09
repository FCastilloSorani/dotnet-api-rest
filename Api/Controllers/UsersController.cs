using Api.Entities;
using Api.Exceptions;
using Api.DTOs;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(ILogger<UsersController> logger, IUsersService usersService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAll()
    {
        var users = await usersService.GetAll();

        if (!users.ToList().Any())
        {
            return NotFound("No users found");
        }

        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetById([FromRoute] Guid id)
    {
        try
        {
            var user = await usersService.GetById(id);

            return Ok(user);
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An unexpected error occurred during searching user");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "Internal server error"
            );
        }
    }

    [HttpPost]
    public async Task<ActionResult<User>> Create([FromBody] CreateUserDto createUserDto)
    {
        try
        {
            var created = await usersService.Create(createUserDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created
            );
        }
        catch (UserAlreadyExistsException e)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An unexpected error occurred during creating user.");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "Internal server error"
            );
        }
    }


    [HttpPut("{id:guid}")]
    public async Task<ActionResult<User>> Update([FromRoute] Guid id, [FromBody] UpdateUserDto updateUserDto)
    {
        try
        {
            var updated = await usersService.Update(id, updateUserDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = updated.Id },
                updated
            );
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An unexpected error occurred during updating user.");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "Internal server error"
            );
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await usersService.Delete(id);

            return NoContent();
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An unexpected error occurred during deleting user.");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "Internal server error"
            );
        }
    }
}