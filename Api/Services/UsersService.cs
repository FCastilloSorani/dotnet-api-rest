using Api.Entities;
using Api.Exceptions;
using Api.DTOs;

namespace Api.Services;

public class UsersService(ILogger<UsersService> logger) : IUsersService
{
    private List<User> _users = [];

    public Task<List<User>> GetAll()
    {
        return Task.FromResult(_users);
    }

    public Task<User> GetById(Guid id)
    {
        var user = _users.SingleOrDefault(x => x.Id.Equals(id));

        if (user == null)
        {
            throw new UserNotFoundException($"User with id {id} not found.");
        }

        return Task.FromResult(user);
    }

    public async Task<User> Create(CreateUserDto createUserDto)
    {
        try
        {
            var alreadyExists = _users.Any(x => x.Username.Equals(
                    createUserDto.Username,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (alreadyExists)
            {
                throw new UserAlreadyExistsException($"User with username '{createUserDto.Username}' already exists.");
            }

            var user = new User
            {
                Id = Guid.CreateVersion7(),
                Username = createUserDto.Username,
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
            };

            await Task.Yield();

            _users.Add(user);

            return user;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to create user");
            throw;
        }
    }

    public async Task<User> Update(Guid id, UpdateUserDto updateUserDto)
    {
        try
        {
            var found = await GetById(id);

            var updated = found with
            {
                Username = updateUserDto.Username ?? found.Username,
                FirstName = updateUserDto.FirstName ?? found.FirstName,
                LastName = updateUserDto.LastName ?? found.LastName
            };

            var index = _users.FindIndex(x => x.Id.Equals(found.Id));

            _users[index] = updated;

            return updated;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to update user");
            throw;
        }
    }

    public async Task Delete(Guid id)
    {
        try
        {
            var found = await GetById(id);

            _users.Remove(found);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to delete user");
            throw;
        }
    }
}