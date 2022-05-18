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
    public class DeleteIssuePointCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public DeleteIssuePointCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteIssuePointCommandHandler : IRequestHandler<DeleteIssuePointCommand, bool>
    {
        private readonly IRepository<IssuePoint> _pointRepository;

        public DeleteIssuePointCommandHandler(IRepository<IssuePoint> pointRepository)
        {
            _pointRepository = pointRepository;
        }

        public async Task<bool> Handle(DeleteIssuePointCommand request, CancellationToken cancellationToken)
        {
            var issuePoint = await _pointRepository.GetByIdAsync(request.Id);

            if (issuePoint == null)
            {
                throw new EntityNotFoundException($"{nameof(IssuePoint)} with id '{request.Id}' doesn't exist");
            }

            await _pointRepository.DeleteAsync(issuePoint);

            return true;
        }
    }
}
