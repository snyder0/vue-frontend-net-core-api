using AutoMapper;
using FluentValidation;
using MediatR;
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
        private readonly IMapper _mapper;

        public CreateUserRequestHandler(
            IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<GetUserDto> Handle(
            CreateUserRequest request, 
            CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<User>(request);
            var getDto = _mapper.Map<GetUserDto>(entity);
            return getDto;
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
