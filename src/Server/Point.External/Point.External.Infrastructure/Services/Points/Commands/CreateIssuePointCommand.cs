using MediatR;
using Point.External.Core.Domain.Entity;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Point.External.Infrastructure.Services.Points.Commands
{
    public class CreateIssuePointCommand : IRequest<Guid>
    {
        public IssuePoint Point { get; }

        public CreateIssuePointCommand(IssuePoint point)
        {
            Point = point;
        }
    }

    public class CreateIssuePointCommandHandler : IRequestHandler<CreateIssuePointCommand, Guid>
    {
        private readonly IRepository<IssuePoint> _pointRepository;

        public CreateIssuePointCommandHandler(IRepository<IssuePoint> pointRepository)
        {
            _pointRepository = pointRepository;
        }

        public async Task<Guid> Handle(CreateIssuePointCommand request, CancellationToken cancellationToken)
        {
            await _pointRepository.AddAsync(request.Point);

            return request.Point.Id; 
        }
    }
}
