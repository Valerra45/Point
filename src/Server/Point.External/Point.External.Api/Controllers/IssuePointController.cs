using MediatR;
using Microsoft.AspNetCore.Mvc;
using Point.External.Api.Services.IssuePoints.Queryes;
using Point.External.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuePointController
        : ControllerBase
    {
        private readonly IMediator _mediatr;

        public IssuePointController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<ActionResult<IssuePoint>> GetAllAsync()
        {
            var response = await _mediatr.Send(new GetAllIssuePointsQuery());

            return Ok(response);    
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IssuePoint>> GetAsync(Guid id)
        {
            var respons = await _mediatr.Send(new GetIssuePointByIdQuery(id));

            return Ok(respons);
        }
    }
}
