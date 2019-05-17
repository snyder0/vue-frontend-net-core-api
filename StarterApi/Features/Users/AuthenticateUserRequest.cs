using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StarterApi.Common.Responses;
using StarterApi.Data;
using StarterApi.Data.Entities;
using StarterApi.Dtos;
using StarterApi.Infrastructure;
using StarterApi.Security;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StarterApi.Features.Users
{
    public class AuthenticateUserRequest 
        : IRequest<GetUserAuthenticationDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserRequestHandler 
        : IRequestHandler<AuthenticateUserRequest, GetUserAuthenticationDto>
    {
        private readonly DataContext _context;
        private readonly AppSettings _appSettings;

        public AuthenticateUserRequestHandler(
            DataContext context,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<GetUserAuthenticationDto> Handle(
            AuthenticateUserRequest request, 
            CancellationToken cancellationToken)
        {
            var user = _context.Set<User>().First(x => x.Email == request.Email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            _context.SaveChanges();

            var userToReturn = new GetUserAuthenticationDto
            {
                UserId = user.Id,
                Token = tokenHandler.WriteToken(token)
            };

            return userToReturn;
        }
    }

    public class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
    {
        private readonly DataContext _context;
        private bool _emailPassed = true;
        private bool _passwordPassed = true;

        public AuthenticateUserRequestValidator(
            DataContext context)
        {
            _context = context;

            RuleFor(x => x.Email)
                .NotEmpty()
                .OnFailure(x => _emailPassed = false);

            RuleFor(x => x.Password)
                .NotEmpty()
                .OnFailure(x => _passwordPassed = false);

            RuleFor(x => x)
                .Must(ExistInDatabase)
                .When(x => _emailPassed && _passwordPassed)
                .WithMessage(ErrorMessages.User.EmailOrPasswordIsIncorrect)
                .Must(BeCorrectEmailAndPassword)
                .When(x => _emailPassed && _passwordPassed)
                .WithMessage(ErrorMessages.User.EmailOrPasswordIsIncorrect);
        }

        private bool ExistInDatabase(AuthenticateUserRequest arg)
        {
            return _context.Set<User>().Any(x => x.Email == arg.Email);
        }

        private bool BeCorrectEmailAndPassword(AuthenticateUserRequest arg)
        {
            var user = _context.Set<User>().SingleOrDefault(x => x.Email == arg.Email);
            var passwordHash = new PasswordHash(user.PasswordSalt, user.PasswordHash);
            return passwordHash.Verify(arg.Password);
        }
    }
}
