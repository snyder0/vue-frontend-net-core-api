using AutoMapper;
using FluentValidation;
using MediatR;
using StarterApi.Common.Constants;
using StarterApi.Common.Responses;
using StarterApi.Data;
using StarterApi.Data.Entities;
using StarterApi.Dtos;
using StarterApi.Security;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarterApi.Features.Users
{
    public class CreateUserRequest : 
        IRequest<GetUserDto>
    {   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserRequestHandler :
        IRequestHandler<CreateUserRequest, GetUserDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CreateUserRequestHandler(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetUserDto> Handle(
            CreateUserRequest request, 
            CancellationToken cancellationToken)
        {
            var passwordHash = new PasswordHash(request.Password);

            var entity = _mapper.Map<User>(request);
            entity.Role = RoleConstants.User;
            entity.PasswordSalt = passwordHash.Salt;
            entity.PasswordHash = passwordHash.Hash;
            
            _context.Set<User>().Add(entity);
            _context.SaveChanges();

            return _mapper.Map<GetUserDto>(entity);
        }
    }

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        private readonly DataContext _context;

        public CreateUserRequestValidator(
            DataContext context)
        {
            _context = context;

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress()
                .Must(BeUniqueEmail)
                .WithMessage(ErrorMessages.User.EmailAlreadyExists);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);
            
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();
        }

        private bool BeUniqueEmail(string arg)
        {
            return !_context.Set<User>().Any(x => x.Email == arg);
        }
    }
}
