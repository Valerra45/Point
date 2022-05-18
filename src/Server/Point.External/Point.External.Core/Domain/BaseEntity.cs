using MongoDB.Bson.Serialization.Attributes;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Core.Domain
{
    public class BaseEntity : IBaseEntity
    {
        public BaseEntity()
        {
            Created = DateTime.Now;
        }

        [BsonId]
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Update { get; set; }
    }
}
