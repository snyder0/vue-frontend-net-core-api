using Microsoft.AspNetCore.Mvc;
using StarterApi.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarterApi.Controllers
{
    public class MediatorControllerBase : ControllerBase
    {
        public IActionResult Ok<TResponse>(Response<TResponse> response)
        {
            if (!response.IsValid) return BadRequest(response);
            return base.Ok(response);
        }

        public IActionResult Created<TResponse>(string uri, Response<TResponse> response)
        {
            if (!response.IsValid) return BadRequest(response);
            return base.Created(uri, response);
        }

        public IActionResult BadRequest<TResponse>(Response<TResponse> response)
        {
            return base.BadRequest(response);
        }
    }
}
