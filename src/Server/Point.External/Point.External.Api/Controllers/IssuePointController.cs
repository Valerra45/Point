using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Point.External.Core.Domain.Entity;
using Point.External.Infrastructure.Services.Points.Queryes;
using Point.SharedKernel.DtoModels;
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
        private readonly IMapper _mapper;

        public IssuePointController(IMediator mediatr,
            IMapper mapper)
        {
            _mediatr = mediatr;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssuePoint>>> GetAllAsync()
        {
            var response = await _mediatr.Send(new GetAllIssuePointsQuery());

            return Ok(response);    
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IssuePointDto>> GetAsync(Guid id)
        {
            var point = await _mediatr.Send(new GetIssuePointByIdQuery(id));

            var response = _mapper.Map<IssuePointDto>(point);

            return Ok(response);
        }
    }
}
