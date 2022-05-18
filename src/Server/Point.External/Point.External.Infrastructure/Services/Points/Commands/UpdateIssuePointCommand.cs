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

namespace Point.External.Infrastructure.Services.Points.Commands
{
    public class UpdateIssuePointCommand : IRequest<Guid>
    {
        public IssuePoint Point { get; }
        
        public UpdateIssuePointCommand(IssuePoint point)
        {
            Point = point;
        }
    }

    public class UpdateIssuePointCommandHandler : IRequestHandler<UpdateIssuePointCommand, Guid>
    {
        private readonly IRepository<IssuePoint> _pointRepository;

        public UpdateIssuePointCommandHandler(IRepository<IssuePoint> pointRepository)
        {
            _pointRepository = pointRepository;
        }

        public async Task<Guid> Handle(UpdateIssuePointCommand request, CancellationToken cancellationToken)
        {
            var issuePoint = request.Point;

            if (issuePoint == null)
            {
                throw new EntityNotFoundException($"{nameof(IssuePoint)} with id '{issuePoint.Id}' doesn't exist");
            }

            await _pointRepository.UpdateAsync(issuePoint);

            return issuePoint.Id;
        }
    }
}
