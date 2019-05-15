using FluentValidation;
using FluentValidation.Results;
using MediatR;
using StarterApi.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarterApi.Infrastructure.Mediatr
{    
    public class ValidatingBehavior<TRequest, TResponse>
          : IPipelineBehavior<TRequest, TResponse>
              where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidatingBehavior(
            IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var result = _validator.Validate(request);

            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            return await next();
        }
    }
}
