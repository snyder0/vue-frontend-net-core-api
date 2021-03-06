﻿using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarterApi.Common.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace StarterApi.Services
{
    public interface IMediatorService
    {
        Task<Response<object>> Send<TResponse>(IRequest<TResponse> request);
    }

    public class MediatorService : IMediatorService
    {
        private readonly IMediator _mediator;

        public MediatorService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<object>> Send<TResponse>(IRequest<TResponse> request)
        {
            var response = new Response<object>();

            try
            {
                var result = await _mediator.Send(request);
                response.Data = result;
            }
            catch (ValidationException validationException)
            {
                response = ConvertValidationErrorsToErrorMessages<TResponse>(validationException);
            }

            return response;
        }

        private Response<object> ConvertValidationErrorsToErrorMessages<TResponse>(ValidationException result)
        {
            var response = new Response<object> { Data = new object() };

            foreach (var error in result.Errors)
            {
                var errorMessage = new ErrorMessage
                {
                    Message = error.ErrorMessage,
                    Property = error.PropertyName
                };
                response.ErrorMessages.Add(errorMessage);
            }

            return response;
        }
    }
}
