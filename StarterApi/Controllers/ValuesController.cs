using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarterApi.Common.Responses;
using StarterApi.Dtos.Values;
using StarterApi.Features.Values;

namespace StarterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ApiControllerBase
    {
        public ValuesController(IMediator mediator) : base(mediator)
        {
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<Response<IEnumerable<string>>>> Get()
        {
            var request = new GetValuesRequest();
            var result = await Send(request);
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
