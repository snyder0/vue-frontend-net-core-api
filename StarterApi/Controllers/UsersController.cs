using Microsoft.AspNetCore.Mvc;
using StarterApi.Common.Responses;
using StarterApi.Dtos;
using StarterApi.Features.Users;
using StarterApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StarterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : MediatorControllerBase
    {
        private readonly IMediatorService _mediatorService;

        public UsersController(IMediatorService mediatorService)
        {
            _mediatorService = mediatorService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Response<List<GetUserDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllUsersRequest();
            var result = await _mediatorService.Send(request);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<GetUserDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(CreateUserRequest request)
        {
            var result = await _mediatorService.Send(request);
            return Created("api/users/Id-Goes-Here", result);
        }
    }
}
