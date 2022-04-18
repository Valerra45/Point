using MediatR;
using Point.External.Core.Domain.Entity;
using Point.SharedKernel.Abstractions;
using Point.SharedKernel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Point.External.Infrastructure.Services.Points.Queryes
{
    public class GetIssuePointByIdQuery : IRequest<IssuePoint>
    {
        public Guid Id { get; set; }

        public GetIssuePointByIdQuery(Guid id)
        {
                Id = id;   
        }
    }

    public class GetIssuePointByIdQueryHandler : IRequestHandler<GetIssuePointByIdQuery, IssuePoint>
    {
        private readonly IRepository<IssuePoint> _pointRepository;

        public GetIssuePointByIdQueryHandler(IRepository<IssuePoint> pointRepository)
        {
            _pointRepository = pointRepository;
        }

        public async Task<IssuePoint> Handle(GetIssuePointByIdQuery request, CancellationToken cancellationToken)
        {
            var point = await _pointRepository.GetByIdAsync(request.Id);

            if (point == null)
            {
                throw new EntityNotFoundException($"{nameof(IssuePoint)} with id '{request.Id}' doesn't exist");
            }

            return point;
        }
    }

}
