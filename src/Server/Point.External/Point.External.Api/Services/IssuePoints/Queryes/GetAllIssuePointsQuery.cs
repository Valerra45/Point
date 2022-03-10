using MediatR;
using Point.External.Core.Domain.Entity;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Point.External.Api.Services.IssuePoints.Queryes
{
    public class GetAllIssuePointsQuery : IRequest<IEnumerable<IssuePoint>> { }

    public class GetAllIssuePointsQueryHandler : IRequestHandler<GetAllIssuePointsQuery, IEnumerable<IssuePoint>>
    {
        private readonly IRepository<IssuePoint> _pointRepository;

        public GetAllIssuePointsQueryHandler(IRepository<IssuePoint> pointRepository)
        {
            _pointRepository = pointRepository;
        }

        public async Task<IEnumerable<IssuePoint>> Handle(GetAllIssuePointsQuery request, CancellationToken cancellationToken)
        {
            var points = await _pointRepository.GetAllAsync();

            return points;  
        }
    }
}
