using AutoMapper;
using IdentityModel;
using Ordering.Core.Abstractions.Repositories;
using Ordering.DataAccess.Entities;
using Ordering.Domain.Models;
using System.Security.Claims;

namespace Ordering.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository<UserEntity> userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<User>> Get(int page, int size)
    {
        var userEntities = await _userRepository.Get(page, size);

        var users = userEntities.Select(
            userEntity => new User
            {
                UserId = userEntity.UserId,
                UserName = userEntity.UserName,
                UserEmail = userEntity.UserEmail
            });

        return users;
    }

    public async Task<User> Add(ClaimsPrincipal userClaims)
    {
        var userId = userClaims.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = userClaims.FindFirst(JwtClaimTypes.Name)?.Value;
        var userEmail = userClaims.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

        foreach (var claim in userClaims.Claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
        }

        var userEntity = new UserEntity
        {
            UserId = userId,
            UserName = userName,
            UserEmail = userEmail
        };

        var existedUserEntity = await _userRepository.GetUserById(userId);

        if (existedUserEntity != null) throw new Exception("User already exists");

        await _userRepository.Add(userEntity);

        var user = new User
        {
            UserId = userEntity.UserId,
            UserName = userEntity.UserName,
            UserEmail = userEntity.UserEmail
        };

        return user;
    }

    public async Task<User> GetUserById(string userId)
    {
        var existedUserEntity = await _userRepository.GetUserById(userId);

        var user = _mapper.Map<User>(existedUserEntity);

        return user;
    }

}
