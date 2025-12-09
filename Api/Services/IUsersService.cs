using Api.Entities;
using Api.DTOs;

namespace Api.Services;

public interface IUsersService
{
    public Task<List<User>> GetAll();
    public Task<User> GetById(Guid id);
    public Task<User> Create(CreateUserDto createUserDto);
    public Task<User> Update(Guid id, UpdateUserDto updateUserDto);
    public Task Delete(Guid id);
}