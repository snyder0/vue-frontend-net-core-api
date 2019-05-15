using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarterApi.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace StarterApi.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<Response<TResponse>> Send<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
        {
            var response = new Response<TResponse>();

            try
            {
                var result = await _mediator.Send(request);
                response.Data = result;
            }
            catch(ValidationException validationException)
            {
                response = ConvertValidationErrorsToErrorMessages<TRequest, TResponse>(validationException);
            }

            return response;
        }

        private Response<TResponse> ConvertValidationErrorsToErrorMessages<TRequest, TResponse>(ValidationException result)
            where TRequest : IRequest<TResponse>
        {
            var response = new Response<TResponse>();

            foreach (var error in result.Errors)
            {
                var errorMessage = new ErrorMessage
                {
                    Message = error.ErrorMessage,
                    Property = error.PropertyName
                };
                response.ErrorMessages.Add(errorMessage);
            }

            response.IsValid = !response.ErrorMessages.Any();

            return response;
        }
    }
}
