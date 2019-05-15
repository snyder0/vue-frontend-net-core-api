using MediatR;
using StarterApi.Dtos.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarterApi.Features.Values
{
    public class GetValuesRequest : IRequest<GetValuesDto> 
    {
        public string[] Values { get; set; }
    }

    public class GetValuesRequestHandler : IRequestHandler<GetValuesRequest, GetValuesDto>
    {
        private readonly IMediator _mediator;

        public GetValuesRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<GetValuesDto> Handle(GetValuesRequest request, CancellationToken cancellationToken)
        {
            return new GetValuesDto { Values = new [] { "Value1", "Dog", "Cat", "Value4" } };
        }
    }
}
