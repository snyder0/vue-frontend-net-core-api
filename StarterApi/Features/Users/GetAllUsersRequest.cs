using FluentValidation;
using MediatR;
using StarterApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarterApi.Features.Users
{
    public class GetAllUsersRequest : IRequest<List<GetUserDto>>
    {
    }

    public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, List<GetUserDto>>
    {
        public async Task<List<GetUserDto>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            return new List<GetUserDto>
            {
                new GetUserDto { FirstName = "Jim", LastName = "Smith" },
                new GetUserDto { FirstName = "Jane", LastName = "Doe" }
            };
        }
    }

    public class GetAllUsersRequestValidator : AbstractValidator<GetAllUsersRequest>
    {
        public GetAllUsersRequestValidator()
        {

        }
    }
}
