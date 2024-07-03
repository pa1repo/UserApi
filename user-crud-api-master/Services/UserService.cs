namespace WebApi.Services;

using AutoMapper;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Users;
using WebApi.Repositories;

public interface IUserService
{
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(int id);
    Task Create(CreateRequest model);
    Task Update(int id, UpdateRequest model);
    Task Delete(int id);
}

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        IEnumerable<User> users = await _userRepository.GetAll();
        return users;
    }

    public async Task<User> GetById(int id)
    {
        var user = await _userRepository.GetById(id);

        if (user == null)
            throw new KeyNotFoundException("User not found");

        return user;
    }

    public async Task Create(CreateRequest model)
    {
        // validate
        if (await _userRepository.GetByUserName(model.user_name!) != null)
            throw new AppException("User with the username '" + model.user_name + "' already exists");

        // map model to new user object
        var user = _mapper.Map<User>(model);

        // save user
        await _userRepository.Create(user);
    }

    public async Task Update(int id, UpdateRequest model)
    {
        var user = await _userRepository.GetById(id);

        if (user == null)
            throw new KeyNotFoundException("User not found");

        // copy model props to user
        _mapper.Map(model, user);

        // save user
        await _userRepository.Update(user);
    }

    public async Task Delete(int id)
    {
        await _userRepository.Delete(id);
    }
}