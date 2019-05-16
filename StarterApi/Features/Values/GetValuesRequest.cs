using FluentValidation;
using MediatR;
using StarterApi.Common.Responses;
using StarterApi.Dtos.Values;
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
        public async Task<GetValuesDto> Handle(GetValuesRequest request, CancellationToken cancellationToken)
        {
            var response = new Response<GetValuesDto>();
            return new GetValuesDto { Values = new[] { "Value1", "Dog", "Cat", "Value4" } };
        }
    }

    public class GetValuesRequestValidator : AbstractValidator<GetValuesRequest>
    {
        public GetValuesRequestValidator()
        {
            RuleFor(x => x)
                .Must(x => false)
                .WithMessage("WithMessageFoo")
                .OverridePropertyName("OverridePropertyNameBar");
        }
    }
}
