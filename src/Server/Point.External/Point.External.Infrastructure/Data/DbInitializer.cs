using Point.External.Core.Domain.Entity;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Infrastructure.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IRepository<IssuePoint> _pointRepository;

        public DbInitializer(IRepository<IssuePoint> pointRepository)
        {
            _pointRepository = pointRepository;
        }

        public void InitializeDb()
        {
            var points = _pointRepository.GetAllAsync().Result;

            if (points.Count() > 0)
            {
                return;
            }

            foreach (var item in FakeDataFactory.IssuePoints())
            {
                _pointRepository.AddAsync(item);
            }
        }
    }
}
