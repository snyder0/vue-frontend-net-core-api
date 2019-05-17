using AutoMapper;
using FluentValidation;
using MediatR;
using StarterApi.Data;
using StarterApi.Data.Entities;
using StarterApi.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace StarterApi.Features.Users
{
    public class CreateUserRequest : 
        IRequest<GetUserDto>
    {   
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
            var entity = _mapper.Map<User>(request);
            
            _context.Set<User>().Add(entity);
            _context.SaveChanges();

            return _mapper.Map<GetUserDto>(entity);
        }
    }

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }
}
